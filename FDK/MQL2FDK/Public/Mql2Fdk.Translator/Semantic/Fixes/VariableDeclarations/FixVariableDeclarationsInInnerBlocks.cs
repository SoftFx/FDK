using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations
{
    public class FixVariableDeclarationsInInnerBlocks : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.ExtractFunctionNodes();
            foreach (var functionNode in functionNodes)
            {
                FixDeclarationsNotInFunctionBody(functionNode);
            }
        }

        void FixDeclarationsNotInFunctionBody(ParseNode functionNode)
        {
            var functionBodyPosition = functionNode.Children.GetNextOfRule(RuleKind.BlockCode);
            var functionBody = functionNode.Children[functionBodyPosition];
            var astVisitor = new AstTokenVisitor(functionBody, TokenKind.TypeName)
            {
                CallOnMatch = node => EvaluateConflict(node, functionBody)
            };
            astVisitor.Visit();
        }

        void EvaluateConflict(ParseNode astNode, ParseNode functionBody)
        {
            var parentParent = astNode.Parent.Parent;
            if (parentParent == functionBody)
                return;
            var states = astNode.Parent.States;
            var typeIndex = states.MappedNodes.IndexOf(astNode);
            var variableNameNode = states.MappedNodes[typeIndex + 1];
            if (variableNameNode.GetTokenKind() != TokenKind.Identifier)
                return;
            DeclareVariable(functionBody, astNode, variableNameNode);
            states.Remap();
        }

        static void DeclareVariable(ParseNode functionBody, ParseNode typeNameNode, ParseNode variableNameNode)
        {
            var nodesToAdd = new List<ParseNode>
                {
                    typeNameNode.Clone(),
                    TokenKind.Space.BuildTokenFromId(" "),
                    variableNameNode.Clone(),
                    TokenKind.SemiColon.BuildTokenFromId()
                };

            var declarationNode = new ParseNode(RuleKind.DeclareVariable);
            declarationNode.AddRange(nodesToAdd);
            functionBody.Insert(1, declarationNode);
            if (typeNameNode.Parent.Rule == RuleKind.DeclareVariable)
            {
                typeNameNode.Parent.Rule = RuleKind.InstructionCode;
            }
            typeNameNode.RemoveFromParent();
        }
    }
}