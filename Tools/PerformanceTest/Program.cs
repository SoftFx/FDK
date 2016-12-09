using SoftFX.Extended;
using System;
using System.IO;

namespace PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 3)
                {
                    if (args.Length == 0)
                    {
                        Console.WriteLine("Usage: <address> <username> <password>");
                        return;
                    }

                    throw new Exception("Invalid command line");
                }

                string address = args[0];
                string username = args[1];
                string password = args[2];

                Console.WriteLine("Benchmark Test");

                Test test = new Test(address, username, password);

                using (test)
                {
                    test.Start();
                    Console.ReadKey();
                    test.Stop();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: " + exception.Message);
            }
        }
    }
}
