using Mql2Fdk.Translator.CodeGenerator.Common;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.CodeWriters
{
    class ExternWriter : CodeGenForT
    {
        public ExternWriter() : base(RuleKind.Extern)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            return " public ";
        }
    }
}