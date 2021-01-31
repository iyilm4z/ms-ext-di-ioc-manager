using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.IocManager
{
    public interface IFooService
    {
        void DoNothing();
    }

    public interface IBarService
    {
        public int Counter { get; }

        void Count();
    }

    public interface IBazService
    {
        void DoNothing();
    }

    public class FooService : IFooService, ITransientDependency
    {
        public void DoNothing()
        {
        }
    }

    public class BarService : IBarService, ISingletonDependency
    {
        public int Counter { get; private set; }

        public void Count()
        {
            Counter++;
        }
    }

    public class BazService : IBazService, IScopedDependency
    {
        public void DoNothing()
        {
        }
    }

    internal static class Program
    {
        static void Main(string[] args)
        {
            IocManager.Instance.AddConventionalRegistrar(new BasicConventionalRegistrar());
            IocManager.Instance.RegisterAssemblyByConvention(Assembly.GetEntryAssembly());

            //Call before resolving
            IocManager.Instance.BuildServiceProvider();

            if (!IocManager.Instance.IsRegistered<IFooService>())
            {
                throw new Exception($"{nameof(IFooService)} is not registered.");
            }

            var fooService = IocManager.Instance.Resolve<IFooService>();
            fooService.DoNothing();

            if (!IocManager.Instance.IsRegistered<IBarService>())
            {
                throw new Exception($"{nameof(IBarService)} is not registered.");
            }

            var barService1 = IocManager.Instance.Resolve<IBarService>();
            barService1.Count();
            barService1.Count();
            var barService2 = IocManager.Instance.Resolve<IBarService>();
            barService1.Count();
            barService1.Count();

            if (barService1.Counter != barService2.Counter)
            {
                throw new Exception($"{nameof(IBarService)} is not singleton.");
            }

            if (!IocManager.Instance.IsRegistered<IBazService>())
            {
                throw new Exception($"{nameof(IBazService)} is not registered.");
            }

            var bazService = IocManager.Instance.Resolve<IBazService>();
            bazService.DoNothing();

            //Call at the end of app
            IocManager.Instance.Dispose();
        }
    }
}