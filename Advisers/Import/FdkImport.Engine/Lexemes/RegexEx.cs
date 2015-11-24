namespace FdkImport.Engine.Lexemes
{
    using System.Text.RegularExpressions;

    class RegexEx
    {
        public RegexEx(string replacing, string replacement)
        {
            var pattern = string.Format(@"(^|\W)({0})($|\W)", replacing);
            this.pattern = new Regex(pattern, RegexOptions.Compiled);
            this.replacing = replacing;
            this.replacement = replacement;
        }

        public string Replace(string text)
        {
            var result = this.pattern.Replace(text, this.Evaluate);
            return result;
        }

        string Evaluate(Match match)
        {
            var result = match.Value.Replace(this.replacing, this.replacement);
            return result;
        }

        #region members

        readonly Regex pattern;
        readonly string replacing;
        readonly string replacement;

        #endregion
    }
}
