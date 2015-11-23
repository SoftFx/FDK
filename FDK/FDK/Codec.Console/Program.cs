namespace Codec.Console
{
    using System.IO;
    using LrpServer.Net;
    using SoftFX.Extended;
    using SoftFX.Internal.Codecs;
    using SoftFX.Lrp;

    static class Program
    {
        public static LrpQuoteEntry ReadQuoteEntry(this TextStream stream, string name = null)
        {
            if (name != null)
            {
                stream.ValidateVerbatimText(name);
                stream.ValidateVerbatimText(" = ");
            }

            var result = new LrpQuoteEntry();
            stream.ValidateVerbatimText('{');
            result.Price = stream.ReadDouble("Price");
            stream.ValidateVerbatimText(';');
            result.Volume = stream.ReadDouble("Volume");
            stream.ValidateVerbatimText(';');
            stream.ValidateVerbatimText('}');
            return result;
        }

        public static LrpQuoteEntry[] ReadQuoteEntryArray(this TextStream stream, string name = null)
        {
            if (name != null)
            {
                stream.ValidateVerbatimText(name);
                stream.ValidateVerbatimText(" = ");
            }

            stream.ValidateVerbatimText('[');
            var count = stream.ReadInt32();

            stream.ValidateVerbatimText("]{");
            var result = new LrpQuoteEntry[count];
            if (count > 0)
            {
                var _value = stream.ReadQuoteEntry(null);
                result[0] = _value;
                stream.ValidateVerbatimText(";");
            }

            for (int index = 1; index < count; ++index)
            {
                stream.ValidateVerbatimText(" ");
                var _value = stream.ReadQuoteEntry(null);
                result[index] = _value;
                stream.ValidateVerbatimText(";");
            }
            stream.ValidateVerbatimText('}');
            return result;
        }

        public static LrpQuote ReadQuote(this TextStream stream, string name = null)
        {
            if (null != name)
            {
                stream.ValidateVerbatimText(name);
                stream.ValidateVerbatimText(" = ");
            }
            var result = new LrpQuote();
            stream.ValidateVerbatimText('{');
            result.Symbol = stream.ReadAString("Symbol");
            stream.ValidateVerbatimText(';');
            result.CreatingTime = stream.ReadTime("CreatingTime");
            stream.ValidateVerbatimText(';');
            result.Bids = stream.ReadQuoteEntryArray("Bids");
            stream.ValidateVerbatimText(';');
            result.Asks = stream.ReadQuoteEntryArray("Asks");
            stream.ValidateVerbatimText(';');
            result.Id = stream.ReadAString("Id");
            stream.ValidateVerbatimText(';');
            stream.ValidateVerbatimText('}');
            return result;
        }

        static Quote Parse(string text)
        {
            var stream = new TextStream();
            stream.Initialize(text);
            var lrpQuote = stream.ReadQuote("quote");

            var bids = new QuoteEntry[lrpQuote.Bids.Length];
            var asks = new QuoteEntry[lrpQuote.Asks.Length];

            for (int index = 0; index < lrpQuote.Bids.Length; ++index)
            {
                bids[index] = new QuoteEntry(lrpQuote.Bids[index].Price, lrpQuote.Bids[index].Volume);
            }

            for (int index = 0; index < lrpQuote.Asks.Length; ++index)
            {
                asks[index] = new QuoteEntry(lrpQuote.Asks[index].Price, lrpQuote.Asks[index].Volume);
            }

            var result = new Quote(lrpQuote.Symbol, lrpQuote.CreatingTime, bids, asks);
            return result;

        }

        public static void Main(string[] args)
        {
            Library.Path = "<FRE>";

            string line = null;
            using (var stream = new StreamReader(@"E:\Output\codec0.txt"))
            {
                line = stream.ReadLine();
            }

            var quote = Parse(line);

            using (var codec = new LrpCodec())
            {
                
                var quotes = new Quote[] { quote };

                codec.EncodeSlow(quotes);
            }
        }
    }
}
