namespace DataFeedExamples
{
    using System;
    using System.Collections.Generic;

    class StorageUpdatingExample : Example
    {
        public StorageUpdatingExample(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            var symbols = this.Feed.Server.GetSymbols();
            Console.WriteLine("symbols.Length = {0}", symbols.Length);

            var list = new List<string>();
            foreach (var element in symbols)
            {
                list.Add(element.Name);
            }

            this.Storage.Bind(this.Feed);
            this.Feed.Server.SubscribeToQuotes(list, 1);

            Console.ReadKey();
        }
    }
}
