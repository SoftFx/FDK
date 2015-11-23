namespace DataClientExamples
{
    using SoftFX.Extended;

    class Program
    {
        static void Main(string[] args)
        {
            Library.Path = "<FRE>";
            //TestConnectionExample.Run();
            //TestFeedConnectionExample.Run();
            TestTradeConnectionExample.Run();
        }
    }
}
