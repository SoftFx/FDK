namespace DataClientExamples
{
    using System;
    using SoftFX.Extended;

    class TestFeedConnectionExample
    {
        public static void Run()
        {
            var buidlers = ConnectionStringBuilder.TestFeedConnections("tpdemo.fxopen.com", "59932", "8mEx7zZ2");
            //var builders = FixConnectionStringBuilder.TestFeedConnections("tpdemo.fxopen.com", "59932", "8mEx7zZ2");;
            Console.WriteLine("buildes.Length = {0}", buidlers.Length);
        }
    }
}
