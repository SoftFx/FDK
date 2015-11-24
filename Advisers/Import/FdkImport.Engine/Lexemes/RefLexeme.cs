namespace FdkImport.Engine.Lexemes
{
    using System.Text.RegularExpressions;

    class RefLexeme : ILexeme
    {
        public string Process(string code)
        {
            var result = this.pattern.Replace(code, this.Evaluate);
            return result;
        }

        private string Evaluate(Match match)
        {
            var type = match.Groups[2].Value;
            var result = match.Value;
            result = result.Replace("&", string.Empty);
            var replacement = string.Format("ref {0}", type);
            result = result.Replace(type, replacement);
            return result;
        }

        #region members

        readonly Regex pattern = new Regex(@"(\s|,)([a-z]+)\s*\&", RegexOptions.Compiled);

        #endregion
    }
}
