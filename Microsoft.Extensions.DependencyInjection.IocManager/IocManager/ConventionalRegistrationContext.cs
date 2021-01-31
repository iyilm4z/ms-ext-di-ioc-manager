using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public Assembly Assembly { get; }
        
        public IIocManager IocManager { get; }

        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager)
        {
            Assembly = assembly;
            IocManager = iocManager;
        }
    }
}