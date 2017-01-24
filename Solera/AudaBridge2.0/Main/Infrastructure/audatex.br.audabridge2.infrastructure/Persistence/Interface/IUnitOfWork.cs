using System.Data.Entity;

using audatex.br.audabridge2.domain.model;

namespace audatex.br.audabridge2.infrastructure.persistence.interfaces
{
    public interface IUnitOfWork
    {
        IDbSet<SeguradoraModel> Seguradora { get; set; }
        IDbSet<PluginModel> Plugin { get; set; }
        IDbSet<OperacaoModel> Operacao { get; set; }
        IDbSet<IntegracaoModel> Integracao { get; set; }

        void Commit();
    }
}
