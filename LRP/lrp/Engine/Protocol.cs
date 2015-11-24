namespace Lrp.Engine
{
    using System;
    using System.Xml;
    using Lrp.Engine.Formating;

    class Protocol
    {
        #region Construction

        public Protocol(string path)
        {
            var document = new XmlDocument();
            document.Load(path);

            var root = document.DocumentElement;
            Clean(root);
            Construct(root);
        }

        static void Clean(XmlNode node)
        {
            var count = node.ChildNodes.Count;
            for (var index = count - 1; index >= 0; --index)
            {
                var child = node.ChildNodes[index];
                if (child.NodeType != XmlNodeType.Element)
                {
                    node.RemoveChild(child);
                }
            }

            count = node.ChildNodes.Count;
            for (var index = 0; index < count; ++index)
            {
                var child = node.ChildNodes[index];
                Clean(child);
            }
        }

        public Protocol(XmlNode root)
        {
            this.Construct(root);
        }

        void Construct(XmlNode root)
        {
            XmlSerializer.Initialize(this, root);
            var count = root.ChildNodes.Count;
            if (count == 1)
            {
                var node = root.ChildNodes[0];
                this.types = new DataTypes();
                this.Components = new Components(this.types, node);
            }
            else if (count == 2)
            {
                var first = root.ChildNodes[0];
                var second = root.ChildNodes[1];
                this.types = new DataTypes(first);
                this.Components = new Components(this.types, second);
            }
            else
            {
                var message = string.Format("Unsupported number of children nodes for protocol element: value = {0}; expeted one or two.", count);
                throw new Exception(message);
            }
        }

        #endregion

        #region Generation

        public void GenerateCharpLocalClient(string namespaceName, string outputDirectory)
        {
            this.types.GenerateCSharp(namespaceName, outputDirectory);
            this.Components.GenerateCharpClient(true, namespaceName, outputDirectory);
        }

        public void GenerateCharpLocalServer(string namespaceName, string outputDirectory)
        {
            this.types.GenerateCSharp(namespaceName, outputDirectory);
            this.Components.GenerateCharpServer(true, namespaceName, outputDirectory);
        }

        public void GenerateCharpRemoteClient(string namespaceName, string outputDirectory)
        {
            this.types.GenerateCSharp(namespaceName, outputDirectory);
            this.Components.GenerateCharpClient(false, namespaceName, outputDirectory);
        }

        public void GenerateCSharpReader(string namespaceName, string path)
        {
            using (var stream = new LrpStream(path))
            {
                stream.WriteLine("using System;");
                stream.WriteLine("using SoftFX.Lrp;");
                stream.WriteLine("using System.Collections.Generic;");
                stream.WriteLine();
                this.types.GenerateCSharpReader(namespaceName, stream);
                this.Components.GenerateCSharpReader(namespaceName, stream);
            }
        }

        public void GenerateCppRemoteClient(string outputDirectory, string prefix)
        {
            this.types.GenerateCpp(outputDirectory, prefix);
            this.Components.GenerateCppRemoteClient(outputDirectory, prefix);
        }

        public void GenerateCppLocalClient(string outputDirectory, string prefix)
        {
            this.types.GenerateCpp(outputDirectory, prefix);
            this.Components.GenerateCppLocalClient(outputDirectory, prefix);
        }

        public void GenerateCppLocalServer(string outputDirectory, string prefix)
        {
            this.types.GenerateCpp(outputDirectory, prefix);
            this.Components.GenerateCppServer(true, this.types, outputDirectory, prefix);
        }

        public void GenerateCppRemoteServer(string outputDirectory, string prefix)
        {
            this.types.GenerateCpp(outputDirectory, prefix);
            this.Components.GenerateCppServer(false, this.types, outputDirectory, prefix);
        }

        public void GenerateCppWriter(string outputPath)
        {
            this.Components.GenerateCppHeaderWriter(outputPath + ".h");
            using (var stream = new LrpStream(outputPath + ".hpp"))
            {
                this.types.GenerateCppWriter(stream);
                this.Components.GenerateCppSourceWriter(stream);
            }
        }

        #endregion

        #region Verification

        public bool Compare(Protocol other)
        {
            var result = true;
            foreach (var element in this.Components)
            {
                if (!this.Compare(element, other))
                {
                    result = false;
                }
            }
            return result;
        }

        bool Compare(Component component, Protocol other)
        {
            foreach (var element in other.Components)
            {
                if (component.Name == element.Name)
                {
                    return component.Compare(element);
                }
            }
            Console.WriteLine("Component {0} does not exist", component.Name);
            return false;
        }

        #endregion

        #region properties

        public Components Components { get; private set; }

        [XmlSerializable(false)]
        public string Namespace { get; private set; }

        #endregion

        #region members

        DataTypes types;

        #endregion
    }
}
