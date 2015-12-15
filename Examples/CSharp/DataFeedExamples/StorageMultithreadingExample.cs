namespace DataFeedExamples
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using SoftFX.Extended.Storage.Sequences;

    class StorageMultithreadingExample : Example
    {
        public StorageMultithreadingExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            this.FirstExample();
            //this.SecondExample();
        }

        #region First Multithreading Example

        void FirstExample()
        {
            var startActivity = this.Feed.Network.GetLastSessionActivity();
            var startTime = DateTime.UtcNow;

            Console.WriteLine("Running first example");
            var symbols = this.Feed.Server.GetSymbols();
            Console.WriteLine("Number of symbols = {0}", symbols.Length);

            var threads = new List<Thread>(symbols.Length);

            foreach (var symbol in symbols)
            {
                var thread = new Thread(this.FirstThreadMethod);
                threads.Add(thread);
                thread.Start(symbol.Name);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("First example has been finished");

            var finishActivity = this.Feed.Network.GetLastSessionActivity();
            var finishTime = DateTime.UtcNow;

            double speed = finishActivity.DataBytesReceived - startActivity.DataBytesReceived;
            var interval = finishTime - startTime;
            speed /= interval.TotalSeconds;
            speed /= 1024;
            Console.WriteLine("speed = {0} in kbytes/second", speed);
        }

        void FirstThreadMethod(object obj)
        {
            var symbol = (string)obj;

            var startTime = DateTime.UtcNow.Date;
            var endTime = startTime.AddHours(1);

            var quotes = new QuotesSingleSequence(this.Storage.Online, symbol, startTime, endTime, 1);

            var averageSpread = 0D;
            var count = 0;
            foreach (var element in quotes)
            {
                averageSpread += element.Spread;
                count++;
            }

            if (count > 0)
            {
                averageSpread /= count;
            }
            else
            {
                averageSpread = double.NaN;
            }

            Console.WriteLine("Averge spread of {0} = {1}", symbol, averageSpread);
        }
        #endregion

        #region Second Multithreading Example

        void SecondExample()
        {
            Console.WriteLine("Running second example");

            var count = 3;
            var threads = new List<Thread>(count);

            this.now = DateTime.UtcNow;

            for (var index = 1; index <= count; ++index)
            {
                var thread = new Thread(this.SecondThreadMethod);
                thread.Start(this.now.AddHours(-index));
                threads.Add(thread);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Second example has been finished");
        }

        void SecondThreadMethod(object obj)
        {
            var symbol = "EURUSD";
            var from = (DateTime)obj;

            var to = this.now;

            var quotes = new QuotesSingleSequence(this.Storage.Online, symbol, from, to, 1);

            var averageSpread = 0D;
            var count = 0;

            foreach (var element in quotes)
            {
                averageSpread += element.Spread;
                count++;
            }

            if (count > 0)
            {
                averageSpread /= count;
            }
            else
            {
                averageSpread = double.NaN;
            }

            Console.WriteLine("Averge spread of {0} [{1}, {2}] = {3}", symbol, from, to, averageSpread);
        }

        #endregion

        #region Members

        DateTime now;

        #endregion
    }
}
