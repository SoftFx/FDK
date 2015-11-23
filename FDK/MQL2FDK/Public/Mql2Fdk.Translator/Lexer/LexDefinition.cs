using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer
{
    public class LexDefinition
    {
        public readonly IMatcher Matcher;
        public readonly TokenKind Token;

        public LexDefinition(string regex, TokenKind token)
        {
            Matcher = new RegexMatcher(regex);
            Token = token;
        }

        public LexDefinition(IMatcher startMatcher, TokenKind token)
        {
            Matcher = startMatcher;
            Token = token;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Matcher, Token);
        }
    }
}