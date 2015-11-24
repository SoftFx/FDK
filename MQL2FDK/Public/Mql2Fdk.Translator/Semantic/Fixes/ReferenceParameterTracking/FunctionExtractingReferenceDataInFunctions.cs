using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.ReferenceParameterTracking
{
    class FunctionExtractingReferenceDataInFunctions : SemanticFixForRule
    {
        public FunctionExtractingReferenceDataInFunctions()
            : base(RuleKind.FunctionDeclaration)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var cleanStates = node.States;
            var functionName = cleanStates[1].GetTokenData().Content;
            var paramsStates = cleanStates[2].States;
            if (paramsStates.MappedNodes.Count == 2)
            {
                FunctionReferenceData.DefineFunction(functionName, 0);
                return;
            }

            var countCommas = paramsStates.MappedNodes.Count(paramNode => paramNode.GetTokenKind() == TokenKind.Comma);
            var function = FunctionReferenceData.DefineFunction(functionName, countCommas + 1);
            var indexParam = 0;
            foreach (var mappedNode in paramsStates.MappedNodes)
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