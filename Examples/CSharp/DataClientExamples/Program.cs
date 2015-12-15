namespace DataClientExamples
{
    using SoftFX.Extended;

    class Program
    {
        static void Main(string[] args)
        {
            // Bootstrap FDK libraries
            Bootstrapper.Initialize();

            //TestConnectionExample.Run();
            //TestFeedConnectionExample.Run();
            TestTradeConnectionExample.Run();
        }
    }
}
