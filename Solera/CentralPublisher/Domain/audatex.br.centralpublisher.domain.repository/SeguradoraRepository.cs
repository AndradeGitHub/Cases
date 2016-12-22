using System.Linq;
using System.Collections.Generic;
using audatex.br.centralpublisher.domain.model;
using audatex.br.centralpublisher.infrastructure.persistence.interfaces;
using audatex.br.centralpublisher.domain.model.Enum;

namespace audatex.br.centralpublisher.domain.repository
{
    public class SeguradoraRepository : Repository<SeguradoraModel>
    {
        private readonly IUnitOfWork _db;

        public SeguradoraRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public IEnumerable<SeguradoraModel> GetSeguradoras()
        {

            var resultado = _db.Seguradoras.Where(x =>
                     x.ListaQueue.Any(z =>
                        z.Status == (int)EnumQueueStatus.Ativo &&
                        z.Origem == (int)EnumQueueOrigemDestino.AxPedido &&
                        z.Destino == (int)EnumQueueOrigemDestino.Central)).ToList();

            return resultado;
        } 
    }
}

