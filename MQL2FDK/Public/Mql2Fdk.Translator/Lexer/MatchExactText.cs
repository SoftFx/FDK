using System;

namespace Mql2Fdk.Translator.Lexer
{
    public class MatchExactText : IMatcher
    {
        readonly string _startingText;

        public MatchExactText(string startingText)
        {
            _startingText = startingText;
        }

        public int Match(string text)
        {
            return text.StartsWith(_startingText, StringComparison.Ordinal)
                       ? _startingText.Length
                       : 0;
        }
    }
}