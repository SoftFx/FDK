namespace SoftFX.AutomaticTrading.Hosting
{
    using System;

    public interface IHostContext
    {
        IServiceProvider ServiceProvider { get; }
    }
}
