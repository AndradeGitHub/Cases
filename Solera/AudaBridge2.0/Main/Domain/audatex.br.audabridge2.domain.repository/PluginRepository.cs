using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.domain.model.enumerator;
using audatex.br.audabridge2.infrastructure.persistence;
using audatex.br.audabridge2.infrastructure.persistence.interfaces;
using audatex.br.audabridge2.domain.repository.interfaces;

namespace audatex.br.audabridge2.domain.repository
{
    public class PluginRepository : IPluginRepository<PluginModel>
    {
        private readonly IUnitOfWork _db;

        public PluginRepository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public PluginModel GetPlugin(PluginModel plugin)
        {
            var result = _db.Plugin
                            .Include(x => x.Seguradora)
                            .Where(x => x.Status == (int)EnumStatus.Ativo &&
                                        x.Tomada == plugin.Tomada &&
                                        x.Seguradora.Nome == plugin.Seguradora.Nome &&
                                        x.Seguradora.Status == (int)EnumStatus.Ativo)
                            .FirstOrDefault();

            return result;
        }
    }
}