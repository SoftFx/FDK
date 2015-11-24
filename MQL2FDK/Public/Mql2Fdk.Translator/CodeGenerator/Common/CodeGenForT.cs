using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    abstract class CodeGenForT : CodeGenForNode
    {
        readonly RuleKind _rule;

        protected CodeGenForT(RuleKind rule)
        {
            _rule = rule;
        }

        public override bool Accept(ParseNode node)
        {
            return node.Rule == _rule;
        }
    }
}