using Microsoft.Practices.Unity;
using audatex.br.centralpublisher.infrastructure.persistence.interfaces;
using audatex.br.centralpublisher.domain.repository.interfaces;

namespace audatex.br.centralpublisher.domain.repository
{
    public static class RepositoryFactory
    {
        public static IRepository<T> CreateRepository<T, R>(IUnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IRepository<T>), typeof(R)).RegisterInstance(unitOfWork);

            return container.Resolve<IRepository<T>>();
        }

    }
}