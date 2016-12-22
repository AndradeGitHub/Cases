using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

using Newtonsoft.Json;

using audatex.br.centralpublisher.infrastructure.invoke.interfaces;
using audatex.br.centralpublisher.infrastructure.log;
using audatex.br.centralpublisher.domain.model;

namespace audatex.br.centralpublisher.infrastructure.invoke
{
    public class QueueInvoke : IQueueInvoke
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

        public void PublishDirect<T>(string exchange, string queue, T message)
        {
            try
            {
                using (IConnection connection = _connectionFactory.CreateConnection())
                {
                    using (IModel model = connection.CreateModel())
                    {
                        model.QueueBind(queue, exchange, "", new Dictionary<string, object>());

                        var json = JsonConvert.SerializeObject(message);

                        IBasicProperties basicProperties = model.CreateBasicProperties();
                        basicProperties.ContentType = "application/json";
                        basicProperties.SetPersistent(true);
                        basicProperties.DeliveryMode = 2;

                        model.BasicPublish(exchange, "", false, basicProperties, Encoding.UTF8.GetBytes(json));

                        model.QueueUnbind(queue, exchange, "", new Dictionary<string, object>());
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
        }
    }
}
