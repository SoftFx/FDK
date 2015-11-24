namespace Mql2Fdk.Translator.Lexer
{
    public class HexMatcher : IMatcher
    {
        public int Match(string text)
        {
            if (!text.StartsWith("0x"))
                return 0;
            for (var i = 2; i < text.Length; i++)
            {
                var c = text[i];
                var isHex = ((c >= '0' && c <= '9') ||
                             (c >= 'a' && c <= 'f') ||
                             (c >= 'A' && c <= 'F'));
                if (!isHex)
                    return i;
            }
            return text.Length;
        }
    }
}