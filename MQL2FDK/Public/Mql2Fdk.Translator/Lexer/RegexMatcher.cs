using System.Text.RegularExpressions;

namespace Mql2Fdk.Translator.Lexer
{
    sealed class RegexMatcher : IMatcher
    {
        readonly Regex _regex;

        public RegexMatcher(string regex)
        {
            _regex = new Regex(string.Format("^{0}", regex));
        }

        public int Match(string text)
        {
            var m = _regex.Match(text);
            return m.Success ? m.Length : 0;
        }

        public override string ToString()
        {
            return _regex.ToString();
        }
    }
}