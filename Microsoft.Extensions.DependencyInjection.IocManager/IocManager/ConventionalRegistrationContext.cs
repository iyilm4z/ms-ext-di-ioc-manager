using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public Assembly Assembly { get; private set; }
        
        public IIocManager IocManager { get; private set; }

        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager)
        {
            Assembly = assembly;
            IocManager = iocManager;
        }
    }
}