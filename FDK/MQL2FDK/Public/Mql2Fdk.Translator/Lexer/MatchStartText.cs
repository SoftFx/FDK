namespace Mql2Fdk.Translator.Lexer
{
    public class MatchStartText : IMatcher
    {
        readonly string _startingText;

        public MatchStartText(string startingText)
        {
            _startingText = startingText;
        }

        public int Match(string text)
        {
            var startsWith = text.StartsWith(_startingText);
            if (!startsWith)
                return 0;
            return text.Length;
        }
    }
}