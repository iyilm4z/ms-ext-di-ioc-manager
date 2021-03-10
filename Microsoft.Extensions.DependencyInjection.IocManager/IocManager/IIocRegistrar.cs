using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public interface IIocRegistrar
    {
        void Register<TService>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
            where TService : class;

        void Register(Type serviceType, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient);

        void Register<TService, TImplementation>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
            where TService : class
            where TImplementation : class, TService;

        void Register(Type serviceType, Type implementationType, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient);

        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        void RegisterAssemblyByConvention(Assembly assembly);

        bool IsRegistered(Type serviceType);

        bool IsRegistered<TService>();
    }
}