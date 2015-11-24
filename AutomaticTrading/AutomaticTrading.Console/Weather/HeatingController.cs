namespace AutomaticTrading.Console.Weather
{
    class HeatingController
    {
        const double OnTemperature = 25D;

        public HeatingController()
        {
            this.Off();
        }

        public void On()
        {
            this.Temperature = OnTemperature;
            this.ControllerState = State.On;
        }

        public void Off()
        {
            this.Temperature = double.NaN;
            this.ControllerState = State.Off;
        }

        public double Temperature { get; private set; }

        public State ControllerState { get; private set; }

        public enum State
        {
            On,
            Off
        };
    }

}
