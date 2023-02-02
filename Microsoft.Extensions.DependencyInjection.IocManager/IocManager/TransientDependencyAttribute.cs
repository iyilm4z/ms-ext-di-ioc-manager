using System;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class TransientDependencyAttribute : Attribute
    {
    }
}