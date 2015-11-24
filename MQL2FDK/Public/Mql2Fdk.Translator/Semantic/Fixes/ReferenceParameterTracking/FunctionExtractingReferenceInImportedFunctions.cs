using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.ReferenceParameterTracking
{
    class FunctionExtractingReferenceInImportedFunctions : SemanticFixForRule
    {
        public FunctionExtractingReferenceInImportedFunctions()
            : base(RuleKind.ImportFunction)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var cleanStates = node.States;
            var functionName = cleanStates[1].GetTokenData().Content;
            var openParen = cleanStates.GeNextTokenKind(TokenKind.OpenParen);
            var closeParen = cleanStates.GeNextTokenKind(TokenKind.CloseParen);
            if (openParen + 1 == closeParen)
            {
                FunctionReferenceData.DefineFunction(functionName, 0);
                return;
            }
            var countCommas = cleanStates.MappedNodes.Count(paramNode => paramNode.GetTokenKind() == TokenKind.Comma);
            var function = FunctionReferenceData.DefineFunction(functionName, countCommas + 1);
            var indexParam = 0;
            foreach (var mappedNode in cleanStates.MappedNodes)
            {
                if (mappedNode.GetTokenKind() == TokenKind.Ref)
                {
                    function[indexParam] = ParameterKind.Ref;
                }
                if (mappedNode.GetTokenKind() == TokenKind.Comma)
                {
                    indexParam++;
                }
            }
        }
    }
}