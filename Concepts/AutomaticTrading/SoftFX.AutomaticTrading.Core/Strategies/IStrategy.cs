namespace SoftFX.AutomaticTrading.Core.Strategies
{
    public interface IStrategy
    {
        void Start();

        void Stop();

        StrategyStatus Status { get; }
    }
}
