namespace QuotesDownloader
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
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new QuotesDownloader());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "QuotesDownloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
