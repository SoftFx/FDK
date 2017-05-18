using SoftFX.Extended;

namespace TradeFeedExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "tp.dev.soft-fx.eu";
            string username = "5";
            string password = "123qwe!";

            var example = new StateCalculatorExample(address, username, password);

            using (example)
            {
                example.Run();
            }
        }
    }
}
