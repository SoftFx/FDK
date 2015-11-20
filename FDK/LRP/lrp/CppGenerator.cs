namespace Lrp
{
    using System;
    using Lrp.Configuration;
    using Lrp.Engine;

    static class CppGenerator
    {
        public static void Generate(Parameters parameters)
        {
            if (parameters.Side == Side.Client)
                GenerateClient(parameters);
            else if (parameters.Side == Side.Server)
                GenerateServer(parameters);
            else if (parameters.Side == Side.Writer)
                GenerateWriter(parameters);
            else if (parameters.Side == Side.Reader)
                GenerateReader(parameters);
            else
                Console.WriteLine("Not supported {0} side by C++ language", parameters.Side);
        }

        static void GenerateClient(Parameters parameters)
        {
            if (parameters.Mode == Mode.Local)
            {
                var protocol = new Protocol(parameters.Input);
                protocol.GenerateCppLocalClient(parameters.Output, parameters.Prefix);
            }
            else if (parameters.Mode == Mode.Remote)
            {
                var protocol = new Protocol(parameters.Input);
                protocol.GenerateCppRemoteClient(parameters.Output, parameters.Prefix);
            }
            else
            {
                Console.WriteLine("Not supported {0} mode by C++ client", parameters.Operation);
            }
        }

        static void GenerateServer(Parameters parameters)
        {
            if (parameters.Mode == Mode.Local)
            {
                var protocol = new Protocol(parameters.Input);
                protocol.GenerateCppLocalServer(parameters.Output, parameters.Prefix);
            }
            else if (parameters.Mode == Mode.Remote)
            {
                var protocol = new Protocol(parameters.Input);
                protocol.GenerateCppRemoteServer(parameters.Output, parameters.Prefix);
            }
            else
            {
                Console.WriteLine("Not supported {0} mode by C++ server", parameters.Operation);
            }
        }

        static void GenerateWriter(Parameters parameters)
        {
            var protocol = new Protocol(parameters.Input);
            protocol.GenerateCppWriter(parameters.Output);
            //XSLT.Generator.Generate(XSLT.Templates.CppWriterHeader, parameters.Input, parameters.Output + ".h");
            //XSLT.Generator.Generate(XSLT.Templates.CppWriterSource, parameters.Input, parameters.Output + ".hpp");
        }

        static void GenerateReader(Parameters parameters)
        {
            Console.WriteLine("Reader is not implemented for C++ language");
        }
    }
}
