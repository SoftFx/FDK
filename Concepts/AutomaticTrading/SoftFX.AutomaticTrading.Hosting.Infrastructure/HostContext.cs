namespace SoftFX.AutomaticTrading.Hosting.Infrastructure
{
    using System;

    public sealed class HostContext : IHostContext
    {
        public HostContext()
            : this(NullServiceProvider.Instance)
        {
        }

        public HostContext(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException("serviceProvider");

            this.ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; private set; }

        class NullServiceProvider : IServiceProvider
        {
            public static readonly IServiceProvider Instance = new NullServiceProvider();

            NullServiceProvider()
            {
            }

            object IServiceProvider.GetService(Type serviceType)
            {
                return null;
            }
        }
    }
}
