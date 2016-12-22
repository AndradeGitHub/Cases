using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using audatex.br.centralconsumer.infrastructure.persistence.interfaces;
using audatex.br.centralconsumer.domain.repository.Interfaces;

namespace audatex.br.centralconsumer.domain.repository
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