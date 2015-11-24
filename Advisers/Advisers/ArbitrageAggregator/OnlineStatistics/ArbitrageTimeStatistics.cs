using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbitrageAggregator
{
    public class ArbitrageTimeStatistics
    {
        #region MultiSingleton
        static object o = new object();
        static Dictionary<string, ArbitrageTimeStatistics> _dictInstance = new Dictionary<string, ArbitrageTimeStatistics>();

        public static ArbitrageTimeStatistics Instance (string symbol)
        {
            ArbitrageTimeStatistics value = null;
            if (!_dictInstance.TryGetValue(symbol, out value))
            {
                lock (o)
                {

                    if (!_dictInstance.TryGetValue(symbol, out value))
                    {
                        _dictInstance.Add(symbol, new ArbitrageTimeStatistics(symbol));
                        value = _dictInstance[symbol];
                    }
                }
            }
            return value;
        }
        #endregion

        static public DateTime StartAppDateTime { get; private set; }
        static ArbitrageTimeStatistics()
        {
            StartAppDateTime = DateTime.UtcNow;
        }
        string Symbol;
        private ArbitrageTimeStatistics(string symbol)
        {
            this.Symbol = symbol;
        }

        public DateTime StartArbitrageDateTime = DateTime.MinValue;
        public double TotalSecondsArbitradge = 0;


    }
}
