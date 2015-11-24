using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class SemanticAnalysisFixConflictFunctionVariableNames : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionsWithConflicts = new Dictionary<string, bool>();
            var functionNodes = ExtractFunctionNames(node, functionsWithConflicts);

            EvaluateConflictsBetweenFunctionsAndVariables(functionNodes, functionsWithConflicts);

            var renameTable = ComputeRenameTable(functionsWithConflicts);
            RenameConflicts(functionNodes, renameTable);
        }

        static Dictionary<string, string> ComputeRenameTable(Dictionary<string, bool> functionsWithConflicts)
        {
            var functionsToRename = functionsWithConflicts.Where(it => it.Value).Select(it => it.Key).ToArray();
            var renameTable = new Dictionary<string, string>();
            foreach (var functionName in functionsToRename)
            {
                renameTable[functionName] = functionName + "_fn";
            }
            return renameTable;
        }

        static void RenameConflicts(ParseNode[] functionNodes, Dictionary<string, string> renameTable)
        {
            foreach (var functionNode in functionNodes)
            {
                RenameConflictsInFunction(functionNode, renameTable);
            }
        }

        static void RenameConflictsInFunction(ParseNode functionNode, Dictionary<string, string> renameTable)
        {
            var functionBodyPosition = functionNode.Children.GetNextOfRule(RuleKind.BlockCode);
            var functionBody = functionNode.Children[functionBodyPosition];
            var astVisitor = new AstTokenVisitor(functionBody, TokenKind.Identifier)
            {
                CallOnMatch = node => RenameConflict(node, renameTable)
            };
            astVisitor.Visit();

            var functionName = GetFunctionName(functionNode);
            if (!renameTable.ContainsKey(functionName))
                return;
            var functionNameNode = functionNode.Children.First(it => it.GetTokenKind() == TokenKind.Identifier);
            var functionNameTokenData = functionNameNode.GetTokenData();
            functionNameTokenData.Content = renameTable[functionName];
        }

        static void RenameConflict(ParseNode node, Dictionary<string, string> renameTable)
        {
            var tokenData = node.GetTokenData();
            var tokenContent = tokenData.Content;
            if (!renameTable.ContainsKey(tokenContent))
                return;
            var parentNodeStates = node.Parent.States;
            var indexInParent = parentNodeStates.MappedNodes.IndexOf(node);
            var nextTokenKind = parentNodeStates.MappedNodes[indexInParent + 1].GetTokenKind();
            if (nextTokenKind == TokenKind.OpenParen)
            {
                node.GetTokenData().Content = renameTable[tokenContent];
            }
        }

        void EvaluateConflictsBetweenFunctionsAndVariables(ParseNode[] functionNodes,
                                                                   Dictionary<string, bool> functionsWithConflicts)
        {
            foreach (var functionNode in functionNodes)
            {
                EvaluateConflictsInFunction(functionNode, functionsWithConflicts);
            }
        }

        static ParseNode[] ExtractFunctionNames(ParseNode node, Dictionary<string, bool> functionsWithConflicts)
        {
            var functionNodes = node.ExtractFunctionNodes();
            foreach (var functionNode in functionNodes)
            {
                ExtractFunctionName(functionNode, functionsWithConflicts);
            }
            return functionNodes;
        }

        static void EvaluateConflictsInFunction(ParseNode functionNode, Dictionary<string, bool> functionsWithConflicts)
        {
            var functionBodyPosition = functionNode.Children.GetNextOfRule(RuleKind.BlockCode);
            var functionBody = functionNode.Children[functionBodyPosition];
            var astVisitor = new AstTokenVisitor(functionBody, TokenKind.Identifier)
            {
                CallOnMatch = node => EvaluateConflict(node, functionsWithConflicts)
            };
            astVisitor.Visit();
        }

        static void EvaluateConflict(ParseNode node, Dictionary<string, bool> functionsWithConflicts)
        {
            var tokenData = node.GetTokenData();
            var tokenContent = tokenData.Content;
            if (!functionsWithConflicts.ContainsKey(tokenContent))
                return;
            var parentNodeStates = node.Parent.States;
            var indexInParent = parentNodeStates.MappedNodes.IndexOf(node);
            var tokenKind = parentNodeStates.MappedNodes[indexInParent + 1].GetTokenKind();
            if (tokenKind == TokenKind.OpenParen)
                return;
            functionsWithConflicts[tokenContent] = true;
        }

        static void ExtractFunctionName(ParseNode functionNode, Dictionary<string, bool> functionsWithConflicts)
        {
            var functionName = GetFunctionName(functionNode);
            functionsWithConflicts[functionName] = false;
        }

        static string GetFunctionName(ParseNode functionNode)
        {
            var states = functionNode.Children.GetCleanStates();
            var functionNameNode = states.MappedNodes.First(fnNode => fnNode.GetTokenKind() == TokenKind.Identifier);
            var functionName = functionNameNode.GetTokenData().Content;
            return functionName;
        }
    }
}