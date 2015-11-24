using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;
using Mql2Fdk.Translator.Semantic.Fixes.TypeTracking;

namespace Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations
{
    class FixMultipleVarsWithArray : SemanticAnalysisFixBase
    {
        bool _found = true;

        public override void Perform(ParseNode node)
        {
            var astVisitor = new AstTreeVisitorAllNodes(node)
                {
                    CallOnMatch = TryFixRuleProblem
                };
            while (_found)
            {
                _found = false;
                astVisitor.Visit();
            }
        }

        void TryFixRuleProblem(ParseNode obj)
        {
            if (obj.Rule != RuleKind.DeclareVariable)
                return;
            FixRuleProblem(obj);
        }

        void FixRuleProblem(ParseNode astNode)
        {
            var states = astNode.States;
            if (states[0].Token != TokenKind.TypeName)
                return;
            var isArrayDefined = states.Any(node => node.GetTokenKind() == TokenKind.OpenSquared);
            if (!isArrayDefined)
                return;
            if (astNode.Parent.Rule == RuleKind.Static)
                return;
            var isMultiParameter = IsMultiParameter(states);
            if (!isMultiParameter) return;
            var commaPos = ComputeCommaPositions(states);
            if (commaPos.Count == 1)
                return;
            HandleMultipleParameters(astNode, states);
        }

        static bool IsMultiParameter(CleanupAstNodeStates states)
        {
            var isInParen = false;
            for (var index = 1; index < states.Count - 1; index++)
            {
                var node = states[index];
                if (node.Token == TokenKind.OpenSquared)
                    isInParen = true;
                if (node.Token == TokenKind.CloseSquared)
                    isInParen = false;
                if (node.Token == TokenKind.Comma)
                {
                    if (isInParen)
                        continue;
                    return true;
                }
            }
            return false;
        }

        void HandleMultipleParameters(ParseNode astNode, CleanupAstNodeStates states)
        {
            var typeNode = states[0];
            var commaPositions = ComputeCommaPositions(states);
            var functionBlockNode = typeNode.GetParentFunctionBlockNode() ?? typeNode.GetRootNode();
            ExtractArrayParameters(states, commaPositions, typeNode, functionBlockNode);
            astNode.RemoveFromParent();
            _found = true;
            states.Remap();
        }

        void ExtractArrayParameters(CleanupAstNodeStates states, List<int> commaPositions, ParseNode typeNode,
                                            ParseNode functionBlockNode)
        {
            var startPos = 1;
            foreach (var commaPosition in commaPositions)
            {
                ExtractArrayParameter(typeNode, states, startPos, commaPosition - 1, functionBlockNode);
                startPos = commaPosition + 1;
            }
        }

        public static List<int> ComputeCommaPositions(CleanupAstNodeStates states)
        {
            var commaPositions = new List<int>();

            var isInSquares = false;
            var isRightHand = false;
            var id = 0;
            foreach (var node in states)
            {
                var isComma = node.Token == TokenKind.Comma;
                var isOpen = node.Token == TokenKind.OpenSquared;
                var isClosed = node.Token == TokenKind.CloseSquared;
                var isAssign = node.Token == TokenKind.Assign;
                if (isOpen)
                    isInSquares = true;
                if (isAssign)
                    isRightHand = true;
                if (isClosed)
                    isInSquares = false;
                if (isComma && !isInSquares && !isRightHand)
                    commaPositions.Add(id);
                id++;
            }
            commaPositions.Add(states.Count - 1);
            return commaPositions;
        }

        void ExtractArrayParameter(ParseNode typeNode, CleanupAstNodeStates states, int startPos,
                                           int lastPosition, ParseNode functionBlockNode)
        {
            var declarationNode = RuleKind.DeclareVariable.NewNodeOfRule();
            declarationNode.AddRange(new[]
                {
                    typeNode.Clone(),
                    TokenKind.Space.BuildTokenFromId(" "),
                });
            Enumerable.Range(startPos, lastPosition - startPos + 1).Each(
                i => declarationNode.Add(states[i].Clone()));

            for (var i = startPos; i < lastPosition; i++)
            {
                var mappedNode = states[i];
                if (mappedNode.Token == TokenKind.Identifier)
                    break;
                TypeData.ValidateToken(mappedNode);
            }

            declarationNode.Add(TokenKind.SemiColon.BuildTokenFromId());
            functionBlockNode.Insert(1, declarationNode);
            FixRuleProblem(declarationNode);
        }
    }
}