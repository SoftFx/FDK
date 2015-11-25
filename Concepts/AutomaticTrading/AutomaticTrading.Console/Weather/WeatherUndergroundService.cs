namespace AutomaticTrading.Console.Weather
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text.RegularExpressions;

    class WeatherUndergroundService : RestWeatherService
    {
        const string UriTemplate = @"http://api.wunderground.com/api/{0}/conditions/q/{1}/{2}.json";
        readonly Regex regex;
        readonly string uri;

        public WeatherUndergroundService(string key, string country, string city)
        {
            this.regex = new Regex("\"temp_c\":(.+),", RegexOptions.Compiled);
            this.uri = string.Format(UriTemplate, key, country, city);
        }

        protected override string Uri
        {
            get { return this.uri; }
        }

        protected override double GetTemperatureFromJson(string json)
        {
            var match = regex.Match(json);
            try
            {
                var temperature = Convert.ToDouble(match.Groups[1].Value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                return temperature;
            }
            catch (FormatException)
            {
                Debug.WriteLine("FormatException");
                throw;
            }
        }
    }
}
