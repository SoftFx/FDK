namespace RealTimeLevel2
{
    using System.Runtime.Serialization;
    using SoftFX.Extended;

    [DataContract]
    struct Level2Entry
    {
        [DataMember]
        public double Price;

        [DataMember]
        public double Volume;

        public Level2Entry(QuoteEntry entry, double lot)
            : this()
        {
            this.Price = entry.Price;
            this.Volume = entry.Volume / lot;
        }
    }
}
