using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations
{
    class FixCloseOpenSquareAsComma : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var sharpDefineVisitor = new AstTreeVisitorAllNodes(node)
                {
                    CallOnMatch = FixLogic
                };
            sharpDefineVisitor.Visit();
        }

        static void FixLogic(ParseNode node)
        {
            while (CanFixOpenCloseSquare(node, 0))
            {
                //do nothing
            }
        }

        static bool CanFixOpenCloseSquare(ParseNode node, int advance)
        {
            if (node.Count < 4)
                return false;
            var states = node.States;
            var closeSquarePos = states.GeNextTokenKind(TokenKind.CloseSquared, advance);
            if (closeSquarePos == 0)
                return false;
            if (closeSquarePos >= states.Count - 2)
                return false;
            if (states[closeSquarePos + 1].Token != TokenKind.OpenSquared)
                return CanFixOpenCloseSquare(node, closeSquarePos);
            states[closeSquarePos].Token = TokenKind.Comma;
            states[closeSquarePos].Content = ",";
            states.RemoveAt(closeSquarePos + 1);
            return true;
        }
    }
}