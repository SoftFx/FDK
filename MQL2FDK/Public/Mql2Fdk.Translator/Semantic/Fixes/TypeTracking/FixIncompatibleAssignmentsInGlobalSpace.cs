using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixIncompatibleAssignmentsInGlobalSpace : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.States;
            foreach (var functionNode in functionNodes)
            {
                FixDeclarationsNotInFunctionBody(functionNode);
            }
        }

        void FixDeclarationsNotInFunctionBody(ParseNode functionBody)
        {
            var states = functionBody.States;
            if (states.Count < 5)
                return;
            if (states[0].Token != TokenKind.TypeName)
                return;
            if (states[1].Token != TokenKind.Identifier)
                return;
            if (states[2].Token != TokenKind.Assign)
                return;
            if (states[3].Token != TokenKind.Identifier)
                return;
            var varType = states[0].Content;
            var rightId = states[3].Content;
            string rightTypeName;
            if (states[4].Token == TokenKind.OpenParen)
            {
                if (!FunctionTypeData.HasFunction(rightId))
                    return;
                var returnType = FunctionTypeData.GetFunctionData(rightId).ReturnType;
                if (returnType.Count != 1)
                    return;
                rightTypeName = returnType[0].Content;
            }
            else
            {
                if (!FunctionTypeData.HasGlobalVariable(rightId))
                    return;

                var rightVariableType = FunctionTypeData.GetGlobalVariable(rightId);
                if (rightVariableType.Count != 1)
                    return;
                rightTypeName = rightVariableType[0].Content;
            }
            if (varType == rightTypeName)
                return;
            var typeFormat = string.Format("{0}={1}", varType, rightTypeName);
            switch (typeFormat)
            {
                case "int=double":

                    SemanticAnalysisUtils.AddExplicitCastAtPosition(states, varType, 3);
                    break;
            }
        }
    }
}