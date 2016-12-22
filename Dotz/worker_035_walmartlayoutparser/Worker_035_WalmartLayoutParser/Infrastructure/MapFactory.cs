using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using worker_WalmartLayoutParser.map.interfaces;

namespace worker_WalmartLayoutParser.infrastructure
{
    public static class MapFactory
    {
        public static IMap CreateMap<M>()
        {
            IUnityContainer container = new UnityContainer();

            container.LoadConfiguration();

            container.RegisterType(typeof(IMap), typeof(M));

            return container.Resolve<IMap>();
        }
    }
}