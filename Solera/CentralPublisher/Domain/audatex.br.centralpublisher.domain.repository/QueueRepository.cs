using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using audatex.br.centralpublisher.domain.model;
using audatex.br.centralpublisher.infrastructure.persistence.interfaces;
using audatex.br.centralpublisher.domain.model.Enum;

namespace audatex.br.centralpublisher.domain.repository
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
                                         x.Origem == (int)EnumQueueOrigemDestino.Central && 
                                         x.Destino == (int)EnumQueueOrigemDestino.AxPedido
                                     )
                              .ToList();

            return lstQueue;
        }

        public QueueModel GetQueuePorSeguradora(EnumQueueOperacao QueueOperacao, SeguradoraModel SeguradoraModel)
        {
            var Resultado = _db.Queue
                              .Where(x =>
                                         x.Operacao == (int)QueueOperacao &&
                                         x.Status == (int)EnumQueueStatus.Ativo &&
                                         x.Origem == (int)EnumQueueOrigemDestino.Central &&
                                         x.Destino == (int)EnumQueueOrigemDestino.AxPedido &&
                                         x.SeguradoraId == SeguradoraModel.Id
                                     )
                              .FirstOrDefault();
            return Resultado;
        }

    }
}