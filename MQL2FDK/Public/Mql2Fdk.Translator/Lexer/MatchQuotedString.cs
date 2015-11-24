using System.IO;

namespace Mql2Fdk.Translator.Lexer
{
    public class MatchQuotedString : IMatcher
    {
        public int Match(string text)
        {
            if (text[0] != ('\"'))
                return 0;
            var isEscaping = false;
            for (var i = 1; i < text.Length; i++)
            {
                if ((text[i] == '\\'))
                {
                    isEscaping = !isEscaping;
                    continue;
                }

                if ((text[i] == '\"') && !isEscaping)
                    return i + 1;
                if (isEscaping)
                {
                    isEscaping = false;
                }
            }
            throw new InvalidDataException("Invalid string");
        }
    }
}