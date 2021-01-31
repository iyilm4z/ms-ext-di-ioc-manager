using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public class IocManager : IIocManager
    {
        public static IocManager Instance { get; }

        public ServiceCollection Services { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        static IocManager()
        {
            Instance = new IocManager();
        }

        private IocManager()
        {
            Services = new ServiceCollection();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();

            //Register self!
            Services.AddSingleton(this);
            Services.AddSingleton<IIocManager>(this);
            Services.AddSingleton<IIocRegistrar>(this);
            Services.AddSingleton<IIocResolver>(this);
        }

        public void BuildServiceProvider()
        {
            ServiceProvider ??= Services.BuildServiceProvider();
        }

        public void Register<TService>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TService : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    Services.AddTransient<Type>();
                    break;
                case DependencyLifeStyle.Singleton:
                    Services.AddSingleton<Type>();
                    break;
                case DependencyLifeStyle.Scoped:
                    Services.AddScoped<Type>();
                    break;
                default:
                    Services.AddTransient<Type>();
                    break;
            }
        }

        public void Register(Type serviceType, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    Services.AddTransient(serviceType);
                    break;
                case DependencyLifeStyle.Singleton:
                    Services.AddSingleton(serviceType);
                    break;
                case DependencyLifeStyle.Scoped:
                    Services.AddScoped(serviceType);
                    break;
                default:
                    Services.AddTransient(serviceType);
                    break;
            }
        }

        public void Register<TService, TImplementation>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TService : class
            where TImplementation : class, TService
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    Services.AddTransient<TService, TImplementation>();
                    break;
                case DependencyLifeStyle.Singleton:
                    Services.AddSingleton<TService, TImplementation>();
                    break;
                case DependencyLifeStyle.Scoped:
                    Services.AddScoped<TService, TImplementation>();
                    break;
                default:
                    Services.AddTransient<TService, TImplementation>();
                    break;
            }
        }

        public void Register(Type serviceType, Type implementationType,
            DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    Services.AddTransient(serviceType, implementationType);
                    break;
                case DependencyLifeStyle.Singleton:
                    Services.AddSingleton(serviceType, implementationType);
                    break;
                case DependencyLifeStyle.Scoped:
                    Services.AddScoped(serviceType, implementationType);
                    break;
                default:
                    Services.AddTransient(serviceType, implementationType);
                    break;
            }
        }

        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            var context = new ConventionalRegistrationContext(assembly, this);

            foreach (var registrar in _conventionalRegistrars)
            {
                registrar.RegisterAssembly(context);
            }
        }

        public T Resolve<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public T Resolve<T>(Type serviceType)
        {
            return (T) ServiceProvider.GetService(serviceType);
        }

        public object Resolve(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType);
        }

        public T[] ResolveAll<T>()
        {
            return ServiceProvider.GetServices<T>().ToArray();
        }

        public object[] ResolveAll(Type serviceType)
        {
            return ServiceProvider.GetServices(serviceType).ToArray();
        }

        public bool IsRegistered(Type serviceType)
        {
            return (bool) ServiceProvider.GetServices(serviceType)?.Any();
        }

        public bool IsRegistered<TService>()
        {
            return (bool) ServiceProvider.GetServices<TService>()?.Any();
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}