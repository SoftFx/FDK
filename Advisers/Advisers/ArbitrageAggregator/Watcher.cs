using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftFX.C;
using System.Threading.Tasks;
using Model;
using System.Threading;

namespace ArbitrageAggregator
{
	internal class Watcher : MultiAdviser<int>
	{
        //private SingleAdviser<int> m_first;
        //private SingleAdviser<int> m_second;
        private List<SingleAdviser<int>> m_listAdviser = new List<SingleAdviser<int>>();
        private SortedList<int, int> quotesCounter;
        private HashSet<WatcherSymbol> m_watchers = new HashSet<WatcherSymbol>();
        private object m_synchronizer = new object();
        System.Threading.Timer timer;

		public Watcher()
		{
			Settings s = Settings.Default;
			char[] chs = new char[] { ';' };
			string[] securities = s.Securities.Split(chs, StringSplitOptions.RemoveEmptyEntries);
			foreach (var element in securities)
			{
				WatcherSymbol watcher = new WatcherSymbol(element);
				m_watchers.Add(watcher);
			}
		}
		protected override void OnInitialize()
		{
			foreach (var element in this)
                m_listAdviser.Add(this[element]);
            
            this.quotesCounter = InitializeCounter();
            timer = new System.Threading.Timer(WriteStatusTimerCallback, null, 1000, 0);
		}

        protected override void OnUpdate(SingleAdviser<int> adviser)
		{
			lock (m_synchronizer)
			{
                quotesCounter[adviser.Tag]++;
                for( int i =0;i<m_listAdviser.Count;i++)
                //Array.For(0, m_listAdviser.Count, i =>
                    {
                        if (adviser.Tag != m_listAdviser[i].Tag)
                        {
                            Parallel.ForEach<WatcherSymbol>( m_watchers, currSymbolWatcher =>
                            {
                                currSymbolWatcher.Update(m_listAdviser[i], adviser);
                                currSymbolWatcher.Update(adviser, m_listAdviser[i]);
                            });
                        }
                    }
			}
		}

        #region TimerOutput
        protected SortedList<int, int> InitializeCounter()
        {
            quotesCounter = new SortedList<int, int>();
            for( int i =0;i<m_listAdviser.Count;i++)
                quotesCounter.Add(m_listAdviser[i].Tag, 0);
            return quotesCounter;
        }
        protected void WriteStatusTimerCallback(Object o)
        {
            SortedList<int, int> oldCounter;
            lock (m_synchronizer)
            {
                oldCounter = this.quotesCounter;
                InitializeCounter();
            }
            StringBuilder strOutput = new StringBuilder();
            foreach (int key in oldCounter.Keys)
            {
                strOutput.AppendFormat("{0}={1} ", key, oldCounter[key]);
            }
            Console.WriteLine("{0} {1}", DateTime.UtcNow.ToLongTimeString(), strOutput.ToString());
            timer.Change(60000, 0);
        }
        #endregion

	}
}
