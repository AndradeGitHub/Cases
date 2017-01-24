using Microsoft.Practices.Unity;

using audatex.br.audabridge2.domain.interfaces;
using audatex.br.audabridge2.infrastructure.persistence.interfaces;

namespace audatex.br.audabridge2.domain
{
    public static class DomainFactory
    {
        public static IOperation CreateDomain<O>(IUnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IOperation), typeof(O)).RegisterInstance(unitOfWork);

            return container.Resolve<IOperation>();
        }
    }
}
