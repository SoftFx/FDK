using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class ExtractTypeDataInGlobalVariables : SemanticFixForRule
    {
        public ExtractTypeDataInGlobalVariables()
            : base(RuleKind.DeclareVariable)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            if (node.Parent.Rule != RuleKind.Root &&
                node.Parent.Rule != RuleKind.Extern)
                return;
            var cleanStates = node.States;
            var firstIdentifier = cleanStates.GeNextTokenKind(TokenKind.Identifier);
            var typeNodes = Enumerable.Range(0, firstIdentifier)
                .Select(it => cleanStates[it]).ToArray();
            ExtractLogicCommon.ExtractGlobalVariables(node, typeNodes);
        }
    }
}