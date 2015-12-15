using Mql2Fdk.Translator.CodeGenerator.Common;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.CodeWriters
{
    class DefineDeclarationWriter : CodeGenForT
    {
        public DefineDeclarationWriter()
            : base(RuleKind.DeclareConstant)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            return "const ";
        }
    }
}