/*
 * Created by SharpDevelop.
 * User: ciprian.khlud
 * Date: 7/20/2015
 * Time: 10:32 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using UserGroupSetup.Orm;



namespace UserGroupSetup
{
    class Program
	{
        public const string ConnectionMySql = "SERVER=localhost;DATABASE=dbuserimport;UID=root;PASSWORD=albatros;";
        public static void Main(string[] args)
		{
            var mtPrices = new mt4_prices();

            mtPrices.All(where: "ASK>1.001")
                .Select(dynIt => new { Ask = dynIt.ASK, })
                .Each(it =>
                {
                    Console.WriteLine(it.Ask);
                });
            foreach (var price in mtPrices.All(where:"ASK>1.001"))
            {
                Console.WriteLine(price.ASK);
            }


			RHost.FdkHelper.ConnectToFdk("","","","");

			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}