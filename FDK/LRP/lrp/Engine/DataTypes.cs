namespace Lrp.Engine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using Lrp.Engine.Formating;

    [XmlName("Types")]
    class DataTypes : IEnumerable<DataType>
    {
        #region Construction

        public DataTypes()
        {
            this.Construct();
        }

        public DataTypes(XmlNode node)
        {
            this.Construct();
            XmlSerializer.Initialize(this, node);
            var nodes = node.ChildNodes;
            var count = nodes.Count;
            for (var index = 0; index < count; ++index)
            {
                var childNode = nodes[index];
                var dataType = new DataType(childNode);
                this.Add(dataType);
            }
        }

        void Construct()
        {
            var type = typeof(EmbeddedDataTypes);
            var fields = type.GetFields();

            foreach (var element in fields)
            {
                var obj = element.GetValue(null);
                var data = (DataType)obj;
                this.Add(data);
            }
        }

        void Add(DataType type)
        {
            if (this.nameToType.ContainsKey(type.Name))
            {
                var message = string.Format("Duplicate type name = {0}", type.Name);
                throw new Exception(message);
            }

            this.nameToType[type.Name] = type;
            this.types.Add(type);
        }

        #endregion

        public DataType this[string name]
        {
            get
            {
                DataType result;

                if (!this.nameToType.TryGetValue(name, out result))
                {
                    var message = string.Format("Unknown type = {0}", name);
                    throw new Exception(message);
                }

                return result;
            }
        }

        #region Generation of Names

        public string ToCSharpFullName(DataType type)
        {
            var kind = type.Kind;
            if (kind == Kind.Embedded || kind == Kind.Class || kind == Kind.Struct || kind == Kind.Enum32 || kind == Kind.Exception)
            {
                return type.CSharpFullName;
            }
            if (kind == Kind.Array)
            {
                return this.ToCSharpArrayFullName(type);
            }
            if (kind == Kind.Vector)
            {
                return this.ToCSharpVectorFullName(type);
            }
            if (kind == Kind.SMap)
            {
                return this.ToCSharpSMapFullName(type);
            }
            if (kind == Kind.HMap)
            {
                return this.ToCSharpHMapFullName(type);
            }
            if (kind == Kind.SSet)
            {
                return this.ToCSharpSSetFullName(type);
            }
            var message = string.Format("Unsupported data type kind = {0}", kind);
            throw new Exception(message);
        }

        string ToCSharpArrayFullName(DataType type)
        {
            var name = this.ToCSharpFullName(this[type.Value]);
            var result = string.Format("{0}[]", name);
            return result;
        }

        string ToCSharpVectorFullName(DataType type)
        {
            var name = this.ToCSharpFullName(this[type.Value]);
            var result = string.Format("System.Collections.Generic.List<{0}>", name);
            return result;
        }

        string ToCSharpSSetFullName(DataType type)
        {
            var name = this.ToCSharpFullName(this[type.Value]);
            var result = string.Format("System.Collections.Generic.SortedSet<{0}>", name);
            return result;
        }

        string ToCSharpSMapFullName(DataType type)
        {
            var key = this.ToCSharpFullName(this[type.Key]);
            var value = this.ToCSharpFullName(this[type.Value]);
            var result = string.Format("System.Collections.Generic.SortedDictionary<{0}, {1}>", key, value);
            return result;
        }

        string ToCSharpHMapFullName(DataType type)
        {
            var key = this.ToCSharpFullName(this[type.Key]);
            var value = this.ToCSharpFullName(this[type.Value]);
            var result = string.Format("System.Collections.Generic.Dictionary<{0}, {1}>", key, value);
            return result;
        }

        public string ToCppFullName(DataType type)
        {
            var kind = type.Kind;
            if (kind == Kind.Embedded || kind == Kind.Class || kind == Kind.Struct || kind == Kind.Enum32 || kind == Kind.Exception)
            {
                return type.CppFullName;
            }
            if (kind == Kind.Array)
            {
                return this.ToCppArrayFullName(type);
            }
            if (kind == Kind.Vector)
            {
                return this.ToCppVectorFullName(type);
            }
            if (Kind.SMap == kind)
            {
                return ToCppSMapFullName(type);
            }
            if (kind == Kind.HMap)
            {
                return this.ToCppHMapFullName(type);
            }
            if (kind == Kind.SSet)
            {
                return this.ToCppSSetFullName(type);
            }

            var message = string.Format("Unsupported data type kind = {0}", kind);
            throw new Exception(message);
        }

        string ToCppArrayFullName(DataType type)
        {
            var name = this.ToCppFullName(this[type.Value]);
            var result = string.Format("std::vector<{0}>", name);
            return result;
        }

        string ToCppVectorFullName(DataType type)
        {
            var name = this.ToCppFullName(this[type.Value]);
            var result = string.Format("std::vector<{0}>", name);
            return result;
        }

        string ToCppSMapFullName(DataType type)
        {
            var key = this.ToCppFullName(this[type.Key]);
            var value = this.ToCppFullName(this[type.Value]);
            var result = string.Format("std::map<{0}, {1}>", key, value);
            return result;
        }

        string ToCppHMapFullName(DataType type)
        {
            var key = this.ToCppFullName(this[type.Key]);
            var value = this.ToCppFullName(this[type.Value]);
            var result = string.Format("stdext::hash_map<{0}, {1}>", key, value);
            return result;
        }

        string ToCppSSetFullName(DataType type)
        {
            var name = this.ToCppFullName(this[type.Value]);
            var result = string.Format("std::set<{0}>", name);
            return result;
        }

        #endregion

        #region Generation of Signature

        public string GenerateSignature(DataType type)
        {
            var builder = new StringBuilder();
            this.GenerateSignature(type, builder);
            var result = builder.ToString();
            return result;
        }

        public void GenerateExceptionsSignature(LrpStream stream, string suffix)
        {
            stream.WriteLine("\"$Exceptions;\"{0}", suffix);
            foreach (var element in this)
            {
                if (element.Kind == Kind.Exception)
                {
                    var st = this.GenerateSignature(element);
                    var hash = HashCalculator.HashFromText(st);
                    stream.WriteLine("\t\"{0}@{1};\"{2}", element.Name, hash, suffix);
                }
            }
        }

        void GenerateSignature(DataType type, StringBuilder builder)
        {
            var kind = type.Kind;
            if (kind == Kind.Embedded || kind == Kind.Enum32)
            {
                builder.AppendFormat("{0}-{1};", kind, type.Name);
            }
            else if (kind == Kind.Class || kind == Kind.Struct || kind == Kind.Exception)
            {
                builder.AppendFormat("{0}-{1}(", kind, type.Name);
                foreach (var element in type)
                {
                    builder.AppendFormat("{0}-", element.Name);
                    var memberType = this[element.TypeName];
                    this.GenerateSignature(memberType, builder);
                }
                builder.Append(')');
            }
            else if ((Kind.Array == kind) || (Kind.Vector == kind) || kind == Kind.SSet)
            {
                builder.AppendFormat("{0}-{1}(", kind, type.Name);
                var valueType = this[type.Value];
                this.GenerateSignature(valueType, builder);
                builder.Append(')');
            }
            else if (kind == Kind.SMap || kind == Kind.HMap)
            {
                var keyType = this[type.Key];
                var valueType = this[type.Value];

                builder.AppendFormat("{0}-{1}(", kind, type.Name);
                this.GenerateSignature(keyType, builder);
                builder.Append(", ");
                this.GenerateSignature(valueType, builder);
                builder.Append(')');
            }
            else
            {
                var message = string.Format("Unsupported data type kind = {0}", kind);
                throw new Exception(message);
            }
        }
        #endregion

        #region CSharp

        #region Binary

        public void GenerateCSharp(string namespaceName, string outputDirectory)
        {
            using (var stream = new LrpStream(outputDirectory, "TypesSerializer.cs"))
            {
                this.GenerateCSharp(stream, namespaceName);
            }
        }

        void GenerateCSharp(LrpStream stream, string namespaceName)
        {
            stream.WriteLine("using SoftFX.Lrp;");
            stream.WriteLine();
            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("namespace {0}", namespaceName);
                stream.WriteLine("{");
                this.GenerateCSharp(stream);
                stream.WriteLine("}");
            }
            else
            {
                this.GenerateCSharp(stream);
            }
        }

        void GenerateCSharp(LrpStream stream)
        {
            stream.WriteLine("internal static class TypesSerializer");
            stream.WriteLine("{");
            foreach (var element in this.types)
            {
                this.GenerateCSharp(stream, element);
            }
            this.GenerateCSharpThrow(stream);
            stream.WriteLine("}");
        }

        void GenerateCSharp(LrpStream stream, DataType type)
        {
            if (type.Embedded)
            {
                return;
            }

            stream.WriteLine("public static {0} Read{1}(this MemoryBuffer buffer)", ToCSharpFullName(type), type.Name);
            stream.WriteLine("{");
            this.GenerateCSharpReading(stream, type);
            stream.WriteLine("}");

            if (type.IsException)
            {
                return;
            }

            stream.WriteLine("public static void Write{0}(this MemoryBuffer buffer, {1} arg)", type.Name, ToCSharpFullName(type));
            stream.WriteLine("{");

            this.GenerateCSharpWriting(stream, type);

            stream.WriteLine("}");
        }

        void GenerateCSharpReading(LrpStream stream, DataType type)
        {
            var kind = type.Kind;
            if (kind == Kind.Exception)
            {
                stream.WriteLine("System.String _message = buffer.ReadAString();");
                stream.WriteLine("var result = new {0}(_message);", type.CSharpFullName);
            }
            else if (kind == Kind.Enum32)
            {
                stream.WriteLine("var result = ({0})buffer.ReadInt32();", type.CSharpFullName);
            }
            else if (kind == Kind.Struct || kind == Kind.Class)
            {
                stream.WriteLine("var result = new {0}();", type.CSharpFullName);
            }
            else if (kind == Kind.Array)
            {
                stream.WriteLine("int length = buffer.ReadCount();");
                var st = ToCSharpFullName(this[type.Value]);

                stream.WriteLine("var result = new {0}[length];", st);
                stream.WriteLine("for(int index = 0; index < length; ++index)");
                stream.WriteLine("{");
                stream.WriteLine("result[index] = buffer.Read{0}();", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            else if (kind == Kind.Vector)
            {
                stream.WriteLine("int length = buffer.ReadCount();");
                var st = ToCSharpFullName(this[type.Value]);

                stream.WriteLine("var result = new System.Collections.Generic.List<{0}>(length);", st);
                stream.WriteLine("for(int index = 0; index < length; ++index)");
                stream.WriteLine("{");
                stream.WriteLine("result.Add(buffer.Read{0}());", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            else if (kind == Kind.SMap)
            {
                stream.WriteLine("int length = buffer.ReadCount();");
                var key = ToCSharpFullName(this[type.Key]);
                var value = ToCSharpFullName(this[type.Value]);

                stream.WriteLine("var result = new System.Collections.Generic.SortedDictionary<{0}, {1}>();", key, value);
                stream.WriteLine("for(int index = 0; index < length; ++index)");
                stream.WriteLine("{");
                stream.WriteLine("result.Add(buffer.Read{0}(), buffer.Read{1}());", this[type.Key].Suffix, this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            else if (kind == Kind.HMap)
            {
                stream.WriteLine("int length = buffer.ReadCount();");
                string key = ToCSharpFullName(this[type.Key]);
                string value = ToCSharpFullName(this[type.Value]);

                stream.WriteLine("var result = new System.Collections.Generic.Dictionary<{0}, {1}>();", key, value);
                stream.WriteLine("for(int index = 0; index < length; ++index)");
                stream.WriteLine("{");
                stream.WriteLine("result.Add(buffer.Read{0}(), buffer.Read{1}());", this[type.Key].Suffix, this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            foreach (var element in type)
            {
                var memberType = this[element.TypeName];
                stream.WriteLine("result.{0} = buffer.Read{1}();", element.Name, memberType.Suffix);
            }
            stream.WriteLine("return result;");
        }

        void GenerateCSharpWriting(LrpStream stream, DataType type)
        {
            var kind = type.Kind;
            if (kind == Kind.Enum32)
            {
                stream.WriteLine("buffer.WriteInt32((int)arg);");
            }
            else if (kind == Kind.Array)
            {
                stream.WriteLine("buffer.WriteInt32(arg.Length);");
                stream.WriteLine("foreach(var element in arg)");
                stream.WriteLine("{");
                stream.WriteLine("buffer.Write{0}(element);", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            else if (kind == Kind.Vector)
            {
                stream.WriteLine("buffer.WriteInt32(arg.Count);");
                stream.WriteLine("foreach(var element in arg)");
                stream.WriteLine("{");
                stream.WriteLine("buffer.Write{0}(element);", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            else if (kind == Kind.SMap || kind == Kind.HMap)
            {
                stream.WriteLine("buffer.WriteInt32(arg.Count);");
                stream.WriteLine("foreach(var element in arg)");
                stream.WriteLine("{");
                stream.WriteLine("buffer.Write{0}(element.Key);", this[type.Key].Suffix);
                stream.WriteLine("buffer.Write{0}(element.Value);", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            foreach (var element in type)
            {
                var memberType = this[element.TypeName];
                stream.WriteLine("buffer.Write{0}(arg.{1});", memberType.Suffix, element.Name);
            }
        }

        void GenerateCSharpThrow(LrpStream stream)
        {
            stream.WriteLine("public static void Throw(System.Int32 status, MemoryBuffer buffer)");
            stream.WriteLine("{");
            stream.WriteLine("if(status >= 0)");
            stream.WriteLine("{");
            stream.WriteLine("return;");
            stream.WriteLine("}");
            stream.WriteLine("if(MagicNumbers.LRP_EXCEPTION != status)");
            stream.WriteLine("{");
            stream.WriteLine("throw new System.Exception(\"Unexpected exception has been encountered\");");
            stream.WriteLine("}");

            stream.WriteLine("System.Int32 _id = buffer.ReadInt32();");

            var exceptionId = -1;
            foreach (var element in this.types)
            {
                if (!element.IsException)
                {
                    continue;
                }
                exceptionId++;
                stream.WriteLine("if({0} == _id)", exceptionId);
                stream.WriteLine("{");
                stream.WriteLine("throw Read{0}(buffer);", element.Suffix);
                stream.WriteLine("}");
            }
            stream.WriteLine("System.String _message = buffer.ReadAString();");
            stream.WriteLine("throw new System.Exception(_message);");
            stream.WriteLine("}");
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
            stream.WriteLine("internal static class TypesTextSerializer");
            stream.WriteLine("{");

            foreach (var element in this)
            {
                this.GenerateCSharpReader(stream, element);
            }
            stream.WriteLine("}");

            if (!string.IsNullOrEmpty(namespaceName))
            {
                stream.WriteLine("}");
            }
        }

        void GenerateCSharpReader(LrpStream stream, DataType type)
        {
            if (type.Embedded)
            {
                return;
            }
            var typeFullName = this.ToCSharpFullName(type);
            stream.WriteLine("public static {0} Read{1}(this TextStream stream, string name = null)", typeFullName, type.Name);
            stream.WriteLine("{");
            stream.WriteLine("if (null != name)");
            stream.WriteLine("{");
            stream.WriteLine("stream.ValidateVerbatimText(name);");
            stream.WriteLine("stream.ValidateVerbatimText(\" = \");");
            stream.WriteLine("}");
            if (type.Kind == Kind.Enum32)
            {
                stream.WriteLine("var result = ({0})stream.ReadInt32(name);", typeFullName);
                stream.WriteLine("return result;");
            }
            else if (type.Kind == Kind.Class)
            {
                this.GenerateCSharpClassReader(stream, type);
            }
            else if (type.Kind == Kind.SMap)
            {
                this.GenerateCSharpSMapReader(stream, type);
            }
            else if (type.Kind == Kind.Array)
            {
                this.GenerateCSharpArrayReader(stream, type);
            }
            else
            {
                var message = string.Format("Unsupported kind = {0}", type.Kind);
            }

            stream.WriteLine("}");
        }

        void GenerateCSharpClassReader(LrpStream stream, DataType type)
        {
            var typeFullName = this.ToCSharpFullName(type);
            stream.WriteLine("var _result = new {0}();", typeFullName);
            stream.WriteLine("stream.ValidateVerbatimText('{');");
            foreach (var element in type)
            {
                var memberType = this[element.TypeName];
                var defalutValue = element.Default;
                if (defalutValue == null)
                {
                    stream.WriteLine("_result.{0} = stream.Read{1}(\"{0}\");", element.Name, memberType.Suffix);
                    stream.WriteLine("stream.ValidateVerbatimText(';');");
                }
                else
                {
                    stream.WriteLine("_result.{0} = stream.Read{1}(\"{0}\", {2});", element.Name, memberType.Suffix, defalutValue);
                    stream.WriteLine("if(stream.EntryWasFound)");
                    stream.WriteLine("{");
                    stream.WriteLine("stream.ValidateVerbatimText(';');");
                    stream.WriteLine("}");
                }
            }
            
            
            stream.WriteLine("stream.ValidateVerbatimText('}');");
            stream.WriteLine("return _result;");
        }

        void GenerateCSharpSMapReader(LrpStream stream, DataType type)
        {
            var keyType = this[type.Key];
            var valueType = this[type.Value];
            var typeFullName = this.ToCSharpFullName(type);
            stream.WriteLine("var _result = new {0}();", typeFullName);
            stream.WriteLine("stream.ValidateVerbatimText('[');");
            stream.WriteLine("var count = stream.ReadInt32();");
            stream.WriteLine("stream.ValidateVerbatimText(\"]{\");");
            stream.WriteLine("if (count > 0)");
            stream.WriteLine("{");
            stream.WriteLine("var _key = stream.Read{0}(null);", keyType.Suffix);
            stream.WriteLine("stream.ValidateVerbatimText(\" = \");");
            stream.WriteLine("var _value = stream.Read{0}(null);", valueType.Suffix);
            stream.WriteLine("_result[_key] = _value;");
            stream.WriteLine("stream.ValidateVerbatimText(\";\");");
            stream.WriteLine("}");
            stream.WriteLine("for (int index = 1; index < count; ++index)");
            stream.WriteLine("{");
            stream.WriteLine("stream.ValidateVerbatimText(\" \");");
            stream.WriteLine("var _key = stream.Read{0}(null);", keyType.Suffix);
            stream.WriteLine("stream.ValidateVerbatimText(\" = \");");
            stream.WriteLine("var _value = stream.Read{0}(null);", valueType.Suffix);
            stream.WriteLine("_result[_key] = _value;");
            stream.WriteLine("stream.ValidateVerbatimText(\";\");");
            stream.WriteLine("}");
            stream.WriteLine("stream.ValidateVerbatimText('}');");
            stream.WriteLine("return _result;");
        }

        void GenerateCSharpArrayReader(LrpStream stream, DataType type)
        {
            var valueType = this[type.Value];
            var typeFullName = this.ToCSharpFullName(valueType);
            stream.WriteLine("stream.ValidateVerbatimText('[');");
            stream.WriteLine("var count = stream.ReadInt32();");
            stream.WriteLine("stream.ValidateVerbatimText(\"]{\");");

            stream.WriteLine("var _result = new {0}[count];", typeFullName);
            stream.WriteLine("if (count > 0)");
            stream.WriteLine("{");
            stream.WriteLine("var _value = stream.Read{0}(null);", valueType.Suffix);
            stream.WriteLine("_result[0] = _value;");
            stream.WriteLine("stream.ValidateVerbatimText(\";\");");
            stream.WriteLine("}");
            stream.WriteLine("for (int index = 1; index < count; ++index)");
            stream.WriteLine("{");
            stream.WriteLine("stream.ValidateVerbatimText(\" \");");
            stream.WriteLine("var _value = stream.Read{0}(null);", valueType.Suffix);
            stream.WriteLine("_result[index] = _value;");
            stream.WriteLine("stream.ValidateVerbatimText(\";\");");
            stream.WriteLine("}");
            stream.WriteLine("stream.ValidateVerbatimText('}');");
            stream.WriteLine("return _result;");
        }
        #endregion

        #endregion

        #region Cpp

        #region Binary

        public void GenerateCpp(string outputDirectory, string prefix)
        {
            using (var stream = new LrpStream(outputDirectory, prefix + "TypesSerializer.hpp"))
            {
                this.GenerateCpp(stream);
            }
        }

        void GenerateCpp(LrpStream stream)
        {
            stream.WriteLine("namespace");
            stream.WriteLine("{");
            this.GenerateCppDeclaration(stream);
            stream.WriteLine("}");

            stream.WriteLine();

            stream.WriteLine("namespace");
            stream.WriteLine("{");
            this.GenerateCppImplementation(stream);
            this.GenerateCppThrow(stream);
            stream.WriteLine("}");
        }

        void GenerateCppImplementation(LrpStream stream)
        {
            foreach (var element in this.types)
            {
                this.GenerateCpp(stream, element);
            }
        }

        void GenerateCppDeclaration(LrpStream stream)
        {
            foreach (var element in this.types)
            {
                var type = element;
                if (!type.Embedded)
                {
                    stream.WriteLine("void Write{0}(const {1}& arg, MemoryBuffer& buffer);", type.Name, this.ToCppFullName(type));
                    stream.WriteLine("{0} Read{1}(MemoryBuffer& buffer);", this.ToCppFullName(type), type.Suffix);
                }
            }
        }

        void GenerateCpp(LrpStream stream, DataType type)
        {
            if (type.Embedded)
            {
                return;
            }
            stream.WriteLine("void Write{0}(const {1}& arg, MemoryBuffer& buffer)", type.Suffix, this.ToCppFullName(type));
            stream.WriteLine("{");

            this.GenerateCppWriting(stream, type);

            stream.WriteLine("}");
            stream.WriteLine("{0} Read{1}(MemoryBuffer& buffer)", this.ToCppFullName(type), type.Suffix);
            stream.WriteLine("{");
            this.GenerateCppReading(stream, type);
            stream.WriteLine("}");
        }

        void GenerateCppReading(LrpStream stream, DataType type)
        {
            var kind = type.Kind;
            if (kind == Kind.Enum32)
            {
                stream.WriteLine("auto result = ({0})ReadInt32(buffer);", type.CppFullName);
            }
            else if (kind == Kind.Class || kind == Kind.Struct)
            {
                stream.WriteLine("{0} result = {0}();", type.CppFullName);
            }
            else if (kind == Kind.Exception)
            {
                stream.WriteLine("auto _message = ReadAString(buffer);");
                stream.WriteLine("{0} result(_message);", type.CppFullName);
            }
            else if (kind == Kind.Array || kind == Kind.Vector)
            {
                var st = ToCppFullName(type);
                stream.WriteLine("const size_t count = buffer.ReadCount();");
                stream.WriteLine("{0} result;", st);
                stream.WriteLine("result.reserve(count);");
                stream.WriteLine("for(size_t index = 0; index < count; ++index)");
                stream.WriteLine("{");
                stream.WriteLine("result.push_back(Read{0}(buffer));", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            else if (kind == Kind.SMap || kind == Kind.HMap)
            {
                var st = this.ToCppFullName(type);
                stream.WriteLine("const size_t count = buffer.ReadCount();");
                stream.WriteLine("{0} result;", st);
                stream.WriteLine("for(size_t index = 0; index < count; ++index)");
                stream.WriteLine("{");
                stream.WriteLine("auto key = Read{0}(buffer);", this[type.Key].Suffix);
                stream.WriteLine("auto value = Read{0}(buffer);", this[type.Value].Suffix);
                stream.WriteLine("result[key] = value;");
                stream.WriteLine("}");
            }
            else if (kind == Kind.SSet)
            {
                var st = ToCppFullName(type);
                stream.WriteLine("const size_t count = buffer.ReadCount();");
                stream.WriteLine("{0} result;", st);
                stream.WriteLine("for(size_t index = 0; index < count; ++index)");
                stream.WriteLine("{");
                stream.WriteLine("result.insert(result.end(), Read{0}(buffer));", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            foreach (var element in type)
            {
                var memberType = this[element.TypeName];
                stream.WriteLine("result.{0} = Read{1}(buffer);", element.Name, memberType.Suffix);
            }
            stream.WriteLine("return result;");
        }

        void GenerateCppWriting(LrpStream stream, DataType type)
        {
            var kind = type.Kind;
            if (kind == Kind.Exception)
            {
                stream.WriteLine("WriteAString(arg.what(), buffer);");
            }
            else if (kind == Kind.Enum32)
            {
                stream.WriteLine("WriteInt32((__int32)arg, buffer);");
            }
            else if (kind == Kind.Array || kind == Kind.Vector || kind == Kind.SSet)
            {
                stream.WriteLine("WriteUInt32((unsigned __int32)arg.size(), buffer);");
                stream.WriteLine("for each(const auto element in arg)");
                stream.WriteLine("{");
                stream.WriteLine("Write{0}(element, buffer);", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            else if (kind == Kind.SMap || kind == Kind.HMap)
            {
                stream.WriteLine("WriteUInt32((unsigned __int32)arg.size(), buffer);");
                stream.WriteLine("for each(const auto element in arg)");
                stream.WriteLine("{");
                stream.WriteLine("Write{0}(element.first, buffer);", this[type.Key].Suffix);
                stream.WriteLine("Write{0}(element.second, buffer);", this[type.Value].Suffix);
                stream.WriteLine("}");
            }
            foreach (var element in type)
            {
                var memberType = this[element.TypeName];
                stream.WriteLine("Write{0}(arg.{1}, buffer);", memberType.Suffix, element.Name);
            }
        }

        void GenerateCppThrow(LrpStream stream)
        {
            stream.WriteLine("void Throw(HRESULT status, MemoryBuffer& buffer)");
            stream.WriteLine("{");
            stream.WriteLine("if(status >= 0)");
            stream.WriteLine("{");
            stream.WriteLine("return;");
            stream.WriteLine("}");
            stream.WriteLine("if(LRP_EXCEPTION != status)");
            stream.WriteLine("{");
            stream.WriteLine("throw std::exception(\"Unexpected exception has been encountered\");");
            stream.WriteLine("}");

            stream.WriteLine("const int _id = ReadInt32(buffer);");
            stream.WriteLine("_id;");
            var exceptionId = -1;
            foreach (var element in this.types)
            {
                if (!element.IsException)
                {
                    continue;
                }
                exceptionId++;
                stream.WriteLine("if({0} == _id)", exceptionId);
                stream.WriteLine("{");
                stream.WriteLine("throw Read{0}(buffer);", element.Suffix);
                stream.WriteLine("}");
            }
            stream.WriteLine("string _message = ReadAString(buffer);");
            stream.WriteLine("throw std::exception(_message.c_str());");
            stream.WriteLine("}");
        }

        #endregion

        #region Database

        public void GenerateCppWriter(LrpStream stream)
        {
            stream.WriteLine("namespace");
            stream.WriteLine("{");
            this.GenerateCppDeclarationWriter(stream);
            stream.WriteLine("}");
            stream.WriteLine("namespace");
            stream.WriteLine("{");
            this.GenerateCppImplementationWriter(stream);
            stream.WriteLine("}");
        }

        void GenerateCppDeclarationWriter(LrpStream stream)
        {
            foreach (var element in this.types)
            {
                var type = element;
                if (!type.Embedded)
                {
                    stream.WriteLine("void LrpWrite{0}(const char* name, const {1}& arg, std::ostream& _stream);", type.Name, ToCppFullName(type));
                    if (type.Kind == Kind.Array)
                    {
                        this.GenerateCppEmbeddedArrayWriter(type, stream);
                    }
                }
            }
        }

        void GenerateCppEmbeddedArrayWriter(DataType type, LrpStream stream)
        {
            var name = type.Name;
            type = this[type.Value];
            stream.WriteLine("template<size_t count> void LrpWrite{0}(const char* name, const {1}(&arg)[count], std::ostream& _stream)", name, ToCppFullName(type));
            stream.WriteLine("{");
            stream.WriteLine("if (nullptr != name)");
            stream.WriteLine("{");
            stream.WriteLine("_stream << name << \" = \";");
            stream.WriteLine("}");
            stream.WriteLine("_stream << \"[\" << count << \"]{\";");
            stream.WriteLine("const {0}* it = arg;", ToCppFullName(type));
            stream.WriteLine("const {0}* end = it + count;", ToCppFullName(type));
            stream.WriteLine("if (it != end)");
            stream.WriteLine("{");
            stream.WriteLine("LrpWrite{0}(nullptr, *it, _stream);", type.Suffix);
            stream.WriteLine("_stream << \";\";");
            stream.WriteLine("++it;");
            stream.WriteLine("}");
            stream.WriteLine("for (; it != end; ++it)");
            stream.WriteLine("{");
            stream.WriteLine("_stream << \" \";");
            stream.WriteLine("LrpWrite{0}(nullptr, *it, _stream);", type.Suffix);
            stream.WriteLine("_stream << \";\";");
            stream.WriteLine("}");
            stream.WriteLine("_stream << \"}\";");
            stream.WriteLine("}");
        }

        void GenerateCppImplementationWriter(LrpStream stream)
        {
            foreach (var element in this.types)
            {
                this.GenerateCppWriter(stream, element);
            }
        }

        void GenerateCppWriter(LrpStream stream, DataType type)
        {
            if (type.Embedded)
            {
                return;
            }
            stream.WriteLine("void LrpWrite{0}(const char* name, const {1}& arg, std::ostream& _stream)", type.Name, ToCppFullName(type));
            stream.WriteLine("{");

            stream.WriteLine("if (nullptr != name)");
            stream.WriteLine("{");
            stream.WriteLine("_stream << name << \" = \";");
            stream.WriteLine("}");
            if (type.Kind == Kind.Enum32)
            {
                stream.WriteLine("_stream<<static_cast<__int32>(arg);", type.Name, ToCppFullName(type));
            }
            else if (type.Kind == Kind.Class)
            {
                this.GenerateCppClassWriter(stream, type);
            }
            else if (type.Kind == Kind.Array || type.Kind == Kind.Vector)
            {
                this.GenerateCppArrayWriter(stream, type);
            }
            else if (type.Kind == Kind.SMap)
            {
                this.GenerateCppSMapWriter(stream, type);
            }
            else
            {
                var message = string.Format("Not supported kind = {0}", type.Kind);
                throw new ArgumentException(message);
            }
            stream.WriteLine("}");
        }

        void GenerateCppClassWriter(LrpStream stream, DataType type)
        {
            stream.WriteLine("_stream<<\"{\";");
            foreach (var element in type)
            {
                stream.WriteLine("LrpWrite{0}(\"{1}\", arg.{1}, _stream);", this[element.TypeName].Suffix, element.Name);
                stream.WriteLine("_stream << ';';");
            }
            stream.WriteLine("_stream<<\"}\";");
        }

        void GenerateCppSMapWriter(LrpStream stream, DataType type)
        {
            stream.WriteLine("_stream << \"[\" << arg.size() << \"]{\";");
            stream.WriteLine("auto it = arg.begin();");
            stream.WriteLine("auto end = arg.end();");
            stream.WriteLine("if (it != end)");
            stream.WriteLine("{");
            stream.WriteLine("LrpWrite{0}(nullptr, it->first, _stream);", this[type.Key].Suffix);
            stream.WriteLine("_stream << \" = \";");
            stream.WriteLine("LrpWrite{0}(nullptr, it->second, _stream);", this[type.Value].Suffix);
            stream.WriteLine("_stream << \";\";");
            stream.WriteLine("++it;");
            stream.WriteLine("}");
            stream.WriteLine("for (; it != end; ++it)");
            stream.WriteLine("{");
            stream.WriteLine("_stream << \" \";");
            stream.WriteLine("LrpWrite{0}(nullptr, it->first, _stream);", this[type.Key].Suffix);
            stream.WriteLine("_stream << \" = \";");
            stream.WriteLine("LrpWrite{0}(nullptr, it->second, _stream);", this[type.Value].Suffix);
            stream.WriteLine("_stream << \";\";");
            stream.WriteLine("}");
            stream.WriteLine("_stream << \"}\";");
        }

        void GenerateCppArrayWriter(LrpStream stream, DataType type)
        {
            stream.WriteLine("_stream << \"[\" << arg.size() << \"]{\";");
            stream.WriteLine("auto it = LrpBeginIterator(arg);");
            stream.WriteLine("auto end = LrpEndIterator(arg);");
            stream.WriteLine("if (it != end)");
            stream.WriteLine("{");
            stream.WriteLine("LrpWrite{0}(nullptr, *it, _stream);", this[type.Value].Suffix);
            stream.WriteLine("_stream << \";\";");
            stream.WriteLine("++it;");
            stream.WriteLine("}");
            stream.WriteLine("for (; it != end; ++it)");
            stream.WriteLine("{");
            stream.WriteLine("_stream << \" \";");
            stream.WriteLine("LrpWrite{0}(nullptr, *it, _stream);", this[type.Value].Suffix);
            stream.WriteLine("_stream << \";\";");
            stream.WriteLine("}");
            stream.WriteLine("_stream << \"}\";");
        }
        #endregion

        #endregion

        #region IEnumerable

        public IEnumerator<DataType> GetEnumerator()
        {
            return this.types.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.types.GetEnumerator();
        }

        #endregion

        #region Fields

        readonly List<DataType> types = new List<DataType>();
        readonly Dictionary<string, DataType> nameToType = new Dictionary<string, DataType>();

        #endregion
    }
}
