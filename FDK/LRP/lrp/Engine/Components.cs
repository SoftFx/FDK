namespace Lrp.Engine
{
    using System;
    using System.Xml;
    using Lrp.Engine.Formating;

    class Components : ListSet<Component>
    {
        public Components(DataTypes types, XmlNode node)
        {
            this.types = types;
            XmlSerializer.Initialize(this, node);
            var nodes = node.ChildNodes;

            var count = nodes.Count;

            for (var index = 0; index < count; ++index)
            {
                var element = nodes[index];
                var component = new Component(this.Count, types, element);
                this.Add(component);
            }
        }

        #region C# Generation

        #region Binary

        public void GenerateCharpClient(bool isLocal, string namespaceName, string outputDirectory)
        {
            foreach (var element in this)
            {
                element.GenerateCharpClient(isLocal, namespaceName, outputDirectory);
            }
            using (var stream = new LrpStream(outputDirectory, "Signature.cs"))
            {
                this.GenerateCSharpSignature(namespaceName, stream);
            }
        }

        public void GenerateCharpServer(bool isLocal, string namespaceName, string outputDirectory)
        {
            using (var stream = new LrpStream(outputDirectory, "Server.cs"))
            {
                this.GenerateCSharpServer(isLocal, stream, namespaceName);
            }
            using (var stream = new LrpStream(outputDirectory, "Signature.cs"))
            {
                this.GenerateCSharpSignature(namespaceName, stream);
            }
        }

        void GenerateCSharpSignature(string namespaceName, LrpStream stream)
        {
            stream.WriteLine("using SoftFX.Lrp;");
            stream.WriteLine();
            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("namespace {0}", namespaceName);
                stream.WriteLine("{");
                this.GenerateCSharpSignature(stream);
                stream.WriteLine("}");
            }
            else
            {
                this.GenerateCSharpSignature(stream);
            }
        }

        void GenerateCSharpSignature(LrpStream stream)
        {
            stream.WriteLine("internal static class Signature");
            stream.WriteLine("{");
            stream.WriteLine("public const string Value = ");
            this.types.GenerateExceptionsSignature(stream, "+");
            foreach (var element in this)
            {
                element.GenerateSignature(stream, "+");
            }
            stream.WriteLine("\"\";");
            stream.WriteLine("}");
        }

        void GenerateCSharpServer(bool isLocal, LrpStream stream, string namespaceName)
        {
            stream.WriteLine("using SoftFX.Lrp;");
            stream.WriteLine();
            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("namespace {0}", namespaceName);
                stream.WriteLine("{");
            }
            stream.WriteLine("public static unsafe class Server");

            stream.WriteLine("{");
            stream.WriteLine("#region members");
            stream.WriteLine("private static readonly Channel m_channel = new Channel();");
            stream.WriteLine("private static readonly LocalServerInvokeHandler m_invoke = LrpInvoke;");
            stream.WriteLine("#endregion");
            stream.WriteLine();
            stream.WriteLine("#region properties");
            stream.WriteLine("public static string LrpSignature");
            stream.WriteLine("{");
            stream.WriteLine("get");
            stream.WriteLine("{");
            stream.WriteLine("return Signature.Value;");
            stream.WriteLine("}");
            stream.WriteLine("}");

            stream.WriteLine("#endregion");

            foreach (var element in this)
            {
                element.GenerateCSharpServer(stream);
            }

            stream.WriteLine("#region component handlers");
            stream.WriteLine("private static readonly ComponentHandler<Channel>[] gHandlers = new ComponentHandler<Channel>[]");
            stream.WriteLine("{");
            foreach (var element in this)
            {
                stream.WriteLine("LrpInvoke_{0},", element.Name);
            }
            stream.WriteLine("};");
            stream.WriteLine();
            stream.WriteLine("#endregion");

            stream.WriteLine("public static int LrpInitialize(string argument)");
            stream.WriteLine("{");
            stream.WriteLine("int result = LocalServer.Initialize(argument, Signature.Value, m_invoke);");
            stream.WriteLine("return result;");
            stream.WriteLine("}");

            stream.WriteLine("public static int LrpInvoke(ushort componentId, ushort methodId, void* heap, int* pSize, void** ppData, int* pCapacity)");
            stream.WriteLine("{");
            stream.WriteLine("try");
            stream.WriteLine("{");
            stream.WriteLine("MemoryBuffer buffer = new MemoryBuffer(heap, *ppData, *pSize, *pCapacity);");
            stream.WriteLine("int result = LrpInvokeEx(0, componentId, methodId, buffer, m_channel);");

            stream.WriteLine("*pSize = buffer.Size;");
            stream.WriteLine("*ppData = buffer.Data;");
            stream.WriteLine("*pCapacity = buffer.Capacity;");
            stream.WriteLine("buffer.Heap = null;");
            stream.WriteLine("return result;");
            stream.WriteLine("}");
            stream.WriteLine("catch(System.Exception)");
            stream.WriteLine("{");
            stream.WriteLine("return MagicNumbers.E_FAIL;");
            stream.WriteLine("}");
            stream.WriteLine("}");

            stream.WriteLine("private static int LrpInvokeEx(int offset, int componentId, int methodId, MemoryBuffer buffer, Channel channel)");
            stream.WriteLine("{");

            stream.WriteLine("if((componentId < 0) || (componentId >= {0}))", this.Count);
            stream.WriteLine("{");
            stream.WriteLine("return MagicNumbers.LRP_INVALID_COMPONENT_ID;");
            stream.WriteLine("}");
            stream.WriteLine("int result = MagicNumbers.LRP_EXCEPTION;");



            stream.WriteLine("try");
            stream.WriteLine("{");
            stream.WriteLine("try");
            stream.WriteLine("{");

            stream.WriteLine("result = gHandlers[componentId](offset, methodId, buffer, channel);");
            stream.WriteLine("return result;");
            stream.WriteLine("}");
            var exceptionId = -1;
            foreach (var element in this.types)
            {
                if (!element.IsException)
                {
                    continue;
                }
                exceptionId++;
                stream.WriteLine("catch({0} e)", element.CppFullName);
                stream.WriteLine("{");
                stream.WriteLine("buffer.Reset(offset);");
                stream.WriteLine("buffer.WriteInt32({0});", exceptionId);
                stream.WriteLine("buffer.Write{0}(e);", element.Suffix);
                stream.WriteLine("}");
            }
            stream.WriteLine("catch(System.Exception e)");
            stream.WriteLine("{");
            stream.WriteLine("buffer.Reset(offset);");
            stream.WriteLine("buffer.WriteInt32(-1);", exceptionId);
            stream.WriteLine("buffer.WriteAString(e.Message);");
            stream.WriteLine("}");
            stream.WriteLine("}");
            stream.WriteLine("catch(System.Exception)");
            stream.WriteLine("{");
            stream.WriteLine("result = MagicNumbers.E_FAIL;");
            stream.WriteLine("}");



            stream.WriteLine("return result;");
            stream.WriteLine("}");

            stream.WriteLine("}");

            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("}");
            }
        }

        #endregion

        #region Database

        public void GenerateCSharpReader(string namespaceName, LrpStream stream)
        {
            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("namespace {0}", namespaceName);
                stream.WriteLine("{");
            }
            stream.WriteLine("public class Protocol : BaseProtocolReader");
            stream.WriteLine("{");
            stream.WriteLine("public Protocol()");
            stream.WriteLine("{");
            foreach (var element in this)
            {
                stream.WriteLine("base.Add(\"{0}\", {0});", element.Name);
            }
            stream.WriteLine("}");
            foreach (var element in this)
            {
                stream.WriteLine("public readonly {0} {0} = new {0}();", element.Name);
            }
            stream.WriteLine("}");
            foreach (var element in this)
            {
                element.GenerateCSharpReader(stream);
            }
            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("}");
            }
        }

        #endregion

        #endregion

        #region C++ generation

        #region Database

        public void GenerateCppHeaderWriter(string path)
        {
            using (var stream = new LrpStream(path))
            {
                stream.WriteLine("#pragma once");
                foreach (var element in this)
                {
                    element.GenerateCppHeaderWriter(stream);
                }
            }
        }

        public void GenerateCppSourceWriter(LrpStream stream)
        {
            foreach (var element in this)
            {
                element.GenerateCppSourceWriter(stream);
            }
        }

        #endregion

        #region Binary

        public void GenerateCppRemoteClient(string outputDirectory, string prefix)
        {
            foreach (var element in this)
            {
                element.GenerateCppRemoteClient(outputDirectory);
            }
            using(var stream = new LrpStream(outputDirectory, prefix + "Signature.hpp"))
            {
                GenerateCppSignature(false, stream);
            }
        }

        public void GenerateCppLocalClient(string outputDirectory, string prefix)
        {
            foreach (var element in this)
            {
                element.GenerateCppLocalClient(outputDirectory);
            }
            using (var stream = new LrpStream(outputDirectory, prefix + "Signature.hpp"))
            {
                this.GenerateCppSignature(false, stream);
            }
        }

        public void GenerateCppServer(bool isLocal, DataTypes types, string outputDirectory, string prefix)
        {
            var name = prefix + "Server.hpp";
            using (var stream = new LrpStream(outputDirectory, name))
            {
                foreach (var element in this)
                {
                    element.GenerateCppServer(stream);
                }
                stream.WriteLine();
                stream.WriteLine("// components handler");
                stream.WriteLine("namespace");
                stream.WriteLine("{");
                stream.WriteLine("typedef HRESULT (*LrpInvoke_Component_Handler)(size_t offset, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel);");
                stream.WriteLine("LrpInvoke_Component_Handler gHandlers[] = ");
                stream.WriteLine("{");
                foreach (var element in this)
                {
                    stream.WriteLine("LrpInvoke_{0},", element.Name);
                }
                stream.WriteLine("};");
                stream.WriteLine("}");
                stream.WriteLine();
                this.GenerateCppLocalServerComponentsHandler(isLocal, types, stream);
                this.GenerateCppSignature(true, stream);
            }
        }

        void GenerateCppSignature(bool isServer, LrpStream stream)
        {
            if (isServer)
            {
                stream.WriteLine("extern \"C\" const char* __stdcall LrpSignature()");
            }
            else
            {
                stream.WriteLine("const char* __stdcall LrpSignature()"); 
            }
            stream.WriteLine("{");
            stream.WriteLine("return ");
            this.types.GenerateExceptionsSignature(stream, string.Empty);
            foreach (var element in this)
            {
                element.GenerateSignature(stream, string.Empty);
            }
            stream.WriteLine("\"\";");
            stream.WriteLine("}");
        }

        void GenerateCppLocalServerComponentsHandler(bool isLocal, DataTypes types, LrpStream stream)
        {
            stream.WriteLine("namespace");
            stream.WriteLine("{");
            this.GenerateCppLocalServerComponentsUnifiedHandler(types, stream);
            
            stream.WriteLine("}");
            if (isLocal)
            {
                this.GenerateCppLocalServerComponentsHandler(stream);
            }
            else
            {
                this.GenerateCppRemoteServerComponentsHandler(stream);
            }
        }

        void GenerateCppLocalServerComponentsUnifiedHandler(DataTypes types, LrpStream stream)
        {
            stream.WriteLine("HRESULT LrpInvokeEx(size_t offset, size_t componentId, size_t methodId, MemoryBuffer& buffer, LrpChannel* pChannel)");
            stream.WriteLine("{");
            stream.WriteLine("if(componentId >= {0})", this.Count);
            stream.WriteLine("{");
            stream.WriteLine("return LRP_INVALID_COMPONENT_ID;");
            stream.WriteLine("}");

            stream.WriteLine("HRESULT result = LRP_EXCEPTION;");
            stream.WriteLine("try");
            stream.WriteLine("{");
            stream.WriteLine("try");
            stream.WriteLine("{");

            stream.WriteLine("result = gHandlers[componentId](offset, methodId, buffer, pChannel);");
            stream.WriteLine("return result;");
            stream.WriteLine("}");
            var exceptionId = -1;
            foreach (var element in types)
            {
                if (!element.IsException)
                {
                    continue;
                }
                exceptionId++;
                stream.WriteLine("catch(const {0}& e)", element.CppFullName);
                stream.WriteLine("{");
                stream.WriteLine("buffer.Reset(offset);");
                stream.WriteLine("WriteInt32({0}, buffer);", exceptionId);
                stream.WriteLine("Write{0}(e, buffer);", element.Suffix);
                stream.WriteLine("}");
            }
            stream.WriteLine("catch(const std::exception& e)");
            stream.WriteLine("{");
            stream.WriteLine("buffer.Reset(offset);");
            stream.WriteLine("WriteInt32(-1, buffer);", exceptionId);
            stream.WriteLine("WriteAString(e.what(), buffer);");
            stream.WriteLine("}");
            stream.WriteLine("catch(...)");
            stream.WriteLine("{");
            stream.WriteLine("result = E_FAIL;");
            stream.WriteLine("}");
            stream.WriteLine("}");
            stream.WriteLine("catch(...)");
            stream.WriteLine("{");
            stream.WriteLine("result = E_FAIL;");
            stream.WriteLine("}");
            stream.WriteLine("return result;");
            stream.WriteLine("}");
        }

        void GenerateCppLocalServerComponentsHandler(LrpStream stream)
        {
            stream.WriteLine();
            stream.WriteLine("extern \"C\" HRESULT __stdcall LrpInvoke(unsigned __int16 componentId, unsigned __int16 methodId, void* heap, unsigned __int32* pSize, void** ppData, unsigned __int32* pCapacity)");

            stream.WriteLine("{");
            stream.WriteLine("MemoryBuffer buffer(heap, *ppData, *pSize, *pCapacity);");
            stream.WriteLine("HRESULT result = LrpInvokeEx(0, componentId, methodId, buffer, nullptr);");

            stream.WriteLine("*pSize = static_cast<unsigned __int32>(buffer.GetSize());");
            stream.WriteLine("*ppData = buffer.GetData();");
            stream.WriteLine("*pCapacity = static_cast<unsigned __int32>(buffer.GetCapacity());");
            stream.WriteLine("buffer = MemoryBuffer();");
            stream.WriteLine("return result;");
            stream.WriteLine("}");
        }

        void GenerateCppRemoteServerComponentsHandler(LrpStream stream)
        {
            stream.WriteLine();
            stream.WriteLine("namespace");
            stream.WriteLine("{");
            stream.WriteLine("HRESULT __stdcall LrpInvoke(size_t componentId, size_t methodId, MemoryBuffer& buffer, void* handle)");

            stream.WriteLine("{");
            stream.WriteLine("LrpChannel* pChannel = reinterpret_cast<LrpChannel*>(handle);");
            stream.WriteLine("HRESULT result = LrpInvokeEx(16, componentId, methodId, buffer, pChannel);");
            stream.WriteLine("buffer.SetPosition(12);");
            stream.WriteLine("WriteInt32(result, buffer);");
            stream.WriteLine("return result;");
            stream.WriteLine("}");
            stream.WriteLine("}");
        }

        #endregion

        #endregion

        #region Fields

        readonly DataTypes types;

        #endregion
    }
}
