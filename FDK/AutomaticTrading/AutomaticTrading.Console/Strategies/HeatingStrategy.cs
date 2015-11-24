namespace AutomaticTrading.Console.Strategies
{
    using AutomaticTrading.Console.Weather;
    using SoftFX.AutomaticTrading.Core.Strategies;

    class HeatingStrategy : Strategy<HeatingController>
    {
        public HeatingStrategy(HeatingController controller, StrategyEvent on, StrategyEvent off)
            : base(controller)
        {
            this.RegisterEvent(on);
            this.RegisterEvent(off);

            on.Executed += (o, e) => this.Change(true);
            off.Executed += (o, e) => this.Change(false);
        }

        void Change(bool on)
        {
            if (on)
            {
                if (this.State.ControllerState == HeatingController.State.Off)
                {
                    this.State.On();
                    this.Logger.Log("Heating turned on");
                }
            }
            else
            {
                if (this.State.ControllerState == HeatingController.State.On)
                {
                    this.State.Off();
                    this.Logger.Log("Heating turned off");
                }
            }
        }
    }
}
