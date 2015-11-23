using System.IO;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations
{
    class ValidateDeclarationRules : SemanticFixForRule
    {
        public ValidateDeclarationRules()
            : base(RuleKind.DeclareVariable)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var states = node.States;
            if(states[0].Token != TokenKind.TypeName)
                throw new InvalidDataException("Any declaration should start with a type");
        }
    }
}