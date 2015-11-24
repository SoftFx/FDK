using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixParametersByCasting : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.GetFunctionBodies();
            foreach (var functionNode in functionNodes)
            {
                EvaluateFunctionReturn(functionNode);
            }
        }

        static void EvaluateFunctionReturn(ParseNode functionNode)
        {
            var declarations = functionNode.GetDeclarations();
            var astVisitor = new AstTokenVisitor(functionNode, TokenKind.Identifier)
            {
                CallOnMatch = node => SearchFunctionCall(node, declarations)
            };
            astVisitor.Visit();
        }

        static void SearchFunctionCall(ParseNode obj, Dictionary<string, TypeData> declarations)
        {
            var indexInParent = obj.PositionInParent();
            var blockStates = obj.Parent.States;
            if (blockStates[indexInParent + 1].Token != TokenKind.OpenParen)
                return;
            var functionData = FunctionTypeData.GetFunctionData(blockStates[indexInParent].Content);
            if (functionData == null)
                return;
            if (functionData.ParamTypes.Length == 0)
                return;
            var closeParen = blockStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen, indexInParent + 1);
            var commas = blockStates.GetAllOfToken(TokenKind.Comma, indexInParent + 1, closeParen);
            if (commas.Count != functionData.ParamTypes.Length - 1)
                return;
            commas.Add(closeParen);
            var startRange = indexInParent + 2;
            for (var i = 0; i < commas.Count; i++)
            {
                var commaPos = commas[i];
                var endRange = commaPos - 1;
                if (blockStates[startRange].Token == TokenKind.Identifier)
                {
                    TryHandleParameter(functionData.ParamTypes, i, blockStates, startRange, endRange, declarations);
                }
                startRange = endRange + 2;
            }
        }

        static void TryHandleParameter(TypeData[] fnParams, int i,
            CleanupAstNodeStates blockStates,
            int startRange, int endRange,
            Dictionary<string, TypeData> declarations)
        {
            var isFunction = blockStates[startRange + 1].Token == TokenKind.OpenParen;
            var typeArgument = fnParams[i];
            var tmp = typeArgument.Count != 1;
            if (tmp)
                return;
            var argumentType = typeArgument[0].Content;
            if (argumentType == TypeNames.Unknown)
                return;
            var token = blockStates[startRange];
            var typeOfParam = declarations.GetTypeOfName(token.Content);
            if (typeOfParam == null || typeOfParam.Count != 1)
                return;
            var paramType = typeOfParam[0].Content;
            if (paramType == argumentType)
                return;
            if (!isFunction && endRange != startRange)
                return;
            var formatTypes = string.Format("{0}={1}", paramType, argumentType);
            FixArgumentType(blockStates, startRange, isFunction, formatTypes);
        }

        static void FixArgumentType(CleanupAstNodeStates blockStates, int startRange, bool isFunction, string formatTypes)
        {
            switch (formatTypes)
            {
                case "int=double":
                    AddExplicitCastToDouble(blockStates, startRange);
                    break;
                case "double=string":
                case "int=string":
                    AddExplicitCastIntToString(blockStates, startRange, isFunction);
                    break;
                case "bool=double":
                    AddExplicitCastBoolToInt(blockStates, startRange, isFunction, "ToDouble");
                    break;
                case "bool=int":
                    AddExplicitCastBoolToInt(blockStates, startRange, isFunction, "ToInt");
                    break;
            }
        }

        static void AddExplicitCastBoolToInt(CleanupAstNodeStates blockStates, int startRange, bool isFunction, string castFunction)
        {
            int insertPos;
            if (!isFunction)
                insertPos = startRange + 1;
            else
                insertPos = blockStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen, startRange) + 1;
            var addTokens = new List<ParseNode>
                {
                    TokenKind.Dot.BuildTokenFromId(),
                    TokenKind.Identifier.BuildTokenFromId(castFunction),
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.CloseParen.BuildTokenFromId(),
                };

            blockStates.InsertRange(insertPos, addTokens);
        }

        static void AddExplicitCastIntToString(CleanupAstNodeStates blockStates, int startRange, bool isFunction)
        {
            int insertPos;
            if (!isFunction)
                insertPos = startRange + 1;
            else
                insertPos = blockStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen, startRange) + 1;
            var addTokens = new List<ParseNode>
                {
                    TokenKind.Dot.BuildTokenFromId(),
                    TokenKind.Identifier.BuildTokenFromId("ConvStr"),
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.CloseParen.BuildTokenFromId(),
                };

            blockStates.InsertRange(insertPos, addTokens);
        }

        static void AddExplicitCastToDouble(CleanupAstNodeStates blockStates, int startRange)
        {
            var addTokens = new List<ParseNode>
                {
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.TypeName.BuildTokenFromId("double"),
                    TokenKind.CloseParen.BuildTokenFromId(),
                };
            blockStates.InsertRange(startRange, addTokens);
        }
    }
}