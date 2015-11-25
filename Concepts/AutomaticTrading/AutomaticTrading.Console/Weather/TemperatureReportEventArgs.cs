namespace AutomaticTrading.Console.Weather
{
    using System;

    public class TemperatureReportEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
    }
}
