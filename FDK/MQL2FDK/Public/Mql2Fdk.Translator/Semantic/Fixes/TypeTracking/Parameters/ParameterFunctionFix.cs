using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking.Parameters
{
    class ParameterFunctionFix
    {
        readonly ParseNode _functionBody;
        Dictionary<string, TypeData> _declarations;

        public ParameterFunctionFix(ParseNode functionBody)
        {
            _functionBody = functionBody;
        }

        public void Perform()
        {
            _declarations = _functionBody.GetDeclarations();
            var visitor = new AstTokenVisitor(_functionBody, TokenKind.OpenParen)
                {
                    CallOnMatch = FixLogic
                };
            visitor.Visit();
        }

        void FixLogic(ParseNode node)
        {
            var states = node.Parent.States;
            var advance = states.IndexOf(node);
            if (advance == 0)
                return;
            if (states[advance + 1].GetTokenKind() == TokenKind.CloseParen)
                return;
            var closeParenPos = states.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen, advance);
            var parentFunctionName = states[advance - 1].GetTokenContent();
            if (!FunctionTypeData.HasFunction(parentFunctionName))
                return;
            var functionData = FunctionTypeData.GetFunctionData(parentFunctionName);
            var startVars = GetParameterStartPositions(advance, closeParenPos, states);
            if (startVars.Count != functionData.ParamTypes.Length)
                return;
            var identifiers = startVars.Select(id => states[id]).ToArray();
            identifiers.EachWithIndex(
                (identifier, index) => HandleCastOnParameters(identifier, functionData, index, states));
        }

        void HandleCastOnParameters(ParseNode identifier, TypeParameterTable functionData, int index,
                                            CleanupAstNodeStates states)
        {
            var rightType = _declarations.GetTypeOfName(identifier.GetTokenContent());
            var leftTypeName = functionData[index].TokenList[0].Content;
            if (rightType == null || rightType.TokenList.Count == 0)
                return;
            var rightTypeName = rightType[0].Content;
            if (rightTypeName == leftTypeName)
                return;
            if (rightTypeName == TypeNames.Unknown)
                return;
            if (leftTypeName == TypeNames.Unknown)
                return;
            var conversionTypes = string.Format("{0}={1}", leftTypeName, rightTypeName);
            switch (conversionTypes)
            {
                case "int=double":
                    SemanticAnalysisUtils.AddExplicitCastAtPosition(states, leftTypeName, states.IndexOf(identifier));
                    break;
                //nothing to do
                case "color=int":
                case "datetime=int":
                case "double=int":
                case "int=datetime":
                case "double=color":
                case "int=color":
                case "color=double":
                case "double=datetime":
                    return;
                case "int=bool":
                    FixAssignmentInFunction.AddCastCall(states, TypeNames.ToInt, states.IndexOf(identifier) + 1);
                    return;
                case "double=bool":
                    FixAssignmentInFunction.AddCastCall(states, TypeNames.ToDouble, states.IndexOf(identifier) + 1);
                    return;
                case "bool=int":
                case "bool=double":
                    FixAssignmentInFunction.AddCastCall(states, TypeNames.ToBool, states.IndexOf(identifier) + 1);
                    return;
                case "string=bool":
                case "string=int":
                case "string=double":
                    FixAssignmentInFunction.AddCastCall(states, TypeNames.ConvStr, states.IndexOf(identifier) + 1);
                    return;
                default:
                    return;
            }
        }

        public static List<int> GetParameterStartPositions(int advance, int closeParenPos, CleanupAstNodeStates states)
        {
            var startVars = new List<int> {advance + 1};

            var skipInnerCall = 0;
            Enumerable.Range(advance + 1, closeParenPos - advance).Each(
                pos =>
                    {
                        var tokenKind = states[pos].GetTokenKind();
                        switch (tokenKind)
                        {
                            case TokenKind.Comma:
                                if (skipInnerCall == 0)
                                    startVars.Add(pos + 1);
                                break;
                            case TokenKind.OpenParen:
                                skipInnerCall++;
                                break;
                            case TokenKind.CloseParen:
                                skipInnerCall--;
                                break;
                        }
                    }
                );
            return startVars;
        }
    }
}