namespace FdkImport.Engine.Lexemes
{
    using System.Globalization;
    using System.Text.RegularExpressions;

    class DefineLexeme : ILexeme
    {
        public string Process(string code)
        {
            var result = this.pattern.Replace(code, this.Evaluate);
            return result;
        }

        string Evaluate(Match match)
        {
            var space0 = match.Groups[1].Value;
            var name = match.Groups[2].Value;
            var space1 = match.Groups[3].Value;
            var value = match.Groups[4].Value;
            var type = "string";

            if (IsInt32(value))
                type = "int";
            else if (IsDouble(value))
                type = "double";

            var result = string.Format("private const {0} {1}{2} ={3}{4};", type, space0, name, space1, value);
            return result;
        }

        static bool IsDouble(string st)
        {
            var value = 0D;
            var result = double.TryParse(st, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value);
            return result;
        }

        static bool IsInt32(string st)
        {
            var value = 0;
            var result = int.TryParse(st, out value);
            return result;
        }

        #region Members

        readonly Regex pattern = new Regex(@"#define(\s+)(\w+)(\s+)(.+)", RegexOptions.Compiled);

        #endregion
    }
}
