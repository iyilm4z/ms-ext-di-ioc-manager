using System;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        ServiceCollection Services { get; }

        IServiceProvider ServiceProvider { get; }

        void BuildServiceProvider();

        new bool IsRegistered(Type serviceType);

        new bool IsRegistered<T>();
    }
}