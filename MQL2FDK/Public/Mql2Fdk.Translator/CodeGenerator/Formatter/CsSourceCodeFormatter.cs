using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace Mql2Fdk.Translator.CodeGenerator.Formatter
{
    public static class CsSourceCodeFormatter
    {
        public static string BeautifyCsFileSource(this string sourceText)
        {
            var parser = new CSharpParser();
            var syntaxTree = parser.Parse(sourceText);
            return syntaxTree.GetText();
        }

        public static List<Error> CsFileErrors(this string sourceText)
        {
            var parser = new CSharpParser();
            var syntaxTree = parser.Parse(sourceText);
            return syntaxTree.Errors;
        }

        public static string ErrorPrettyPrinter(this Error err)
        {
            return string.Format("Error ({0}:{1}): {2} ", err.Region.BeginLine, err.Region.BeginColumn, err.Message);
        }
    }
}