namespace RealTimeLevel2
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using SoftFX.Extended;

    [DataContract]
    class Level2
    {
        [DataMember]
        public DateTime CreatingTime;

        [DataMember]
        public string Symbol;

        [DataMember]
        public Level2Entry[] Asks;

        [DataMember]
        public Level2Entry[] Bids;

        public Level2(Quote quote, SymbolInfo info)
        {
            this.CreatingTime = quote.CreatingTime;
            this.Symbol = quote.Symbol;
            this.Bids = Level2EntriesFromQuoteEntries(quote.Bids, info.RoundLot);
            this.Asks = Level2EntriesFromQuoteEntries(quote.Asks, info.RoundLot);
        }

        static Level2Entry[] Level2EntriesFromQuoteEntries(QuoteEntry[] entries, double lot)
        {
            return entries.Select(o => new Level2Entry(o, lot))
                          .ToArray();
        }

        public string ToJson()
        {
            using (var stream = new MemoryStream())
            {
                this.serializator.WriteObject(stream, this);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }

        #region Members

        readonly DataContractJsonSerializer serializator = new DataContractJsonSerializer(typeof(Level2));

        #endregion
    }
}
