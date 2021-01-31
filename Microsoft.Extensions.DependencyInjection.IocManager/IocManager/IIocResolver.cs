using System;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public interface IIocResolver
    {
        T Resolve<T>();

        T Resolve<T>(Type serviceType);
        
        object Resolve(Type serviceType);
        
        T[] ResolveAll<T>();
        
        object[] ResolveAll(Type serviceType);
        
        bool IsRegistered<T>();
        
        bool IsRegistered(Type serviceType);
    }
}