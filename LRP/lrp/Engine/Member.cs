namespace Lrp.Engine
{
    using System.Xml;

    class Member : INamed
    {
        [XmlSerializable()]
        public string Name { get; private set; }

        [XmlSerializable(), XmlName("type")]
        public string TypeName { get; private set; }

        [XmlSerializable(false)]
        public string Default { get; private set; }

        public Member(XmlNode node)
        {
            XmlSerializer.Initialize(this, node);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
