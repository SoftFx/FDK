using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Arbitrage coldArb = null;
            List<Arbitrage> warmArbList = new List<Arbitrage>();

            for (int i = 0; i < 10000; i++)
            {
                Arbitrage newArb = new Arbitrage(DateTime.Now, 1, 2, "dfas");
                for (int j = 0; j < 10; j++)
                {
                    newArb.ArbitrageDetails.Add(new ArbitrageDetail(DateTime.Now));
                }
                warmArbList.Add(newArb);
                coldArb = newArb;
            }

            using (Model.ArbitrageContext arbContext = new Model.ArbitrageContext())
            {
                arbContext.Arbitrages.Add(coldArb);
                arbContext.SaveChanges();
            }

            Stopwatch sw = new Stopwatch();

            using (Model.ArbitrageContext arbContext = new Model.ArbitrageContext())
            {
                arbContext.Configuration.AutoDetectChangesEnabled = false;
                //arbContext.Configuration.ValidateOnSaveEnabled = false;

                sw.Start();

                foreach( var item in warmArbList )
                    arbContext.Arbitrages.Add(item);
                sw.Stop();
                Console.WriteLine("Time of adding 10000 elements : " + sw.ElapsedMilliseconds + " ms");

                sw.Reset();
                sw.Start();
                arbContext.SaveChanges();
                sw.Stop();
                Console.WriteLine("Time of saving 10000 elements : " + sw.ElapsedMilliseconds + " ms");
            }

        }
    }
}
