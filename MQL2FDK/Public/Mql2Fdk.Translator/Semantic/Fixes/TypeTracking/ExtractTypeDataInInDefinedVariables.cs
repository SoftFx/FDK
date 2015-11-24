using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class ExtractTypeDataInInDefinedVariables : SemanticFixForRule
    {
        public ExtractTypeDataInInDefinedVariables() : base(RuleKind.DefineDeclaration)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var cleanStates = node.States;
            var firstIdentifier = cleanStates.GeNextTokenKind(TokenKind.Identifier);
            var variableNode = cleanStates[firstIdentifier].GetTokenData().Content;
            var globalVariable = FunctionTypeData.DefineGlobalVariable(variableNode);
            Enumerable.Range(0, firstIdentifier).Each(
                i => globalVariable.AddTypeNode(cleanStates[i]));
        }
    }
}