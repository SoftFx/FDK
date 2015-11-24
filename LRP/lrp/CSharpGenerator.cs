using System;
using Lrp.Configuration;
using Lrp.Engine;

namespace Lrp
{
    static class CSharpGenerator
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
                Console.WriteLine("Not supported {0} side by C# language", parameters.Side);
        }

        static void GenerateClient(Parameters parameters)
        {
            if (parameters.Mode == Mode.Local)
            {
                var protocol = new Protocol(parameters.Input);
                protocol.GenerateCharpLocalClient(parameters.Namespace, parameters.Output);
            }
            else if (parameters.Mode == Mode.Remote)
            {
                var protocol = new Protocol(parameters.Input);
                protocol.GenerateCharpRemoteClient(parameters.Namespace, parameters.Output);
            }
            else
            {
                Console.WriteLine("Not supported {0} mode by C# client", parameters.Operation);
            }
        }

        static void GenerateServer(Parameters parameters)
        {
            if (parameters.Mode == Mode.Local)
            {
                var protocol = new Protocol(parameters.Input);
                protocol.GenerateCharpLocalServer(parameters.Namespace, parameters.Output);
            }
            else if (parameters.Mode == Mode.Remote)
            {
                Console.WriteLine("Remote C# server is not implemented");
            }
            else
            {
                Console.WriteLine("Not supported {0} mode by C# server", parameters.Operation);
            }
        }

        static void GenerateWriter(Parameters parameters)
        {
            Console.WriteLine("Writer is not implemented for C# language");
            
        }

        static void GenerateReader(Parameters parameters)
        {
            var protocol = new Protocol(parameters.Input);
            protocol.GenerateCSharpReader(protocol.Namespace, parameters.Output);
            //XSLT.Generator.Generate(XSLT.Templates.CSharpReader, parameters.Input, parameters.Output);
        }
    }
}
