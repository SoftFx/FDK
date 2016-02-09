using SoftFX.Extended;

namespace TradeFeedExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "localhost";
            string username = "5";
            string password = "123qwe!";

            Library.Path = @"Y:\TickTrader\FDK\FRE";

            var example = new StateCalculatorExample(address, username, password);

            using (example)
            {
                example.Run();
            }
        }
    }
}
