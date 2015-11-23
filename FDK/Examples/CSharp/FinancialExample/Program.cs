namespace FinancialExample
{
    using System;
    using System.Windows.Forms;
    using SoftFX.Extended;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Library.Path = "<FRE>";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
