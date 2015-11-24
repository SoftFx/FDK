using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Semantic.Common
{
    abstract class SemanticFixForToken : SemanticAnalysisFixBase
    {
        readonly TokenKind _tokenKind;

        protected SemanticFixForToken(TokenKind tokenKind)
        {
            _tokenKind = tokenKind;
        }

        public override void Perform(ParseNode node)
        {
            var sharpDefineVisitor = new AstTokenVisitor(node, _tokenKind)
                {
                    CallOnMatch = FixLogic
                };
            sharpDefineVisitor.Visit();
        }

        protected abstract void FixLogic(ParseNode node);
    }
}