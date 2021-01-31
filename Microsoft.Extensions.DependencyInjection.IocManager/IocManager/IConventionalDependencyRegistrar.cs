namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public interface IConventionalDependencyRegistrar
    {
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}