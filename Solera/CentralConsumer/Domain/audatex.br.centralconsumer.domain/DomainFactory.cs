using Microsoft.Practices.Unity;

namespace audatex.br.centralconsumer.domain
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