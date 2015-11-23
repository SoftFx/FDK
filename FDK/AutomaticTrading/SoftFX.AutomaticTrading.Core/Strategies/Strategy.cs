namespace SoftFX.AutomaticTrading.Core.Strategies
{
    using System.Collections.Generic;

    public class Strategy : IStrategy
    {
        readonly IList<StrategyEvent> events;

        public Strategy()
        {
            this.events = new List<StrategyEvent>();
            this.Status = StrategyStatus.Stopped;
            this.Logger = new DefaultLogger();
        }

        public void Start()
        {
            foreach (var e in this.events)
                e.Enable();

            this.Status = StrategyStatus.Running;
        }

        public void Stop()
        {
            foreach (var e in this.events)
                e.Disable();

            this.Status = StrategyStatus.Stopped;
        }

        public void RegisterEvent(StrategyEvent e)
        {
            this.events.Add(e);
        }

        public StrategyStatus Status { get; private set; }

        protected IStrategyLogger Logger { get; private set; }
    }

    public class Strategy<TState> : Strategy
    {
        public TState State { get; private set; }

        public Strategy(TState state)
        {
            this.State = state;
        }
    }
}
