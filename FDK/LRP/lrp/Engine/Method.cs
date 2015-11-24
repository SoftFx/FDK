namespace Lrp.Engine
{
    using System;
    using System.Text;
    using System.Xml;
    using Lrp.Engine.Formating;

    class Method : ListSet<Parameter>, INamed
    {
        [XmlSerializable]
        public string Name { get; private set; }

        public int Id { get; private set; }

        public Return Return { get; private set; }

        public Method(int id, DataTypes types, XmlNode node)
        {
            this.types = types;
            this.Id = id;
            XmlSerializer.Initialize(this, node);
            var nodes = node.ChildNodes;
            var count = nodes.Count;
            for (var index = 0; index < count; ++index)
            {
                var childNode = nodes[index];
                this.Process(types, childNode);
            }
            if (this.Return == null)
            {
                var message = string.Format("Method = {0} does not have return node", this.Name);
                throw new Exception(message);
            }
        }

        void Process(DataTypes types, XmlNode node)
        {
            if (node.Name == "Param")
            {
                var parameter = new Parameter(this.Count, types, node);
                this.Add(parameter);
            }
            else if (node.Name == "Return")
            {
                this.ProcessReturnValue(types, node);
            }
            else
            {
                var message = string.Format("Unsupported child node = {0} of component element.", node.Name);
                throw new Exception(message);
            }
        }

        void ProcessReturnValue(DataTypes types, XmlNode node)
        {
            if (this.Return != null)
            {
                var message = string.Format("Method = {0} has duplicate return node", this.Name);
                throw new Exception(message);
            }

            this.Return = new Return(types, node);
        }

        #region Verification

        internal bool Compare(Method other)
        {
            var hashThis = this.GenerateHash();
            var hashOther = other.GenerateHash();
            var result = hashThis == hashOther;
            return result;
        }

        #endregion

        #region CSharp

        #region Binary

        public void GenerateCSharpServer(LrpStream stream, Component component)
        {
            stream.WriteLine("private static void LrpInvoke_{0}_{1}(int offset, MemoryBuffer buffer, Channel channel)", component.Name, this.Name);
            stream.WriteLine("{");
            stream.WriteLine("var component = channel.{0};", component.Name);
            foreach (var element in this)
            {
                element.GenerateCSharpServerRequest(this.types, stream);
            }
            stream.WriteLine("buffer.Reset(offset);");


            stream.BeginLine();
            var type = this.Return.Type;

            if (type == EmbeddedDataTypes.Void)
            {
                stream.Write("component.{0}(", this.Name);
            }
            else
            {
                stream.Write("var result = component.{0}(", this.Name);
            }
            var count = this.Count;
            if (count > 0)
            {
                this[0].GenerateCSharpAgrgumentById(stream);
            }
            for (var index = 1; index < count; ++index)
            {
                stream.Write(", ");
                this[index].GenerateCSharpAgrgumentById(stream);
            }
            stream.Write(");");
            stream.EndLine();

            foreach (var element in this)
            {
                element.GenerateCSharpServerResponse(stream);
            }

            if (type != EmbeddedDataTypes.Void)
            {
                stream.WriteLine("buffer.Write{0}(result);", type.Suffix);
            }

            stream.WriteLine("}");
        }
        public void GenerateCharp(int componentId, LrpStream stream)
        {
            stream.WriteLine("public bool Is_{0}_Supported", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("get");
            stream.WriteLine("{");
            stream.WriteLine("return m_client.IsSupported({0}, {1});", componentId, this.Id);
            stream.WriteLine("}");
            stream.WriteLine("}");

            stream.BeginLine();
            stream.Write("public {0} {1}(", this.types.ToCSharpFullName(this.Return.Type), this.Name);

            var it = this.GetEnumerator();
            if (it.MoveNext())
            {
                it.Current.GenerateCSharpSignature(this.types, stream);
            }
            for (; it.MoveNext(); )
            {
                stream.Write(", ");
                it.Current.GenerateCSharpSignature(this.types, stream);
            }
            stream.Write(")");
            stream.EndLine();
            stream.WriteLine("{");
            stream.WriteLine("using(MemoryBuffer buffer = m_client.Create())");
            stream.WriteLine("{");
            GenerateCharpBody(componentId, stream);
            stream.WriteLine("}");
            stream.WriteLine("}");
        }

        bool IsProxyMethod
        {
            get
            {
                if (this.Count == 0)
                    return false;

                var parameter = this[0];
                return parameter.Type == EmbeddedDataTypes.RemotePointer || parameter.Type == EmbeddedDataTypes.LocalPointer;
            }
        }

        bool IsProxyConstructor
        {
            get
            {
                return this.Name.StartsWith("Constructor");
            }
        }

        bool IsProxyDestructor
        {
            get
            {
                return this.Name == "Destructor";
            }
        }

        void GenerateCharpIsSupportedProxy(LrpStream stream)
        {
            stream.WriteLine("public bool Is_{0}_Supported", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("get");
            stream.WriteLine("{");
            stream.WriteLine("return this.Instance.Is_{0}_Supported;", this.Name);
            stream.WriteLine("}");
            stream.WriteLine("}");
        }

        void GenerateCharpProxyDestructor(LrpStream stream)
        {
            this.GenerateCharpIsSupportedProxy(stream);
            stream.WriteLine("public void Dispose()");
            stream.WriteLine("{");
            stream.WriteLine("if(!this.Handle.IsZero)");
            stream.WriteLine("{");
            stream.WriteLine("this.Instance.Destructor(this.Handle);");
            stream.WriteLine("this.Handle.Clear();");
            stream.WriteLine("}");
            stream.WriteLine("}");
        }
        public void GenerateCharpProxy(Component component, bool isLocal, LrpStream stream)
        {
            if (this.IsProxyConstructor)
            {
                this.GenerateCharpProxyConstructor(component, isLocal, stream);
            }
            else if (this.IsProxyDestructor)
            {
                this.GenerateCharpProxyDestructor(stream);
            }
            else if (this.IsProxyMethod)
            {
                this.GenerateCharpProxyMethod(stream);
            }
        }

        void GenerateCharpProxyConstructor(Component component, bool isLocal, LrpStream stream)
        {
            this.GenerateCharpIsSupportedProxy(stream);
            stream.BeginLine();
            if (isLocal)
            {
                stream.Write("public {0}(LocalClient client", component.ProxyName);
            }
            else
            {
                stream.Write("public {0}(IRemoteClient client", component.ProxyName);
            }
            {
                var it = this.GetEnumerator();
                for (; it.MoveNext(); )
                {
                    stream.Write(", ");
                    it.Current.GenerateCSharpSignature(this.types, stream);
                }
            }
            stream.Write(")");
            stream.EndLine();
            stream.WriteLine("{");
            stream.WriteLine("this.Instance = new {0}(client);", component.RawName);
            stream.BeginLine();
            stream.Write("this.Handle = this.Instance.{0}(", this.Name);
            {
                var it = this.GetEnumerator();
                if (it.MoveNext())
                {
                    it.Current.GenerateCSharpArgumentByName(stream);
                }
                for (; it.MoveNext(); )
                {
                    stream.Write(", ");
                    it.Current.GenerateCSharpArgumentByName(stream);
                }
            }
            stream.Write(");");
            stream.EndLine();
            stream.WriteLine("}");
        }

        void GenerateCharpProxyMethod(LrpStream stream)
        {
            this.GenerateCharpIsSupportedProxy(stream);
            stream.BeginLine();
            stream.Write("public {0} {1}(", this.types.ToCSharpFullName(this.Return.Type), this.Name);

            {
                var it = this.GetEnumerator();
                it.MoveNext();
                if (it.MoveNext())
                {
                    it.Current.GenerateCSharpSignature(this.types, stream);
                }
                for (; it.MoveNext(); )
                {
                    stream.Write(", ");
                    it.Current.GenerateCSharpSignature(this.types, stream);
                }
            }
            stream.Write(")");
            stream.EndLine();
            stream.WriteLine("{");
            stream.BeginLine();
            stream.Write("");
            if (this.Return.Type != EmbeddedDataTypes.Void)
            {
                stream.Write("return ");
            }
            stream.Write("this.Instance.{0}(this.Handle", this.Name);
            {
                var it = this.GetEnumerator();
                it.MoveNext();
                for (; it.MoveNext(); )
                {
                    stream.Write(", ");
                    it.Current.GenerateCSharpArgumentByName(stream);
                }
            }
            stream.Write(");");
            stream.EndLine();
            stream.WriteLine("}");
        }

        public void GenerateSignature(LrpStream stream, String suffix)
        {
            var hash = this.GenerateHash();
            stream.WriteLine("\t\"{0}@{1};\"{2}", this.Name, hash, suffix);
        }

        string GenerateHash()
        {
            var builder = new StringBuilder();
            builder.Append(this.types.GenerateSignature(this.Return.Type));
            builder.AppendFormat(" {0} ", this.Name);
            foreach (var element in this)
            {
                var st = this.types.GenerateSignature(element.Type);
                builder.AppendFormat("{0}:{1}:{2}", element.Direction, element.Name, st);
            }
            var result = HashCalculator.HashFromText(builder.ToString());
            return result;
        }

        void GenerateCharpBody(int componentId, LrpStream stream)
        {
            foreach (var element in this)
            {
                element.GenerateCSharpRequest(stream);
            }
            stream.WriteLine();
            stream.WriteLine("int _status = m_client.Invoke({0}, {1}, buffer);", componentId, this.Id);

            stream.WriteLine("TypesSerializer.Throw(_status, buffer);");

            stream.WriteLine();

            foreach (var element in this)
            {
                element.GenerateCSharpResponse(stream);
            }
            var returnType = this.Return.Type;
            if (returnType != EmbeddedDataTypes.Void)
            {
                stream.WriteLine("var _result = buffer.Read{0}();", returnType.Suffix);
                stream.WriteLine("return _result;");
            }
        }
        #endregion

        #region Database

        public void GenerateCSharpReader(LrpStream stream)
        {
            this.GenerateCSharpDelegateEventReader(stream);
            this.GenerateCSharpRaiseEventReader(stream);
        }

        void GenerateCSharpDelegateEventReader(LrpStream stream)
        {
            stream.BeginLine();
            stream.Write("public delegate void {0}Handler(", this.Name);
            var count = this.Count;
            if (count > 0)
            {
                var parameter = this[0];
                parameter.GenerateCSharpSignatureReader(stream, this.types);
            }
            for (var index = 1; index < count; ++index)
            {
                stream.Write(", ");
                var parameter = this[index];
                parameter.GenerateCSharpSignatureReader(stream, this.types);
            }
            stream.Write(");");
            stream.EndLine();
            stream.WriteLine("public event {0}Handler {0};", this.Name);
        }

        void GenerateCSharpRaiseEventReader(LrpStream stream)
        {
            stream.WriteLine("private void Raise{0}(TextStream stream)", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("var handler = {0};", this.Name);
            stream.WriteLine("if(null == handler)");
            stream.WriteLine("{");
            stream.WriteLine("return;");
            stream.WriteLine("}");


            stream.WriteLine("stream.ValidateVerbatimText('(');");

            var count = this.Count;
            var hasDefaultValue = false;
            if (count > 0)
            {
                var parameter = this[0];
                hasDefaultValue = parameter.HasDefaultValue;
                parameter.GenerateCSharpMemberReader(stream);
            }
            for (int index = 1; index < count; ++index)
            {
                if (hasDefaultValue)
                {
                    stream.WriteLine("if (stream.EntryWasFound)");
                    stream.WriteLine("{");
                }
                stream.WriteLine("stream.ValidateVerbatimText(\", \");");
                if (hasDefaultValue)
                {
                    stream.WriteLine("}");
                }
                var parameter = this[index];
                hasDefaultValue = parameter.HasDefaultValue;
                parameter.GenerateCSharpMemberReader(stream);
            }
            stream.WriteLine("stream.ValidateVerbatimText(\");\");");

            stream.BeginLine();
            stream.Write("handler(");
            if (count > 0)
            {
                var parameter = this[0];
                stream.Write("{0}", parameter.Name);
            }
            for (var index = 1; index < count; ++index)
            {
                var parameter = this[index];
                stream.Write(", {0}", parameter.Name);
            }
            stream.Write(");");
            stream.EndLine();

            stream.WriteLine("}");
        }

        #endregion

        #endregion

        #region Cpp Server

        public void GenerateCppServer(LrpStream stream, Component component)
        {
            stream.WriteLine("void LrpInvoke_{0}_{1}(size_t offset, MemoryBuffer& buffer, LrpChannel* pChannel)", component.Name, this.Name);
            stream.WriteLine("{");

            stream.WriteLine("pChannel;// if all methods of LrpChannel are static then the next line generates warning #4100");
            stream.WriteLine("auto& component = pChannel->Get{0}();", component.Name);
            stream.WriteLine("component; // if all methods of component are static then the next line generates warning #4189");
            foreach (var element in this)
            {
                element.GenerateCppServerRequest(this.types, stream);
            }
            stream.BeginLine();
            var type = this.Return.Type;

            if (type == EmbeddedDataTypes.Void)
            {
                stream.Write("component.{0}(", this.Name);
            }
            else
            {
                stream.Write("auto result = component.{0}(", this.Name);
            }
            var count = this.Count;
            if (count > 0)
            {
                stream.Write("arg0");
            }
            for (var index = 1; index < count; ++index)
            {
                stream.Write(", arg{0}", index);
            }
            stream.Write(");");
            stream.EndLine();
            stream.WriteLine("buffer.Reset(offset);");

            foreach (var element in this)
            {
                element.GenerateCppLocalServerResponse(stream);
            }
            if (type != EmbeddedDataTypes.Void)
            {
                stream.WriteLine("Write{0}(result, buffer);", type.Suffix);
            }
            stream.WriteLine("}");
        }
        #endregion

        #region Cpp Writer

        public void GenerateCppHeaderWriter(LrpStream stream)
        {
            stream.BeginLine();
            stream.Write("void {0}(", this.Name);
            var count = this.Count;
            if (count > 0)
            {
                var param = this[0];
                param.GenerateCppHeaderWriter(this.types, stream);
            }
            for (var index = 1; index < count; ++index)
            {
                stream.Write(", ");
                var param = this[index];
                param.GenerateCppHeaderWriter(this.types, stream);
            }
            stream.Write(");");
            stream.EndLine();
        }

        public void GenerateCppSourceWriter(LrpStream stream, Component component)
        {
            stream.BeginLine();
            stream.Write("void {0}::{1}(", component.Name, this.Name);
            var count = this.Count;
            if (count > 0)
            {
                var param = this[0];
                param.GenerateCppHeaderWriter(this.types, stream);
            }
            for (var index = 1; index < count; ++index)
            {
                stream.Write(", ");
                var param = this[index];
                param.GenerateCppHeaderWriter(this.types, stream);
            }
            stream.Write(")");
            stream.EndLine();
            stream.WriteLine("{");
            stream.WriteLine("std::stringstream _stream;");

            stream.WriteLine("_stream << \"[{0}]{1}[{2}]{3}(\";", component.Id, component.Name, this.Id, this.Name);

            if (count > 0)
            {
                var param = this[0];
                param.GenerateCppSourceWriter(this.types, stream);
            }
            for (var index = 1; index < count; ++index)
            {
                stream.WriteLine("_stream<<\", \";");
                var param = this[index];
                param.GenerateCppSourceWriter(this.types, stream);
            }
            stream.WriteLine("_stream << \");\";");
            stream.WriteLine("m_stream->Write(_stream.str());");
            stream.WriteLine("}");
        }

        #endregion

        #region Cpp Client

        public void GenerateCpp(bool isLocal, int componentId, LrpStream stream)
        {
            stream.WriteLine("const static unsigned short LrpMethod_{0}_Id = {1};", this.Name, this.Id);
            stream.WriteLine("bool Is_{0}_Supported() const", this.Name);
            stream.WriteLine("{");
            stream.WriteLine("return m_channel->IsSupported({0}, {1});", componentId, this.Id);
            stream.WriteLine("}");

            stream.BeginLine();
            stream.Write("{0} {1}(", this.types.ToCppFullName(this.Return.Type) , this.Name);

            var it = this.GetEnumerator();
            if (it.MoveNext())
            {
                it.Current.GenerateCppSignature(this.types, stream);
            }
            for (; it.MoveNext(); )
            {
                stream.Write(", ");
                it.Current.GenerateCppSignature(this.types, stream);
            }
            stream.Write(")");
            stream.EndLine();
            stream.WriteLine("{");
            this.GenerateCppBody(componentId, stream);
            stream.WriteLine("}");
        }

        void GenerateCppBody(int componentId, LrpStream stream)
        {
            stream.WriteLine("MemoryBuffer buffer;");
            stream.WriteLine("m_channel->Initialize(buffer);");
            stream.WriteLine();

            foreach (var element in this)
            {
                element.GenerateCppRequest(stream);
            }
            stream.WriteLine();
            
            stream.WriteLine("const HRESULT _status = m_channel->Invoke({0}, {1}, buffer);", componentId, this.Id);
            
            stream.WriteLine("Throw(_status, buffer);");
            stream.WriteLine();

            foreach (var element in this)
            {
                element.GenerateCppResponse(stream);
            }
            var returnType = this.Return.Type;
            if (returnType != EmbeddedDataTypes.Void)
            {
                stream.WriteLine("auto _result = Read{0}(buffer);", returnType.Suffix);
                stream.WriteLine("return _result;");
            }
        }

        #endregion

        #region Fields

        readonly DataTypes types;

        #endregion
    }
}
