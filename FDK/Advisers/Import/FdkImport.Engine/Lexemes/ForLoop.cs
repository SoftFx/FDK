namespace FdkImport.Engine.Lexemes
{
    using System.Text.RegularExpressions;

    class ForLoop : ILexeme
    {
        public string Process(string code)
        {
            var result = this.pattern.Replace(code, Evaluate);
            return result;
        }

        string Evaluate(Match match)
        {
            var value = match.Groups[2].Value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                var result = value;
                result += ";\r\n";
                value = match.Value.Replace(value, string.Empty);
                result += value;
                return result;
            }
            else
            {
                return match.Value;
            }
        }

        #region Members

        readonly Regex pattern = new Regex(@"(^|\s*)for\s*\(([^;]+);");

        #endregion
    }
}
