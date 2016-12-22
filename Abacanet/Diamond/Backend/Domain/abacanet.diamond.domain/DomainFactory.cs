using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using abacanet.diamond.domain.interfaces;
using abacanet.diamond.infrastructure.persistence.interfaces;

namespace abacanet.diamond.domain
{
    public static class DomainFactory
    {
        public static IOperation<T> CreateDomain<T, O>(IUnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();

            container.RegisterType(typeof(IOperation<T>), typeof(O)).RegisterInstance(unitOfWork);

            return container.Resolve<IOperation<T>>();
        }
    }
}
