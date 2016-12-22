using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using audatex.br.centralconsumer.domain.model;
using audatex.br.centralconsumer.infrastructure.persistence.interfaces;


namespace audatex.br.centralconsumer.domain.repository
{
   public class PedidoStatusRepository : Repository<PedidoStatusModel>
    {
        private readonly IUnitOfWork _db;

        public PedidoStatusRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public IEnumerable<PedidoStatusModel> GetConfirmacaoPedidosSeguradora()
        {
           
            var Resultado = (from _p in _db.PedidoStatus
                            where _p.TipoOperacao == (int)EnumTipoOperacaoModel.ConfPedidos03 &&
                            _p.StatusOperacao == (int) EnumStatusOperacaoModel.Disponivel
                             orderby _p.IdPedido ascending
                            select _p).ToList();

            return Resultado;
        }
        public override void Add(PedidoStatusModel PedidoStatusModel)
        {
            _db.PedidoStatus.Add(PedidoStatusModel);
        }


    }
}
