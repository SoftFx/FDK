using SoftFX.Extended;

namespace TradeFeedExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "tp.st.soft-fx.eu";
            string username = "101186";
            string password = "123qwe!";

            var example = new StateCalculatorExample(address, username, password);

            using (example)
            {
                example.Run();
            }
        }
    }
}
