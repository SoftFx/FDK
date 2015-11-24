using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class SemanticStaticVariableDeclaration : SemanticFixForToken
    {
        public SemanticStaticVariableDeclaration()
            : base(TokenKind.Static)
        {
        }


        protected override void FixLogic(ParseNode node)
        {
            var parentStaticDeclaration = node.Parent;

            var parentRule = parentStaticDeclaration.Rule;
            if (parentRule != RuleKind.Static)
                return;

            var getFunctionNode = node.GetParentFunctionDeclaration();
            if(getFunctionNode==null)
                return;
            var siblingInstructions = parentStaticDeclaration.Parent.Children;
            siblingInstructions.RemoveAt(siblingInstructions.IndexOf(parentStaticDeclaration));

            var functionName = GetFunctionName(getFunctionNode);
            var renameTable = new RenameTable(functionName);
            renameTable.ScanNames(parentStaticDeclaration);

            renameTable.RenameInTree(parentStaticDeclaration);
            renameTable.RenameInTree(getFunctionNode);
            var functionChildren = getFunctionNode.Parent.Children;
            var functionPosInProgram = functionChildren.IndexOf(getFunctionNode);

            var staticStates = parentStaticDeclaration.States.MappedNodes;
            functionChildren.Insert(functionPosInProgram, staticStates[1]);
        }

        static string GetFunctionName(ParseNode getFunctionNode)
        {
            var states = getFunctionNode.States;
            var functionName = states.MappedNodes[1].GetTokenData().Content;
            return functionName;
        }
    }
}