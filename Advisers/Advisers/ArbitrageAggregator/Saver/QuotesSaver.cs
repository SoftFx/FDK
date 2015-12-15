using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.C;
using SoftFX.Extended.Storage;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using SoftFX.Extended;

namespace ArbitrageAggregator.Saver
{

    public static class Level2Extender
    {
        public static Quote ToQuote(this Level2 level2)
        {
            return new Quote(level2.Symbol, DateTime.UtcNow, level2.Bid, level2.Ask);
        }
    }

    static class IEnumerableExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }

    internal class QuotesSaver : MultiAdviser<int>
    {
        //SortedList<int, BankSaver> listCodeToBankSaver = new SortedList<int, BankSaver>();
        List<string> listSymbols = new List<string>();
        Dictionary<string, DataFeedStorage> dictKeyToStorage = new Dictionary<string, DataFeedStorage>();
        object obj = new object();

        protected override void OnInitialize()
        {
            Settings s = Settings.Default;
            char[] chs = new char[] { ';' };
            string[] securities = s.Securities.Split(chs, StringSplitOptions.RemoveEmptyEntries);
            foreach (string symbol in securities)
            {
                listSymbols.Add(symbol);
            }
            
            Assembly assembly = Assembly.GetEntryAssembly();
			string root = assembly.Location;
			root = Path.GetDirectoryName(root);
			string storagePath = Path.Combine(root, "Storage");
			Directory.CreateDirectory(storagePath);

            foreach (int code in this)
            {
                string bankCodePath = Path.Combine( storagePath, code.ToString());
			    Directory.CreateDirectory(bankCodePath);

                foreach (string currSymbol in listSymbols)
                {
                    dictKeyToStorage.Add(GetKey(code, currSymbol), new DataFeedStorage(bankCodePath));
                }
                //listCodeToBankSaver.Add(code, new BankSaver());

            }
        }

        Task LastTask = Task.Factory.StartNew(() => { });

        protected override void OnUpdate(SingleAdviser<int> adviser)
        {
            lock (obj)
            {

                foreach (string currSymbol in listSymbols)
                {
                    Level2 level2 = adviser.GetLevel2(currSymbol);
                    if (!level2.Exist || level2.HasBeenChanged)
                        continue;

                    LastTask = LastTask.ContinueWith(ant =>
                        {
                            DataFeedStorage dfs = dictKeyToStorage[GetKey(adviser.Tag, currSymbol)];
                            dfs.Import(level2.ToQuote().ToEnumerable(), false, true, true);
                        }
                    );
                    //listCodeToBankSaver[adviser.Tag].AddQuote(level2);
                }
            }
        }

        private string GetKey(int bankCode, string symbol)
        {
            return bankCode.ToString() + symbol;
        }

        //private void Flush(object state)
        //{
        //    foreach (var currKeyValue in listCodeToBankSaver)
        //    {
        //        int code = currKeyValue.Key;
        //        BankSaver bankSaver = currKeyValue.Value;

                
        //    }

        //    FlushTimer.Change(FlushPeriod, 0);
        //}

    }
}
