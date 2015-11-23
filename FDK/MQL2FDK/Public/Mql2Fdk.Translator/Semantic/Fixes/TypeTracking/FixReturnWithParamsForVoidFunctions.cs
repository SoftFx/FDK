using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixReturnWithParamsForVoidFunctions : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.GetFunctionBodies();
            foreach (var functionNode in functionNodes)
            {
                EvaluateFunctionReturn(functionNode);
            }
        }

        void EvaluateFunctionReturn(ParseNode functionNode)
        {
            var parentFunctionDeclaration = functionNode.Parent.States;
            if (parentFunctionDeclaration[0].Token != TokenKind.TypeName)
                return;
            var functionTypeName = parentFunctionDeclaration[0].Content;
            if (functionTypeName != "void")
                return;
            var astVisitor = new AstTokenVisitor(functionNode, TokenKind.Return)
            {
                CallOnMatch = HandleOnReturn
            };
            astVisitor.Visit();
        }

        static void HandleOnReturn(ParseNode obj)
        {
            var blockStates = obj.Parent.States;
            var retPosition = blockStates.GeNextTokenKind(TokenKind.Return);
            var semiColonPos = blockStates.GeNextTokenKind(TokenKind.SemiColon);
            if (blockStates[retPosition + 1].Token == TokenKind.SemiColon)
                return;
            blockStates.RemoveRange(retPosition + 1, semiColonPos - 1);
        }
    }
}