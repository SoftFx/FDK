namespace Lrp.Engine
{
    using System;
    using System.Xml;
    using Lrp.Engine.Formating;

    [XmlName("Param")]
    class Parameter : INamed
    {
        #region Properties
        [XmlSerializable]
        public string Name { get; private set; }

        public int Number { get; private set; }

        [XmlSerializable()]
        public ParameterDirection Direction { get; private set; }

        [XmlSerializable()]
        [XmlName("Type")]
        private string TypeName { get; set; }

        [XmlSerializable(false)]
        public string Default { get; private set; }

        public bool HasDefaultValue
        {
            get
            {
                return this.Default != null;
            }
        }


        public int Id { get; private set; }

        public DataType Type { get; private set; }


        #endregion

        public Parameter(int id, DataTypes types, XmlNode node)
        {
            this.Id = id;
            XmlSerializer.Initialize(this, node);
            this.Type = types[TypeName];
        }

        public void GenerateCSharpSignature(DataTypes types, LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut)
            {
                stream.Write("ref ");
            }
            else if (this.Direction == ParameterDirection.Out)
            {
                stream.Write("out ");
            }
            stream.Write("{0} {1}", types.ToCSharpFullName(this.Type), this.Name);
        }

        public void GenerateCSharpArgumentByName(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut)
            {
                stream.Write("ref ");
            }
            else if (this.Direction == ParameterDirection.Out)
            {
                stream.Write("out ");
            }
            stream.Write("{0}", this.Name);
        }
        public void GenerateCSharpAgrgumentById(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut)
            {
                stream.Write("ref ");
            }
            else if (this.Direction == ParameterDirection.Out)
            {
                stream.Write("out ");
            }
            stream.Write("arg{0}", this.Id);
        }

        public void GenerateCppSignature(DataTypes types, LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut || this.Direction == ParameterDirection.Out)
            {
                stream.Write("{0}& {1}", types.ToCppFullName(this.Type), this.Name);
            }
            else if (this.Type == EmbeddedDataTypes.LocalPointer || this.Type == EmbeddedDataTypes.RemotePointer)
            {
                stream.Write("{0} {1}", types.ToCppFullName(this.Type), this.Name);
            }
            else
            {
                stream.Write("const {0}& {1}", types.ToCppFullName(this.Type), this.Name);
            }
            
        }

        #region CSharp

        #region Binary

        public void GenerateCSharpRequest(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.In || this.Direction == ParameterDirection.InOut)
                stream.WriteLine("buffer.Write{0}({1});", this.Type.Suffix, this.Name);
        }

        public void GenerateCSharpServerRequest(DataTypes types, LrpStream stream)
        {
            var type = this.Type;
            stream.BeginLine();
            stream.Write("var arg{0} = ", this.Id);
            if (this.Direction == ParameterDirection.In || this.Direction == ParameterDirection.InOut)
            {
                stream.Write("buffer.Read{0}();", this.Type.Suffix);
            }
            else if (this.Direction == ParameterDirection.Out)
            {
                if (type == EmbeddedDataTypes.LocalPointer)
                {
                    stream.Write("Lptr.Zero;");
                }
                else if (type == EmbeddedDataTypes.RemotePointer)
                {
                    stream.Write("Rptr.Zero;");
                }
                else
                {
                    stream.Write("default({0});", types.ToCSharpFullName(this.Type));
                }
            }
            stream.EndLine();
        }

        public void GenerateCSharpServerResponse(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut || this.Direction == ParameterDirection.Out)
                stream.WriteLine("buffer.Write{0}(arg{1});", this.Type.Suffix, this.Id);
        }

        public void GenerateCSharpResponse(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut || this.Direction == ParameterDirection.Out)
                stream.WriteLine("{0} = buffer.Read{1}();", this.Name, this.Type.Suffix);
        }

        #endregion

        #region Database

        public void GenerateCSharpSignatureReader(LrpStream stream, DataTypes types)
        {
            var typeFullname = types.ToCSharpFullName(this.Type);
            stream.Write("{0} {1}", typeFullname, this.Name);
        }

        public void GenerateCSharpMemberReader(LrpStream stream)
        {
            var defaultValue = this.Default;
            if (defaultValue == null)
                stream.WriteLine("var {0} = stream.Read{1}(\"{0}\");", this.Name, this.Type.Suffix);
            else
                stream.WriteLine("var {0} = stream.Read{1}(\"{0}\", {2});", this.Name, this.Type.Suffix, defaultValue);
        }

        #endregion

        #endregion

        #region Cpp Client

        public void GenerateCppRequest(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.In || this.Direction == ParameterDirection.InOut)
                stream.WriteLine("Write{0}({1}, buffer);", this.Type.Suffix, this.Name);
        }

        public void GenerateCppResponse(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut || this.Direction == ParameterDirection.Out)
            {
                stream.WriteLine("{0} = Read{1}(buffer);", this.Name, this.Type.Suffix);
            }
        }

        #endregion

        #region Cpp Local Server

        public void GenerateCppServerRequest(DataTypes types, LrpStream stream)
        {
            var type = this.Type;
            stream.BeginLine();
            if (type == EmbeddedDataTypes.Raw)
                stream.Write("auto& arg{0} = ", this.Id);
            else
                stream.Write("auto arg{0} = ", this.Id);
            
            if (this.Direction == ParameterDirection.In || this.Direction == ParameterDirection.InOut)
            {
                stream.Write("Read{0}(buffer);", this.Type.Suffix);
            }
            else if (this.Direction == ParameterDirection.Out)
            {
                if (type != EmbeddedDataTypes.LocalPointer && type != EmbeddedDataTypes.RemotePointer)
                {
                    stream.Write("{0}();", types.ToCppFullName(this.Type));
                }
                else
                {
                    stream.Write("(void*)nullptr;");
                }
            }
            stream.EndLine();
        }

        public void GenerateCppLocalServerResponse(LrpStream stream)
        {
            if (this.Direction == ParameterDirection.InOut || this.Direction == ParameterDirection.Out)
                stream.WriteLine("Write{0}(arg{1}, buffer);", this.Type.Suffix, this.Id);
        }

        #endregion

        #region Cpp Writer

        public void GenerateCppHeaderWriter(DataTypes types, LrpStream stream)
        {
            var cppType = types.ToCppFullName(this.Type);
            stream.Write("const {0}& {1}", cppType, this.Name);
        }

        public void GenerateCppSourceWriter(DataTypes types, LrpStream stream)
        {
            stream.WriteLine("LrpWrite{0}(\"{1}\", {1}, _stream);", this.Type.Suffix, this.Name);
        }

        #endregion
    }
}
