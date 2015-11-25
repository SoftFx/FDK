namespace SoftFX.AutomaticTrading.Core.Strategies
{
    using System;

    public abstract class StrategyEvent
    {
        public abstract void Enable();

        public abstract void Disable();

        public EventHandler Executed;

        protected void RaiseExecuted()
        {
            var eh = this.Executed;
            if (eh != null)
            {
                eh(this, EventArgs.Empty);
            }
        }
    }
}
