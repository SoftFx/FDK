namespace TradeFeedExamples
{
    class Program
    {
        static void Main(string[] args)
        {
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
