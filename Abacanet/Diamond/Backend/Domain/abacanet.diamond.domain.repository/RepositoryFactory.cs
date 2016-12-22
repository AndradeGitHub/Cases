using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using abacanet.diamond.infrastructure.persistence.interfaces;
using abacanet.diamond.domain.repository.interfaces;

namespace abacanet.diamond.domain.repository
{
    public static class RepositoryFactory
    {
        public static IRepository<T> CreateRepository<T, R>(IUnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();

            container.RegisterType(typeof(IRepository<T>), typeof(R)).RegisterInstance(unitOfWork);

            return container.Resolve<IRepository<T>>();
        }
    }
}