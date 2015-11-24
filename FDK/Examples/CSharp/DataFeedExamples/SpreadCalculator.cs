namespace DataFeedExamples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SoftFX.Extended.Storage.Sequences;

    class SpreadCalculator : Example
    {
        public SpreadCalculator(string address, string username, string password)
            : base(address, username, password)
        {
        }

        protected override void RunExample()
        {
            this.Feed.SynchOperationTimeout = 60000;

            var startTime = new DateTime(2013, 7, 26);
            var endTime = new DateTime(2013, 7, 30);

            var etalonSymbol = "EURUSD";
            var etalonSpread = 100;
            var calcSpreadForSymbols = "AUDCHF AUDJPY AUDNZD AUDUSD CADCHF CADJPY CHFJPY EURAUD EURCAD EURCHF EURGBP EURJPY EURNOK EURSEK GBPAUD GBPCAD GBPCHF GBPJPY GBPNZD GBPUSD NZDCAD NZDCHF NZDJPY NZDUSD USDCAD USDCHF USDJPY USDSEK USDNOK EURNZD AUDCAD XAGUSD XAUUSD USDDKK USDHKD USDMXN USDPLN USDSGD USDTRY EURDKK EURHKD EURPLN EURTRY GBPSGD HKDJPY NOKJPY NOKSEK NZDSGD SGDJPY USDRUB".Split(' ').ToArray();

            this.InitializeFactors();
            var mathSequenceStat = this.CalculateSpread(etalonSymbol, startTime, endTime);
            var sigmaReliability = (etalonSpread - mathSequenceStat.Spread) / mathSequenceStat.Sigma;


            var slistStat = new SortedList<string, MathSequenceStat>();
            Console.WriteLine("Symbol={0}; {1}; sigmaReliability={2}", etalonSymbol, mathSequenceStat, sigmaReliability);

            foreach (var currSymbol in calcSpreadForSymbols)
            {
                var currMathSequenceStat = this.CalculateSpread(currSymbol, startTime, endTime);
                slistStat.Add(currSymbol, currMathSequenceStat);
                Console.WriteLine("Symbol={0}; {1}; spreadThreshold={2}", currSymbol, currMathSequenceStat, sigmaReliability * currMathSequenceStat.Sigma + currMathSequenceStat.Spread);
            }

            foreach (var item in slistStat)
            {
                Console.Write("{0}={1} ", item.Key, PipRound((int)(sigmaReliability * item.Value.Sigma + item.Value.Spread)));
            }

            Console.WriteLine();
        }
                
        MathSequenceStat CalculateSpread(string symbol, DateTime startTime, DateTime endTime)
        {
            do
            {
                try
                {
                    var sequence = new QuotesRangeSingleSequence(this.Storage.Online, symbol, startTime, endTime, 1, 2);
                    var timeInterval = (endTime - startTime).TotalSeconds;
                    var totalDuration = 0D;
                    var sumOfSpreads = 0D;
                    var sumOfSpreadsSquares = 0D;
                    var countTick = 0;

                    foreach (var currQuoteRange in sequence)
                    {
                        var currentQuote = currQuoteRange[0];
                        var nextQuote = currQuoteRange[1];

                        var spread = currentQuote.Spread;
                        var spreadInPips = this.PipsFromPrice(symbol, spread);
                        var duration = (nextQuote.CreatingTime - currentQuote.CreatingTime).TotalSeconds;
                        if (duration > 3600)
                            continue;
                        totalDuration += duration;
                        var weightCoefficient = duration / timeInterval;
                        sumOfSpreads += weightCoefficient * spread;
                        sumOfSpreadsSquares += weightCoefficient * spread * spread;

                        countTick++;

                        if (currentQuote.CreatingTime.Day != nextQuote.CreatingTime.Day)
                            Console.WriteLine("Now: {3};Symbol: {2};ProcessingTime: {0}; Ticks {1}", nextQuote.CreatingTime.ToString(), countTick, symbol, DateTime.Now);
                    }

                    // we need correction, because initial time interval can be different than actual time interval
                    var correctionCoefficient = timeInterval / totalDuration;
                    sumOfSpreads *= correctionCoefficient;
                    sumOfSpreadsSquares *= correctionCoefficient;

                    var sigma = sumOfSpreadsSquares - sumOfSpreads * sumOfSpreads;
                    if (sigma > 0)
                        sigma = Math.Sqrt(sigma);
                    else
                        sigma = 0;

                    return new MathSequenceStat(this.PipsFromPrice(symbol, sumOfSpreads), this.PipsFromPrice(symbol, sigma), countTick);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Trying again");
                }
            } while (true);
        }

        public struct MathSequenceStat
        {
            public double Spread;
            public double Sigma;
            public int Count;

            public MathSequenceStat(double spread, double sigma, int count)
                : this()
            {
                this.Spread = spread;
                this.Sigma = sigma;
                this.Count = count;
            }

            public override string ToString()
            {
                return string.Format("Spread={0}; Sigma={1}; TicksCount={2}",  this.Spread, this.Sigma, this.Count);
            }
        }

        #region Pip Helper

        void InitializeFactors()
        {
            var symbols = this.Feed.Server.GetSymbols();
            foreach (var element in symbols)
            {
                var factor = Math.Pow(10, element.Precision);
                this.m_symbolToFactor[element.Name] = factor;
            }
        }

        double PipsFromPrice(string symbol, double price)
        {
            var factor = this.m_symbolToFactor[symbol];
            var result = price * factor;
            return result;
        }

        static int PipRound(int r)
        {
            var divider = 1000;
            if (r < 1000)
                divider = 100;
            if (r <= 100)
                divider = 10;
            return (r / divider) * divider + divider;
        }

        Dictionary<string, double> m_symbolToFactor = new Dictionary<string, double>();

        #endregion
    }
}
