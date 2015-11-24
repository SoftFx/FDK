using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking.Parameters
{
    class FixIncompatibleParameters : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.GetFunctionBodies();
            foreach (var functionBody in functionNodes)
            {
                var fix = new ParameterFunctionFix(functionBody);
                fix.Perform();
            }
        }
    }
}