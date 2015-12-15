using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Parser.Comon
{
    class AstTreeVisitorAllNodes : AstTreeVisitor
    {
        public AstTreeVisitorAllNodes(ParseNode node) : base(node)
        {
        }

        protected override void VisitAstNode(ParseNode parentNode)
        {
            if (CallOnMatch != null)
                CallOnMatch(parentNode);
            base.VisitAstNode(parentNode);
        }
    }
}