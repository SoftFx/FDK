using SoftFX.Extended;

namespace TradeFeedExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "localhost";
            string username = "11";
            string password = "123qwe!";

            var example = new StateCalculatorExample(address, username, password);

            using (example)
            {
                example.Run();
            }
        }
    }
}
