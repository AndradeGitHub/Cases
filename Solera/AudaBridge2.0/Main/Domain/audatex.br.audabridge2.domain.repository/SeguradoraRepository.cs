using System.Linq;
using System.Collections.Generic;

using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.domain.model.enumerator;
using audatex.br.audabridge2.infrastructure.persistence.interfaces;
using audatex.br.audabridge2.domain.repository.interfaces;

namespace audatex.br.audabridge2.domain.repository
{
    public class SeguradoraRepository : ISeguradoraRepository<SeguradoraModel>
    {
        private readonly IUnitOfWork _db;

        public SeguradoraRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public IEnumerable<SeguradoraModel> GetSeguradora()
        {
            var result = _db.Seguradora
                            .Where(x => x.Status == (int)EnumStatus.Ativo)
                            .ToList();

            return result;
        }
    }
}