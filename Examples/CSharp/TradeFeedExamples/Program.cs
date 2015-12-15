namespace TradeFeedExamples
{
    using SoftFX.Extended;

    class Program
    {
        static void Main(string[] args)
        {
            // Bootstrap FDK libraries
            Bootstrapper.Initialize();

            var address = "tpdemo.fxopen.com";
            var username = "81000001";
            var password = "F9J76sPk";

            var example = new StateCalculatorExample(address, username, password);

            using (example)
            {
                example.Run();
            }
        }
    }
}
