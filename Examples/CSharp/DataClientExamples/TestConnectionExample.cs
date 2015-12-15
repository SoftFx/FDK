namespace DataClientExamples
{
    using System;
    using SoftFX.Extended;

    class TestConnectionExample
    {
        public static void Run()
        {
            var buidlers = ConnectionStringBuilder.TestConnections("tpdemo.fxopen.com");
            //var builders = FixConnectionStringBuilder.TestConnections("tpdemo.fxopen.com");
            Console.WriteLine("buildes.Length = {0}", buidlers.Length);
        }
    }
}
