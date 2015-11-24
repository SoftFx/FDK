using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    class EmtpyCodeGenForT : CodeGenForNode
    {
        readonly RuleKind _rule;

        public EmtpyCodeGenForT(RuleKind rule)
        {
            _rule = rule;
        }

        public override bool Accept(ParseNode node)
        {
            return node.Rule == _rule;
        }

        public override string DoWrite(ParseNode node)
        {
            return string.Empty;
        }
    }
}