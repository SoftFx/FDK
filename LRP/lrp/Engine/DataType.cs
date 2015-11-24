namespace Lrp.Engine
{
    using System;
    using System.Xml;

    [XmlName("Type")]
    class DataType : ListSet<Member>, INamed
    {
        public bool Embedded
        {
            get
            {
                var result = !string.IsNullOrEmpty(this.suffix);
                return result;
            }
        }

        #region Properties

        [XmlSerializable]
        public string Name { get; private set; }

        [XmlSerializable(false)]
        public string CSharpFullName { get; private set; }

        [XmlSerializable(false)]
        public string CppFullName { get; private set; }

        [XmlSerializable]
        public Kind Kind { get; private set; }

        #endregion

        #region Properties

        public string Suffix
        {
            get
            {
                if (string.IsNullOrEmpty(this.suffix))
                {
                    return this.Name;
                }
                return this.suffix;
            }
        }

        public bool IsException
        {
            get
            {
                return this.Kind == Kind.Exception;
            }
        }

        public bool IsEnum32
        {
            get
            {
                return this.Kind == Kind.Enum32;
            }
        }

        public string Value { get; private set; }

        public string Key { get; private set; }

        #endregion

        public DataType(XmlNode node)
        {
            XmlSerializer.Initialize(this, node);
            this.Construct(this.Name, this.CSharpFullName, this.CppFullName, string.Empty);
            var kind = this.Kind;
            if (kind == Kind.Class || kind == Kind.Struct || kind == Kind.Enum32 || kind == Kind.Exception)
            {
                this.InitializeMembers(node);
            }
            else if (kind == Kind.Array || kind == Kind.Vector || kind == Kind.SSet)
            {
                this.InitializeValue(node);
            }
            else if (kind == Kind.SMap || kind == Kind.HMap)
            {
                this.InitializeKeyValue(node);
            }
            else
            {
                var message = string.Format("Invalid kind = {0} for type = {1}", kind, this.Name);
                throw new Exception(message);
            }
        }

        public DataType(string name, string csharpFullName, string cppFullName, string alias)
        {
            this.Construct(name, csharpFullName, cppFullName, alias);
        }

        void Construct(string name, string csharpFullName, string cppFullName, string alias)
        {
            this.Name = name;
            this.suffix = alias;
            this.CSharpFullName = csharpFullName;
            this.CppFullName = cppFullName;
        }

        void InitializeMembers(XmlNode node)
        {
            var nodes = node.ChildNodes;
            var count = nodes.Count;
            for (var index = 0; index < count; ++index)
            {
                var childNode = nodes[index];
                var member = new Member(childNode);
                this.Add(member);
            }
        }

        void InitializeValue(XmlNode node)
        {
            var nodes = node.ChildNodes;
            if (nodes.Count != 1)
            {
                var message = string.Format("Invalid number of child nodes for type = {0}", this.Name);
                throw new Exception(message);
            }
            var child = nodes[0];
            var value = new ValueType();
            XmlSerializer.Initialize(value, child);
            this.Value = value.Name;
        }

        void InitializeKey(XmlNode node)
        {
            var nodes = node.ChildNodes;
            if (nodes.Count != 1)
            {
                var message = string.Format("Invalid number of child nodes for type = {0}", this.Name);
                throw new Exception(message);
            }
            var child = nodes[0];
            var key = new KeyType();
            XmlSerializer.Initialize(key, child);
            this.Key = key.Name;
        }

        void InitializeKeyValue(XmlNode node)
        {
            var nodes = node.ChildNodes;
            if (nodes.Count != 2)
            {
                var message = string.Format("Invalid number of child nodes for type = {0}", this.Name);
                throw new Exception(message);
            }

            var key = new KeyType();
            var value = new ValueType();

            XmlSerializer.Initialize(key, nodes[0]);
            XmlSerializer.Initialize(value, nodes[1]);

            this.Key = key.Name;
            this.Value = value.Name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region Fields

        string suffix;

        #endregion
    }
}
