using Microsoft.Practices.Unity;

using audatex.br.audabridge2.infrastructure.persistence.interfaces;

namespace audatex.br.audabridge2.infrastructure.persistence
{
    public static class RepositoryFactory
    {
        public static IRepository<T> CreateRepository<T, R>(UnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IRepository<T>), typeof(R), new InjectionConstructor(unitOfWork));

            return container.Resolve<IRepository<T>>();
        }

        public static IRepositoryCustom<T> CreateRepositoryCustom<T, R>(UnitOfWork unitOfWork)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IRepositoryCustom<T>), typeof(R), new InjectionConstructor(unitOfWork));

            return container.Resolve<IRepositoryCustom<T>>();
        }

        public static IRepository<T> CreateRepository<T, R>(dynamic repositoryBase, string collection)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType(typeof(IRepository<T>), typeof(R), new InjectionConstructor(repositoryBase, collection));

            return container.Resolve<IRepository<T>>();
        }

        //public static IRepositoryCustom<T> CreateRepositoryCustom<T, R>(dynamic repositoryBase, string collection)
        //{
        //    IUnityContainer container = new UnityContainer();

        //    container.RegisterType(typeof(IRepositoryCustom<T>), typeof(R), new InjectionConstructor(repositoryBase, collection));

        //    return container.Resolve<IRepositoryCustom<T>>();
        //}
    }
}