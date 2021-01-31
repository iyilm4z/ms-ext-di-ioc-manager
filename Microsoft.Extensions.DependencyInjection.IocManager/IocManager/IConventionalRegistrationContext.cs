using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public interface IConventionalRegistrationContext
    {
        Assembly Assembly { get; }
        
        IIocManager IocManager { get; }
    }
}