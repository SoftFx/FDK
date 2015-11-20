namespace Lrp.Engine
{
    using System;
    using System.Xml;
    using Lrp.Engine.Formating;

    class Component : ListSet<Method>, INamed
    {
        #region Properties

        [XmlSerializable()]
        public string Name { get; private set; }

        [XmlSerializable(false)]
        public Modifier Modifier { get; private set; }

        [XmlSerializable(false)]
        public bool Proxy { get; private set; }

        public int Id { get; private set; }

        public string RawName
        {
            get
            {
                if (this.Proxy)
                {
                    return this.Name + "Raw";
                }

                return this.Name;
            }
        }

        public string ProxyName
        {
            get
            {
                return this.Name + "Proxy";
            }
        }

        #endregion

        public Component(int id, DataTypes types, XmlNode node)
        {
            this.Modifier = Modifier.Internal;
            this.Id = id;
            XmlSerializer.Initialize(this, node);
            var nodes = node.ChildNodes;
            var count = nodes.Count;
            for (var index = 0; index < count; ++index)
            {
                var childNode = nodes[index];
                var method = new Method(this.Count, types, childNode);
                this.Add(method);
            }
        }

        #region verification

        internal bool Compare(Component other)
        {
            bool result = true;
            foreach (var element in this)
            {
                if (!Compare(element, other))
                {
                    result = false;
                }
            }
            return result;
        }

        bool Compare(Method method, Component other)
        {
            foreach (var element in other)
            {
                if (method.Name == element.Name)
                {
                    if (method.Compare(element))
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Method {0} of component {1} has incorrect signature", element.Name, this.Name);
                        return false;
                    }
                }
            }
            Console.WriteLine("Method {0} of component {1} does not exist", method.Name, this.Name);
            return false;
        }

        #endregion

        #region csharp
        #region binary
        public void GenerateCharpClient(Boolean isLocal, string namespaceName, string outputDirectory)
        {
            using (LrpStream stream = new LrpStream(outputDirectory, this.Name + ".cs"))
            {
                GenerateCharp(isLocal, stream, namespaceName);
                if (this.Proxy)
                {
                    GenerateCharpProxy(isLocal, stream, namespaceName);
                }
            }
        }
        public void GenerateCSharpServer(LrpStream stream)
        {
            stream.WriteLine("#region handlers of {0} component", this.Name);
            foreach (var element in this)
            {
                element.GenerateCSharpServer(stream, this);
            }
            stream.WriteLine();
            stream.WriteLine("private static readonly MethodHandler<Channel>[] g{0}Handlers = new MethodHandler<Channel>[]", this.Name);
            stream.WriteLine("{");
            foreach (var element in this)
            {
                stream.WriteLine("LrpInvoke_{0}_{1},", this.Name, element.Name);
            }
            stream.WriteLine("};");
            stream.WriteLine();
            stream.WriteLine("private static int LrpInvoke_{0}(int offset, int methodId, MemoryBuffer buffer, Channel channel)", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("if((methodId < 0) || (methodId >= {0}))", this.Count);
            stream.WriteLine("{");
            stream.WriteLine("return MagicNumbers.LRP_INVALID_METHOD_ID;");
            stream.WriteLine("}");
            stream.WriteLine("g{0}Handlers[methodId](offset, buffer, channel);", this.Name);
            stream.WriteLine("return MagicNumbers.S_OK;");
            stream.WriteLine("}");


            stream.WriteLine();
            stream.WriteLine("#endregion");
            stream.WriteLine();
        }
        public void GenerateSignature(LrpStream stream, String suffix)
        {
            stream.WriteLine("\"${0};\"{1}", this.Name, suffix);
            foreach (var element in this)
            {
                element.GenerateSignature(stream, suffix);
            }
        }
        private void GenerateCharp(Boolean isLocal, LrpStream stream, string namespaceName)
        {
            stream.WriteLine("using SoftFX.Lrp;");
            stream.WriteLine();
            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("namespace {0}", namespaceName);
                stream.WriteLine("{");
                GenerateCharp(stream);
                stream.WriteLine("}");
            }
            else
            {
                GenerateCharp(stream);
            }
        }
        private void GenerateCharpProxy(Boolean isLocal, LrpStream stream, string namespaceName)
        {
            stream.WriteLine();
            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("namespace {0}", namespaceName);
                stream.WriteLine("{");
                GenerateCharpProxy(isLocal, stream);
                stream.WriteLine("}");
            }
            else
            {
                GenerateCharpProxy(isLocal, stream);
            }
        }
        private void GenerateCharp(LrpStream stream)
        {
            string modifier = (Modifier.Public == this.Modifier) ? "public" : "internal";

            stream.WriteLine("{0} class {1}", modifier, this.RawName);

            stream.WriteLine("{");

            stream.WriteLine("private readonly IClient m_client;");
            stream.WriteLine("public {0}(IClient client)", this.RawName);

            stream.WriteLine("{");
            stream.WriteLine("if(null == client)");
            stream.WriteLine("{");
            stream.WriteLine("throw new System.ArgumentNullException(\"client\", \"Client argument can not be null\");");
            stream.WriteLine("}");
            stream.WriteLine("m_client = client;");

            stream.WriteLine("}");
            stream.WriteLine("public bool IsSupported");
            stream.WriteLine("{");
            stream.WriteLine("get");
            stream.WriteLine("{");
            stream.WriteLine("return m_client.IsSupported({0});", this.Id);
            stream.WriteLine("}");
            stream.WriteLine("}");

            foreach (var element in this)
            {
                element.GenerateCharp(this.Id, stream);
            }
            stream.WriteLine("}");
        }
        private void GenerateCharpProxy(Boolean isLocal, LrpStream stream)
        {
            string modifier = (Modifier.Public == this.Modifier) ? "public" : "internal";
            stream.WriteLine("{0} class {1} : System.IDisposable", modifier, this.ProxyName);
            stream.WriteLine("{");
            stream.WriteLine("public {0} Instance {{ get; private set; }}", this.RawName);
            if (isLocal)
            {
                stream.WriteLine("public LPtr Handle { get; private set; }");
            }
            else
            {
                stream.WriteLine("public RPtr Handle { get; private set; }");
            }
            stream.WriteLine("public bool IsSupported");
            stream.WriteLine("{");
            stream.WriteLine("get");
            stream.WriteLine("{");
            stream.WriteLine("return this.Instance.IsSupported;");
            stream.WriteLine("}");
            stream.WriteLine("}");
            if (isLocal)
            {
                stream.WriteLine("{0} {1}(LocalClient client, LPtr handle)", modifier, this.ProxyName);
                stream.WriteLine("{");
                stream.WriteLine("this.Instance = new {0}(client);", this.RawName);
                stream.WriteLine("this.Handle = handle;");
                stream.WriteLine("}");
            }

            foreach (var element in this)
            {
                element.GenerateCharpProxy(this, isLocal, stream);
            }

            stream.WriteLine("}");
        }
        #endregion
        #region database
        public void GenerateCSharpReader(LrpStream stream)
        {
            stream.WriteLine("public class {0} : BaseComponentReader", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("public {0}()", this.Name);
            stream.WriteLine("{");
            foreach(var element in this)
            {
                stream.WriteLine("base.Add(\"{0}\", Raise{0});", element.Name);
            }
            stream.WriteLine("}");
            foreach (var element in this)
            {
                element.GenerateCSharpReader(stream);
            }
            stream.WriteLine("}");
        }
        #endregion
        #endregion

        #region cpp local server
        public void GenerateCppServer(LrpStream stream)
        {
            stream.WriteLine();
            stream.WriteLine("// handlers of {0} component", this.Name);
            stream.WriteLine("namespace");
            stream.WriteLine("{");
            stream.WriteLine("const unsigned short LrpComponent_{0}_Id = {1};", this.Name, this.Id);
            foreach (var element in this)
            {
                stream.WriteLine("const unsigned short LrpMethod_{0}_{1}_Id = {2};", this.Name, element.Name, element.Id);
            }
            stream.WriteLine();
            stream.WriteLine("typedef void (*LrpInvoke_{0}_Method_Handler)(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel);", this.Name);
            foreach (var element in this)
            {
                element.GenerateCppServer(stream, this);
            }
            stream.WriteLine();
            stream.WriteLine("LrpInvoke_{0}_Method_Handler g{0}Handlers[] = ", this.Name);
            stream.WriteLine("{");
            foreach (var element in this)
            {
                stream.WriteLine("LrpInvoke_{0}_{1},", this.Name, element.Name);
            }
            stream.WriteLine("};");
            stream.WriteLine();
            stream.WriteLine("HRESULT LrpInvoke_{0}(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)", this.Name);
                
            stream.WriteLine("{");
            stream.WriteLine("if(methodId >= {0})", this.Count);
            stream.WriteLine("{");
            stream.WriteLine("return LRP_INVALID_METHOD_ID;");
            stream.WriteLine("}");
            stream.WriteLine("g{0}Handlers[methodId](offset, buffer, pChannel); ", this.Name);

            stream.WriteLine("return S_OK;");
            stream.WriteLine("}");
            stream.WriteLine("}");
            stream.WriteLine();
        }
        public void GenerateCppHeaderWriter(LrpStream stream)
        {
            stream.WriteLine("class {0}", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("private:");
            stream.WriteLine("ILrpTextStream* m_stream;");
            stream.WriteLine("public:");
            stream.WriteLine("{0}(ILrpTextStream* pStream);", this.Name);
            stream.WriteLine("public:");
            foreach (var element in this)
            {
                element.GenerateCppHeaderWriter(stream);
            }
            stream.WriteLine("};");
            stream.WriteLine();
        }
        public void GenerateCppSourceWriter(LrpStream stream)
        {
            stream.WriteLine("{0}::{0}(ILrpTextStream* pStream) : m_stream(pStream)", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("}");
            foreach (var element in this)
            {
                element.GenerateCppSourceWriter(stream, this);
            }
        }
        #endregion

        #region cpp remote client
        public void GenerateCppRemoteClient(string outputDirectory)
        {
            using (LrpStream stream = new LrpStream(outputDirectory, this.Name + ".hpp"))
            {
                GenerateCppRemoteClient(stream);
            }
        }
        public void GenerateCppLocalClient(string outputDirectory)
        {
            using (LrpStream stream = new LrpStream(outputDirectory, this.Name + ".hpp"))
            {
                GenerateCppLocalClient(stream);
            }
        }
        private void GenerateCppRemoteClient(LrpStream stream)
        {
            stream.WriteLine("class {0}", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("private:");
            stream.WriteLine("ILrpChannel* m_channel;");
            stream.WriteLine("public:");


            stream.WriteLine("inline {0}(ILrpChannel& channel) : m_channel(&channel)", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("}");
            stream.WriteLine("const static unsigned short LrpComponentId = {0};", this.Id);
            stream.WriteLine("bool IsSupported() const");
            stream.WriteLine("{");
            stream.WriteLine("return m_channel->IsSupported({0});", this.Id);
            stream.WriteLine("}");
            foreach (var element in this)
            {
                element.GenerateCpp(false, this.Id, stream);
            }
            stream.WriteLine("};");
        }
        private void GenerateCppLocalClient(LrpStream stream)
        {
            stream.WriteLine("class {0}", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("private:");
            stream.WriteLine("CLrpLocalClient* m_channel;");
            stream.WriteLine("public:");

            stream.WriteLine("inline {0}() : m_channel()", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("}");
            stream.WriteLine("inline {0}(CLrpLocalClient& client) : m_channel(&client)", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("}");
            stream.WriteLine("bool IsSupported() const");
            stream.WriteLine("{");
            stream.WriteLine("return m_channel->IsSupported({0});", this.Id);
            stream.WriteLine("}");
            foreach (var element in this)
            {
                element.GenerateCpp(true, this.Id, stream);
            }
            stream.WriteLine("};");
        }
        #endregion
    }
}

