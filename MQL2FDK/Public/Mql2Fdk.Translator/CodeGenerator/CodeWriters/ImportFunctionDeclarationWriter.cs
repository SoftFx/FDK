using System;
using Mql2Fdk.Translator.CodeGenerator.Common;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.CodeWriters
{
    class ImportFunctionDeclarationWriter : CodeGenForT
    {
        public ImportFunctionDeclarationWriter()
            : base(RuleKind.ImportFunction)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            var parentNode = node.Parent;
            var pInvokeImport = string.Format("[DllImport(@{0}, CharSet = CharSet.Ansi)]", parentNode.Content);
            return string.Format("{0}{1}private static extern ", pInvokeImport, Environment.NewLine);
        }
    }
}