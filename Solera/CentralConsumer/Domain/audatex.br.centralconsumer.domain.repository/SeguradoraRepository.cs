using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using audatex.br.centralconsumer.domain.model;
using audatex.br.centralconsumer.infrastructure.persistence.interfaces;
using audatex.br.centralconsumer.domain.model.Enum;

namespace audatex.br.centralconsumer.domain.repository
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
        public SeguradoraModel GetSeguradora(string Cnpj)
        {                   
            var resultado = (from _p in _db.Seguradoras
                             where _p.CNPJ == Cnpj
                           select _p).FirstOrDefault();
            return resultado;
        }
        public override void Add(SeguradoraModel SeguradoraModel)
        {
            _db.Seguradoras.Add(SeguradoraModel);
        }
    }
}
