namespace FdkImport
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using FdkImport.Engine;

    class Program
    {
        #region Console Functions

        private const int ATTACH_PARENT_PROCESS = -1;

        [DllImport("kernel32.dll")]
        static extern int AttachConsole(int dwProcessId);
        [DllImport("kernel32.dll")]
        static extern int FreeConsole();

        #endregion

        #region Console Mode

        static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\tFdkImport <intput path> [<class name>] [<namespace name>]");
        }

        static bool TryRunAsHelp(string[] args)
        {
            if (1 != args.Length)
            {
                return false;
            }
            var arg = args[0];
            var result = (("/?" == arg) || ("-?" == arg));
            return result;
        }

        static bool TryRunAsConvert(string[] args)
        {
            if (args.Length > 3)
            {
                return false;
            }

            var intputPath = args[0];
            var outputPath = Path.ChangeExtension(intputPath, "cs");
            var className = (args.Length > 1) ? args[1] : "MyAdviser";
            var namespaceName = (args.Length > 2) ? args[2] : "MyNamespace";

            var converter = new Converter();
            converter.Process(namespaceName, className, intputPath, outputPath);

            return true;
        }

        static void RunInConsoleMode(string[] args)
        {
            AttachConsole(ATTACH_PARENT_PROCESS);
            try
            {
                if (TryRunAsHelp(args))
                {
                    PrintUsage();
                }
                else if (!TryRunAsConvert(args))
                {
                    PrintUsage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            FreeConsole();
        }

        #endregion

        #region Windows mode

        static void RunInWindowsMode()
        {
            Application.EnableVisualStyles();
            var form = new MainForm();
            Application.Run(form);
        }

        #endregion

        static void Main(string[] args)
        {
            if (args.Length > 0)
                RunInConsoleMode(args);
            else
                RunInWindowsMode();
        }
    }
}
