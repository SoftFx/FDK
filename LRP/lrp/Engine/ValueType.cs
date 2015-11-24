namespace Lrp.Engine
{
    [XmlName("Value")]
    class ValueType
    {
        [XmlSerializable()]
        public string Name { get; private set; }
    }
}
