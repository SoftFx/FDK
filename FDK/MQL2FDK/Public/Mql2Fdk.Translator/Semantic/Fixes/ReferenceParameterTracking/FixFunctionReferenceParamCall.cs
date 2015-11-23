using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.ReferenceParameterTracking
{
    class FixFunctionReferenceParamCall : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.ExtractFunctionNodes();

            ResolveRefConflicts(functionNodes);
        }


        void ResolveRefConflicts(ParseNode[] functionNodes)
        {
            foreach (var functionNode in functionNodes)
            {
                ResolveConflictsInFunction(functionNode);
            }
        }

        static void RenameConflict(ParseNode node)
        {
            var tokenData = node;
            var functionName = tokenData.Content;
            if (!FunctionReferenceData.HasFunction(functionName))
                return;
            if (!FunctionReferenceData.FunctionHasRefArguments(functionName))
                return;
            var parentNodeStates = node.Parent.States;
            var indexInParent = parentNodeStates.MappedNodes.IndexOf(node);
            var nextTokenKind = parentNodeStates.MappedNodes[indexInParent + 1].GetTokenKind();
            if (nextTokenKind != TokenKind.OpenParen) return;
            var closeParen = parentNodeStates.GetNextMachingTokenKind(
                TokenKind.CloseParen, TokenKind.OpenParen, indexInParent + 1);
            var referenceData = FunctionReferenceData.GetReferenceData(functionName);
            var positionsStart = new List<int> { indexInParent + 2 };
            for (var i = indexInParent + 2; i < closeParen; i++)
            {
                var nodeTokenKind = parentNodeStates[i].GetTokenKind() == TokenKind.Comma;
                if (nodeTokenKind)
                {
                    positionsStart.Add(i + 1);
                }
            }
            //parsing of params seems wrong, better to skip it
            if (positionsStart.Count != referenceData.RefBools.Length)
                return;
            positionsStart.ReverseEachWithIndex((posStart, index) =>
                {
                    if (referenceData[index] == ParameterKind.None) return;
                    parentNodeStates.Insert(posStart, TokenKind.Space.BuildTokenFromId(" "));
                    parentNodeStates.Insert(posStart, TokenKind.Ref.BuildTokenFromId());
                });
            parentNodeStates.Remap();
        }

        static void ResolveConflictsInFunction(ParseNode functionNode)
        {
            var functionBodyPosition = functionNode.Children.GetNextOfRule(RuleKind.BlockCode);
            var functionBody = functionNode.Children[functionBodyPosition];
            var astVisitor = new AstTokenVisitor(functionBody, TokenKind.Identifier)
            {
                CallOnMatch = RenameConflict
            };
            astVisitor.Visit();
        }
    }
}