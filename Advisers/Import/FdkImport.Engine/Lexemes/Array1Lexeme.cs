namespace FdkImport.Engine.Lexemes
{
    using System.Text.RegularExpressions;

    class Array1Lexeme : ILexeme
    {
        public string Process(string code)
        {
            var result = this.pattern.Replace(code, this.Evaluate);
            return result;
        }

        string Evaluate(Match match)
        {
            var type = match.Groups[2].Value;
            var name = match.Groups[3].Value;
            var count = match.Groups[4].Value;
            var result = match.Value;
            result = result.Replace(count, string.Empty);
            result = result.Replace("[", string.Empty);
            result = result.Replace("]", string.Empty);

            var replacement = string.Format("{0}[]", type);
            result = result.Replace(type, replacement);
            replacement = string.Format(" = new {0}[{1}];", type, count);
            result = result.Replace(";", replacement);
            return result;
        }

        #region Members

        readonly Regex pattern = new Regex(@"(^|\s)([a-z]+)\s+(\w+)\s*\[([^\]]+)\]\s*;", RegexOptions.Compiled);

        #endregion
    }
}
