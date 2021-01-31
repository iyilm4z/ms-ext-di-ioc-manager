using Microsoft.Extensions.DependencyInjection.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            //Transient
            typeof(ITransientDependency)
                .AssignedTypesInAssembly(context.Assembly)
                .ForEach(assignedType =>
                {
                    //Self
                    context.IocManager.Services.AddTransient(assignedType);

                    //DefaultInterfaces
                    assignedType
                        .GetDefaultInterfaces()
                        .ForEach(defaultInterface =>
                        {
                            context.IocManager.Services.AddTransient(defaultInterface, assignedType);
                        });
                });

            //Singleton
            typeof(ISingletonDependency)
                .AssignedTypesInAssembly(context.Assembly)
                .ForEach(assignedType =>
                {
                    //Self
                    context.IocManager.Services.AddSingleton(assignedType);

                    //DefaultInterfaces
                    assignedType
                        .GetDefaultInterfaces()
                        .ForEach(defaultInterface =>
                        {
                            context.IocManager.Services.AddSingleton(defaultInterface, assignedType);
                        });
                });

            //Scoped
            typeof(IScopedDependency)
                .AssignedTypesInAssembly(context.Assembly)
                .ForEach(assignedType =>
                {
                    //Self
                    context.IocManager.Services.AddScoped(assignedType);

                    //DefaultInterfaces
                    assignedType
                        .GetDefaultInterfaces()
                        .ForEach(defaultInterface =>
                        {
                            context.IocManager.Services.AddScoped(defaultInterface, assignedType);
                        });
                });
        }
    }
}