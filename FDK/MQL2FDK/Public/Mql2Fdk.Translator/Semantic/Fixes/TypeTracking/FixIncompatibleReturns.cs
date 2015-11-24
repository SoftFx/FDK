using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixIncompatibleReturns : SemanticFixForToken
    {
        public FixIncompatibleReturns() : base(TokenKind.Return)
        {
        }

        protected override void FixLogic(ParseNode node)
        {
            var returnStates = node.Parent.States;
            if (returnStates.Count == 2)
                return;
            var advance = returnStates.IndexOf(node);
            var identifier = returnStates[advance + 2];
            if (identifier.Token != TokenKind.Identifier)
                return;
            var functionBlockNode = node.GetParentFunctionBlockNode();
            var declarations = functionBlockNode.GetDeclarations();
            var functionStates = functionBlockNode.Parent.States;
            var functionType = declarations.GetTypeOfName(functionStates[1].GetTokenContent());
            var returnVarType = declarations.GetTypeOfName(identifier.GetTokenContent());
            if (functionType.Equals(returnVarType))
                return;
            if (functionType.Count != 1 || returnVarType.Count != 1)
                return;

            var leftTypeName = functionType[0].Content;
            var rightTypeName = returnVarType[0].Content;
            var conversionTypes = string.Format("{0}={1}", leftTypeName, rightTypeName);
            switch (conversionTypes)
            {
                case "int=double":
                    SemanticAnalysisUtils.AddExplicitCastAtPosition(returnStates, leftTypeName,
                                                                    returnStates.IndexOf(identifier));
                    break;
                    //nothing to do
                case "color=int":
                case "datetime=int":
                case "double=int":
                case "int=datetime":
                    return;
                case "int=bool":
                    var insertTokens = new[]
                        {
                            TokenKind.Dot.BuildTokenFromId(),
                            TokenKind.Identifier.BuildTokenFromId("ToInt"),
                            TokenKind.OpenParen.BuildTokenFromId(),
                            TokenKind.CloseParen.BuildTokenFromId()
                        };
                    returnStates.InsertRange(returnStates.IndexOf(identifier) + 1,
                                             insertTokens
                        );
                    returnStates.Remap();
                    return;
                default:
                    return;
            }
        }
    }
}