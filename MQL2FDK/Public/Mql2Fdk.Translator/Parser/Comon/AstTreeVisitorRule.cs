using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Parser.Comon
{
    class AstTreeVisitorRule : AstTreeVisitor
    {
        readonly RuleKind _rule;

        public AstTreeVisitorRule(ParseNode node, RuleKind rule)
            : base(node)
        {
            _rule = rule;
        }

        protected override void VisitAstNode(ParseNode parentNode)
        {
            if (parentNode.Rule == _rule)
                VisitMatch(parentNode);
            base.VisitAstNode(parentNode);
        }
    }
}