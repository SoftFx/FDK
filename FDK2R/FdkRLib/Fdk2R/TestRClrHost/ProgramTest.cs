/*
 * Created by SharpDevelop.
 * User: ciprian.khlud
 * Date: 7/20/2015
 * Time: 5:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using RHost;

namespace TestRClrHost
{
	/// <summary>
	/// Description of ProgramTest.
	/// </summary>
	public class ProgramTest
	{
		public static void Main()
		{
			FdkHelper.ConnectToFdk("localhost", "100001", "123qwe!", "");
            var bars = FdkTradeReports.GetTradeTransactionReportAll();
            var comission = FdkTradeReports.GetTradeComment(bars);
            FdkVars.Unregister(bars);
		}
	}
}
