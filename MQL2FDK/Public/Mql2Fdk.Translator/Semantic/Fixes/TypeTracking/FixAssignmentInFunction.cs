using System;
using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixAssignmentInFunction
    {
        readonly ParseNode _functionBody;

        Dictionary<string, TypeData> _variableTypes;

        public FixAssignmentInFunction(ParseNode functionBody)
        {
            _functionBody = functionBody;
        }

        public void Perform()
        {
            _variableTypes = _functionBody.GetDeclarations();

            var visitorAssigns = new AstTokenVisitor(_functionBody, TokenKind.Assign)
                {
                    CallOnMatch = node => EvaluateAssign(node, _variableTypes)
                };
            visitorAssigns.Visit();
        }

        static void EvaluateAssign(ParseNode node, Dictionary<string, TypeData> variableTypes)
        {
            var states = node.Parent.States;

            var assign = states.MappedNodes.IndexOf(node);
            var leftToken = states[assign - 1].GetTokenData();
            var rightToken = states[assign + 1].GetTokenData();
            if (leftToken.Token != TokenKind.Identifier)
                return;
            if (rightToken.Token != TokenKind.Identifier)
                return;
            var leftVariableName = leftToken.Content;
            var leftExpression = variableTypes.GetTypeOfName(leftVariableName);
            var rightVariableName = rightToken.Content;
            var rightExpression = variableTypes.GetTypeOfName(rightVariableName);
            if (leftExpression.Count == 0 || rightExpression.Count == 0)
                return;
            if (leftExpression.Equals(rightExpression))
                return;
            var leftTypeName = leftExpression[0].Content;
            var rightTypeName = rightExpression[0].Content;

            //we don't handle arrays or other complex types
            if (leftExpression.TokenList.Count != 1 || rightExpression.TokenList.Count != 1)
                return;
            var conversionTypes = string.Format("{0}={1}", leftTypeName, rightTypeName);
            HandleConversions(conversionTypes, states, leftTypeName, assign);
        }

        static void HandleConversions(string conversionTypes, CleanupAstNodeStates states, string leftTypeName,
                                              int assign)
        {
            int tokenToInsert;
            if (ComputeInsertCastPosition(states, assign, out tokenToInsert)) return;
            switch (conversionTypes)
            {
                case "int=double":
                    SemanticAnalysisUtils.AddExplicitCastAtPosition(states, leftTypeName, assign + 1);
                    break;
                //nothing to do
                case "color=int":
                case "int=color":
                case "datetime=int":
                case "double=int":
                case "int=datetime":
                case "color=double":
                    return;

                case "double=datetime":
                case "int=string":
                    //hard to handle
                    break;

                case "int=bool":
                    AddCastCall(states, TypeNames.ToInt, tokenToInsert);
                    break;

                case "bool=double":
                case "bool=int":
                    AddCastCall(states, TypeNames.ToBool, tokenToInsert);
                    break;

                case "string=int":
                case "string=double":
                case "string=bool":
                    AddCastCall(states, TypeNames.ConvStr, tokenToInsert);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public static void AddCastCall(CleanupAstNodeStates states, string convFunction, int tokenToInsert)
        {
            var toAdd = new List<ParseNode>
                {
                    TokenKind.Dot.BuildTokenFromId(),
                    TokenKind.Identifier.BuildTokenFromId(convFunction),
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.CloseParen.BuildTokenFromId(),
                };
            states.InsertRange(tokenToInsert, toAdd);
        }

        static bool ComputeInsertCastPosition(CleanupAstNodeStates states, int assign, out int tokenToInsert)
        {
            tokenToInsert = assign + 2;
            if (states[tokenToInsert].Token == TokenKind.OpenParen)
            {
                var matchingCloseParen = states.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen,
                                                                        tokenToInsert);
                tokenToInsert = matchingCloseParen + 1;
            }
            if (states[tokenToInsert].Token != TokenKind.SemiColon)
                return true;
            return false;
        }
    }
}