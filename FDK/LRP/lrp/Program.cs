namespace Lrp
{
    using System;
    using Lrp.Engine;
    using Lrp.Configuration;

    class Program
    {
        static bool ShouldPrintHelpOnly(string[] args)
        {
            if (args == null)
                return true;

            if (args.Length == 0)
                return true;

            if (args.Length == 1)
            {
                var text = args[0];
                return text == "/help" || text == "-help" || text == "--help" || text == "/?" || text == "-?" || text == "--?";
            }

            return false;
        }

        static int Main(string[] args)
        {
            try
            {
                if (ShouldPrintHelpOnly(args))
                {
                    Parameters.PrintUsage();
                }
                else
                {
                    var parameters = new Parameters(args);
                    if (parameters.Operation == Operation.Generation)
                        Generate(parameters);
                    else if (parameters.Operation == Operation.Verification)
                        return Verify(parameters);
                    else
                        Parameters.PrintUsage();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        static void Generate(Parameters parameters)
        {
            if (parameters.Language == Language.CSharp)
                CSharpGenerator.Generate(parameters);
            else if (parameters.Language == Language.Cpp)
                CppGenerator.Generate(parameters);
            else
                Console.WriteLine("Unknown language = {0}", parameters.Language);
        }

        static int Verify(Parameters parameters)
        {
            var oldProtocol = new Protocol(parameters.Old);
            var newProtocol = new Protocol(parameters.New);
            if (oldProtocol.Compare(newProtocol))
                return 0;

            return 1;
        }
    }
}
