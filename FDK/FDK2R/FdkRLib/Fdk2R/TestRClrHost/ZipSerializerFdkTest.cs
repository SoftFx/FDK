/*
 * Created by SharpDevelop.
 * User: ciprian.khlud
 * Date: 6/5/2015
 * Time: 3:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TickTrader.BusinessObjects.QuoteHistory;
using TickTrader.Common.Business;
using TickTrader.Common.Proto;
using TickTrader.Server.QuoteHistory.Serialization;

namespace TestRClrHost
{
	[TestFixture]
	public class ZipSerializerFdkTest
	{
		[Test]
		public void TestPerf()
		{

            var serializer = new ItemsZipSerializer<HistoryBar, List<HistoryBar>>(BarFormatter.Instance, "M1 bid");
		    var bytes =
		        File.ReadAllBytes(
		            @"d:\Work\FDK\trunk\Concepts\Fdk2R\FdkGit\trunk\Fdk2R\TestRClrHost\bin\Storage\EURUSD\2014\03\02\M1 bid.zip");
		    Crc32Hash hash;
            var bars = serializer.Deserialize(bytes, out hash);
		    var bytesProto = bars.SerializeInstanceProto();
		}
	}
}
