using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public interface IIocRegistrar
    {
        void Register<TService>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TService : class;

        void Register(Type serviceType, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        void Register<TService, TImplementation>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TService : class
            where TImplementation : class, TService;

        void Register(Type serviceType, Type implementationType,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        void RegisterAssemblyByConvention(Assembly assembly);

        bool IsRegistered(Type serviceType);

        bool IsRegistered<TService>();
    }
}