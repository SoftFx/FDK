using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Semantic.Common
{
    abstract class SemanticFixForRule : SemanticAnalysisFixBase
    {
        readonly RuleKind _rule;

        protected SemanticFixForRule(RuleKind rule)
        {
            _rule = rule;
        }

        public override void Perform(ParseNode ruleNode)
        {
            var visitor = new AstTreeVisitorRule(ruleNode, _rule)
                {
                    CallOnMatch = FixRuleProblem
                };
            visitor.Visit();
        }

        protected abstract void FixRuleProblem(ParseNode node);
    }
}