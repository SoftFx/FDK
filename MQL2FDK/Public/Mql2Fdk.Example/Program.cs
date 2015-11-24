namespace Mql2Fdk.Example
{
    using System;
    using SoftFX.Extended;

    class Program
	{
		static void Main(string[] args)
		{
           MyAdviser adviser = new MyAdviser();

            //using (MqlLauncher launcher = new MqlLauncher
            //    (
            //    Settings.Default.Address,
            //    Settings.Default.Username,
            //    Settings.Default.Password,
            //    null,
            //    Settings.Default.Symbol,
            //    Settings.Default.PriceType,
            //    new BarPeriod(Settings.Default.Periodicity),
            //    adviser)
            //    )

            using (var launcher = new MqlLauncher(Settings.Default, adviser))
            {
                launcher.Start();
                Console.ReadKey();
                launcher.Stop();
            }

		}
	}
}
