using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Parser.Comon
{
    class AstTokenVisitor : AstTreeVisitor
    {
        readonly TokenKind _tokenKind;

        public AstTokenVisitor(ParseNode node, TokenKind tokenKind) : base(node)
        {
            _tokenKind = tokenKind;
        }

        protected override void VisitAstNode(ParseNode parentNode)
        {
            if (parentNode.Token == _tokenKind)
                VisitMatch(parentNode);
            base.VisitAstNode(parentNode);
        }
    }
}