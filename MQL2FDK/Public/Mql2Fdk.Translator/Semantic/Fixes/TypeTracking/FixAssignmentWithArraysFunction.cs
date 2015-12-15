using System;
using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixAssignmentWithArraysFunction
    {
        readonly ParseNode _functionBody;

        Dictionary<string, TypeData> _variableTypes;

        public FixAssignmentWithArraysFunction(ParseNode functionBody)
        {
            _functionBody = functionBody;
        }

        public void Perform()
        {
            _variableTypes = _functionBody.GetDeclarations();

            var visitorAssigns = new AstTokenVisitor(_functionBody, TokenKind.Assign)
                {
                    CallOnMatch = EvaluateAssign
                };
            visitorAssigns.Visit();
        }

        TypeData GetExpressionOfName(string name)
        {
            TypeData result;
            if (_variableTypes.TryGetValue(name, out result))
                return result;
            if (FunctionTypeData.HasFunction(name))
            {
                var functionData = FunctionTypeData.GetFunctionData(name);
                return functionData.ReturnType;
            }
            if (FunctionTypeData.HasGlobalVariable(name))
            {
                var variableData = FunctionTypeData.GetGlobalVariable(name);
                return variableData;
            }
            return null;
        }

        void EvaluateAssign(ParseNode node)
        {
            var states = node.Parent.States;

            var assign = states.MappedNodes.IndexOf(node);
            var leftToken = states[assign - 1].GetTokenData();
            var rightToken = states[assign + 1].GetTokenData();
            if (leftToken.Token != TokenKind.CloseSquared)
                return;
            if (rightToken.Token != TokenKind.Identifier)
                return;
            var pos = -1;
            for (var i = assign - 2; i >= 0; i--)
            {
                leftToken = states[i].GetTokenData();

                if (leftToken.Token != TokenKind.OpenSquared) continue;
                pos = i - 1;
                break;
            }
            if (pos < 0)
                return;
            leftToken = states[pos].GetTokenData();
            var leftVariableName = leftToken.Content;
            var leftExpression = GetExpressionOfName(leftVariableName);
            var rightVariableName = rightToken.Content;
            var rightExpression = GetExpressionOfName(rightVariableName);
            if (leftExpression == null || rightExpression == null)
                return;
            if (rightExpression.Count < 1)
                return;
            var leftTypeName = leftExpression[0].Content;
            var rightTypeName = rightExpression[0].Content;
            if (leftTypeName == rightTypeName)
                return;
            if (leftTypeName == "Unknown")
                return;
            if (rightTypeName == "Unknown")
                return;

            //we don't handle arrays or other complex types
            if (rightExpression.TokenList.Count != 1)
                return;
            var conversionTypes = string.Format("{0}={1}", leftTypeName, rightTypeName);
            switch (conversionTypes)
            {
                case "int=double":
                    AddExplicitCastAtPosition(states, leftTypeName, assign + 1);
                    break;
                //nothing to do
                case "color=int":
                case "datetime=int":
                case "double=int":
                case "int=datetime":
                case "double=datetime":
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        static void AddExplicitCastAtPosition(CleanupAstNodeStates states, string typeName, int positionCast)
        {
            var explicitCastList = new List<ParseNode>
                {
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.TypeName.BuildTokenFromId(typeName),
                    TokenKind.CloseParen.BuildTokenFromId()
                };

            states.InsertRange(positionCast, explicitCastList);
        }
    }
}