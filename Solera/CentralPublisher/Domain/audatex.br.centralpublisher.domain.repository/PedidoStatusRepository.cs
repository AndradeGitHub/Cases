using System.Linq;
using System.Collections.Generic;
using audatex.br.centralpublisher.domain.model;
using audatex.br.centralpublisher.infrastructure.persistence.interfaces;




namespace audatex.br.centralpublisher.domain.repository
{
    public class PedidoStatusRepository : Repository<PedidoStatusModel>
    {
        private readonly IUnitOfWork _db;

        public PedidoStatusRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
 

        public IEnumerable<PedidoStatusModel> GetPedidoStatusDisponivelPorOperacao(EnumTipoOperacaoModel TipoOperacaoModel)
        {
            var resultado = (from _p in _db.PedidoStatus
                             where _p.TipoOperacao == (int)TipoOperacaoModel && _p.StatusOperacao == (int)EnumStatusOperacaoModel.Disponivel
                             select _p).ToList();

            return resultado;
        }

        public override void Add(PedidoStatusModel PedidoStatusModel)
        {
            _db.PedidoStatus.Add(PedidoStatusModel);
        }

    }
}

