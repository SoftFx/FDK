using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixParametersWithConstants : SemanticAnalysisFixBase
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
            var astVisitor = new AstTokenVisitor(functionNode, TokenKind.Identifier)
            {
                CallOnMatch = SearchFunctionCall
            };
            astVisitor.Visit();
        }

        static void SearchFunctionCall(ParseNode obj)
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
            var closeParen = blockStates.GeNextTokenKind(TokenKind.CloseParen, indexInParent);
            var commas = blockStates.GetAllOfToken(TokenKind.Comma, indexInParent + 1, closeParen);
            if (commas.Count != functionData.ParamTypes.Length - 1)
                return;
            commas.Add(closeParen);
            var startRange = indexInParent + 2;
            for (var i = 0; i < commas.Count; i++)
            {
                var commaPos = commas[i];
                var endRange = commaPos - 1;
                if (startRange == endRange && blockStates[startRange].Token != TokenKind.Identifier)
                {
                    TryHandleParameter(functionData.ParamTypes, i, blockStates, startRange);
                }
                if (startRange == endRange && blockStates[startRange].Content == "null")
                {
                    TryHandleParameterNull(functionData.ParamTypes, i, blockStates, startRange);
                }
                startRange = endRange + 2;
            }
        }

        static void TryHandleParameterNull(TypeData[] fnParams, int i, CleanupAstNodeStates blockStates, int startRange)
        {
            var token = blockStates[startRange];
            var typeParam = fnParams[i];
            var tmp = typeParam.Count != 1;
            if (tmp)
                return;
            var paramType = typeParam[0].Content;
            if (paramType == TypeNames.Unknown)
                return;
            switch (typeParam[0].Content)
            {
                case "double":
                    token.Token = TokenKind.Float;
                    token.Content = string.Format("0.0");
                    break;
                case "int":
                    token.Token = TokenKind.Int;
                    token.Content = string.Format("0");
                    break;
            }
        }

        static void TryHandleParameter(TypeData[] fnParams, int i, CleanupAstNodeStates blockStates, int startRange)
        {
            var token = blockStates[startRange];
            var typeParam = fnParams[i];
            var tmp = typeParam.Count != 1;
            if (tmp)
                return;
            var paramType = typeParam[0].Content;
            if (paramType == TypeNames.Unknown)
                return;
            var formatType = string.Format("{0}={1}", paramType, token.Token);
            switch (formatType)
            {
                case "double=Int":
                    var intValue = int.Parse(token.Content);
                    token.Token = TokenKind.Float;
                    token.Content = string.Format("{0}.0", intValue);

                    break;
                case "int=Float":
                    var floatValue = double.Parse(token.Content);
                    token.Token = TokenKind.Int;
                    token.Content = string.Format("{0}", (int)floatValue);

                    break;

                case "string=Int":
                case "string=Float":
                    var combinedText = string.Format("\"{0}\"", token.Content);

                    token.Token = TokenKind.QuotedString;
                    token.Content = combinedText;

                    break;
            }
        }
    }
}