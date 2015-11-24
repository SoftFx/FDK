using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.C;
using SoftFX.Extended.Storage;

namespace ArbitrageAggregator.Saver
{
    internal class BankSaver
    {
        int Capacity = 100000;
        Object obj = new object();
        Dictionary<string, Queue<Level2>> dictSymbolLevel2 = new Dictionary<string, Queue<Level2>>();

        public void AddQuote(Level2 level2)
        {
            Queue<Level2> queueLevel2;
            lock(obj)
            {
                if (!dictSymbolLevel2.TryGetValue(level2.Symbol, out queueLevel2))
                {
                    queueLevel2 = new Queue<Level2>(100000);
                    dictSymbolLevel2.Add(level2.Symbol, queueLevel2);
                }
                queueLevel2.Enqueue(level2);
            }
        }

        public IEnumerable<string> GetAllSymbols()
        {
            lock (obj)
            {
                return dictSymbolLevel2.Keys.ToList();
            }
        }
        public Queue<Level2> DequeueQuotesBySymbol(string symbol)
        {
            Queue<Level2> queueLevel2;

            lock (obj)
            {
                if (!dictSymbolLevel2.TryGetValue(symbol, out queueLevel2))
                    queueLevel2 = new Queue<Level2>();
                dictSymbolLevel2[symbol] = new Queue<Level2>(Capacity);
            }
            return queueLevel2;

        }

    }
}
