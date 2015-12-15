using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixIncompatibleAssignments : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.GetFunctionBodies();
            foreach (var functionNode in functionNodes)
            {
                FixDeclarationsNotInFunctionBody(functionNode);
            }
        }

        static void FixDeclarationsNotInFunctionBody(ParseNode functionBody)
        {
            var fixInFunction = new FixAssignmentInFunction(functionBody);
            fixInFunction.Perform();

            var fixArraysAssigned = new FixAssignmentWithArraysFunction(functionBody);
            fixArraysAssigned.Perform();
        }
    }
}