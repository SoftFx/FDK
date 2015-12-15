namespace FdkImport.Engine.Lexemes
{
    using System.Text.RegularExpressions;

    class PropertyLexeme : ILexeme
    {
        public PropertyLexeme()
        {
        }

        public string Process(string code)
        {
            var result = this.pattern.Replace(code, this.Evaluate);
            return result;
        }

        string Evaluate(Match match)
        {
            var prolog = match.Groups[1].Value;
            var key = match.Groups[2].Value;
            var value = match.Groups[3].Value;
            var result = string.Format("{0} public readonly static object {1} = {2};", prolog, key, value);
            return result;
        }

        #region Members

        readonly Regex pattern = new Regex(@"(^|\s+)#property\s+(\w+)\s+([^\s].*)(//.*|$)", RegexOptions.Compiled);

        #endregion
    }
}
