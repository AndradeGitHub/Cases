using System;
using System.Collections.Generic;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using Newtonsoft.Json;

using audatex.br.centralconsumer.infrastructure.invoke.interfaces;
using audatex.br.centralconsumer.infrastructure.log;
using audatex.br.centralconsumer.domain.model;

namespace audatex.br.centralconsumer.infrastructure.invoke
{
    public class QueueInvoke : IQueueInvoke<QueueModel>
    {
        private ConnectionFactory _connectionFactory;

        public QueueInvoke(IDictionary<string, string> rabbitMQConn)
        {
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.VirtualHost = rabbitMQConn["VirtualHost"];
            _connectionFactory.UserName = rabbitMQConn["UserName"];
            _connectionFactory.Password = rabbitMQConn["Password"];
            _connectionFactory.Uri = rabbitMQConn["HostName"];
        }

        public List<object> BasicConsumerListener(List<QueueModel> lstQueue)
        {
            var lstMessage = new List<object>();

            try
            {
                using (IConnection connection = _connectionFactory.CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        foreach (var queue in lstQueue)
                        {
                            string jsonMessage = string.Empty;

                            lock (channel)
                            {
                                channel.QueueBind(queue.Nome, queue.Exchange, "", new Dictionary<string, object>());

                                var consumer = new EventingBasicConsumer();
                                consumer.ConsumerTag = Guid.NewGuid().ToString();

                                consumer.Received += (o, e) =>
                                {
                                    string message = Encoding.UTF8.GetString(e.Body);
                                    jsonMessage = JsonConvert.SerializeObject(message);

                                    //Diz ao RabbitMQ que a mensagem foi lida com sucesso pelo consumidor
                                    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: true);

                                    lstMessage.Add(jsonMessage);
                                };

                                //Registra o consumidor no RabbitMQ
                                channel.BasicConsume(queue.Nome, noAck: false, consumer: consumer);

                                channel.QueueUnbind(queue.Nome, queue.Exchange, "", new Dictionary<string, object>());
                            }
                        }
                    }
                }                              
            }
            catch (BrokerUnreachableException bex)
            {
                Exception ex = bex;

                while (ex != null)
                    Log.RecordError(ex.InnerException);
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);
            }

            return lstMessage;
        }
    }
}
