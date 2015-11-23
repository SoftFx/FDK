namespace Mql2Fdk.Translator.Lexer
{
    public class UnicodeIdentifierMatcher : IMatcher
    {
        static bool IsInAnsiRange(char ch)
        {
            return ch < 128;
        }

        static bool IsValidFirstCharId(char ch)
        {
            if (ch == '_' || ch == '$')
                return true;
            if (ch >= 'a' && ch <= 'z')
                return true;

            if (ch >= 'A' && ch <= 'Z')
                return true;
            if (!IsInAnsiRange(ch))
                return true;
            return false;
        }


        public static bool IsValidIdNotFirstChar(char ch)
        {
            if (IsValidFirstCharId(ch))
                return true;
            if (ch >= '0' && ch <= '9')
                return true;
            if (ch == '.')
                return true;
            return false;
        }

        public int Match(string text)
        {
            if (!IsValidFirstCharId(text[0]))
                return 0;
            for (var i = 1; i < text.Length; i++)
            {
                var ch = text[i];
                if (!IsValidIdNotFirstChar(ch))
                    return i;
            }
            return text.Length;
        }
    }
}