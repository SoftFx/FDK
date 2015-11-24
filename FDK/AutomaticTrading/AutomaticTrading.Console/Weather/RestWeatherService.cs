namespace AutomaticTrading.Console.Weather
{
    using System.Net.Http;
    using System.Threading.Tasks;

    abstract class RestWeatherService : IWeatherService
    {
        readonly HttpClient client;

        protected RestWeatherService()
        {
            this.client = new HttpClient();
        }

        public double Temperature
        {
            get { return this.GetTemperatureAsync().Result; }
        }

        async Task<double> GetTemperatureAsync()
        {
            var responseBody = await this.client.GetStringAsync(this.Uri);

            return this.GetTemperatureFromJson(responseBody);
        }

        protected abstract double GetTemperatureFromJson(string json);

        protected abstract string Uri { get; }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
