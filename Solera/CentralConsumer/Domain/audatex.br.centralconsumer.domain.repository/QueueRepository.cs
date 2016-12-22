using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using audatex.br.centralconsumer.domain.model;
using audatex.br.centralconsumer.infrastructure.persistence.interfaces;
using audatex.br.centralconsumer.domain.model.Enum;

namespace audatex.br.centralconsumer.domain.repository
{
    public class QueueRepository : Repository<QueueModel>
    {
        private readonly IUnitOfWork _db;

        public QueueRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public IEnumerable<QueueModel> GetQueue(QueueModel queue)
        {
            var lstQueue = _db.Queue
                              .Where(x =>
                                         x.Operacao == queue.Operacao &&
                                         x.Status == (int)EnumQueueStatus.Ativo &&
                                         x.Origem == (int)EnumQueueOrigemDestino.AxPedido &&
                                         x.Destino == (int)EnumQueueOrigemDestino.Central                                          
                                     )
                              .ToList();

            return lstQueue;
        }
 
    }
}