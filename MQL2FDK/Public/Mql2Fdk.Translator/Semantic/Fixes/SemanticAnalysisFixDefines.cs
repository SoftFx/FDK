using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class SemanticAnalysisFixDefines : SemanticFixForToken
    {
        public SemanticAnalysisFixDefines()
            : base(TokenKind.SharpDefine)
        {
        }

        protected override void FixLogic(ParseNode node)
        {
            var advancePosition = node.PositionInParent();
            var cleanStates = node.Parent.Children.GetCleanStates();
            cleanStates.ShiftSharpDefineDeclaration(advancePosition);
        }
    }
}