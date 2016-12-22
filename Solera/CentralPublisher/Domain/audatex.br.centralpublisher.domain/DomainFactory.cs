using Microsoft.Practices.Unity;

using audatex.br.centralpublisher.domain.interfaces;

namespace audatex.br.centralpublisher.domain
{
    public static class DomainFactory
    {
        public static IOperation CreateDomain<O>()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IOperation), typeof(O));

            return container.Resolve<IOperation>();
        }
    }
}
