using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;
using Mql2Fdk.Translator.Semantic.Fixes.TypeTracking;

namespace Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations
{
    class FixDeclaredArray : SemanticFixForRule
    {
        public FixDeclaredArray()
            : base(RuleKind.DeclareVariable)
        {
        }

        protected override void FixRuleProblem(ParseNode astNode)
        {
            var states = astNode.States;

            var isArrayDefined = states.Any(node => node.GetTokenKind() == TokenKind.OpenSquared);
            if (!isArrayDefined)
                return;
            var isMultiParameter = IsMultiParameter(states);
            if (isMultiParameter)
            {
                //should be fixed by: FixMultipleVarsWithArray
                return;
            }
            var varNamePos = states.GeNextTokenKind(TokenKind.Identifier);
            if (varNamePos == 0)
                return; 
            var squareAfterVar = states[varNamePos+1];
            if (squareAfterVar.Token != TokenKind.OpenSquared)
                return;
            var checkArrayToken = states[varNamePos - 1].Token;
            if (checkArrayToken == TokenKind.CloseSquared)
                return;
            SetRequiredParen(states);
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

        public static void SetRequiredParen(CleanupAstNodeStates states)
        {
            var openSquarePos = states.MappedNodes.GeNextTokenKind(TokenKind.OpenSquared);
            var closeSquarePos = states.MappedNodes.GeNextTokenKind(TokenKind.CloseSquared);
            var commaPos = states.MappedNodes.GeNextTokenKind(TokenKind.Comma);

            if (openSquarePos + 1 == closeSquarePos || openSquarePos + 1 == commaPos)
            {
                states.Insert(openSquarePos + 1, TokenKind.Int.BuildTokenFromId("0"));
                states.Remap();
            }
            if (commaPos + 1 == closeSquarePos)
            {
                states.Insert(commaPos + 1, TokenKind.Int.BuildTokenFromId("0"));
                states.Remap();
            }

            var tokenData = states[0].GetTokenData();
            var typeToken = tokenData.Clone();


            var assignPos = states.GeNextTokenKind(TokenKind.Assign);
            if (assignPos > 0)
            {
                var openSquaredPos = states.GeNextTokenKind(TokenKind.OpenSquared);
                if (openSquaredPos > assignPos)
                    return;
            }
            var nodesToBeAdded = new List<ParseNode>
                {
                    TokenKind.Assign.BuildTokenFromId(),
                    TokenKind.Space.BuildTokenFromId(" "),
                    TokenKind.New.BuildTokenFromId(),
                    TokenKind.Space.BuildTokenFromId(" "),
                    typeToken.BuildTerminalNode(),
                };
            states.InsertRange(2, nodesToBeAdded);

            var countSquared = states.Count(node => node.GetTokenKind() == TokenKind.Comma) + 1;

            var toAdd = BuildSquaresAndCommas(countSquared);
            states.InsertRange(1, toAdd);
        }

        static List<ParseNode> BuildSquaresAndCommas(int countSquared)
        {
            var toAdd = new List<ParseNode>();
            toAdd.Add(TokenKind.OpenSquared.BuildTokenFromId());
            Enumerable.Range(0, countSquared - 1).Each(i => toAdd.Add(TokenKind.Comma.BuildTokenFromId()));
            toAdd.Add(TokenKind.CloseSquared.BuildTokenFromId());
            return toAdd;
        }

        void HandleMultipleParameters(ParseNode astNode, CleanupAstNodeStates states)
        {
            var typeNode = states[0];
            var commaPositions = new List<int>();

            var isInSquares = false;
            states.EachWithIndex((node, id) =>
                {
                    var isComma = node.Token == TokenKind.Comma;
                    var isOpen = node.Token == TokenKind.OpenSquared;
                    var isClosed = node.Token == TokenKind.CloseSquared;
                    if (isOpen)
                        isInSquares = true;
                    if (isClosed)
                        isInSquares = false;
                    if (isComma && !isInSquares)
                        commaPositions.Add(id);
                });
            commaPositions.Add(states.Count - 1);


            var functionBlockNode = typeNode.GetParentFunctionBlockNode();
            if (functionBlockNode == null)
                functionBlockNode = typeNode.GetRootNode();
            var startPos = 1;
            foreach (var commaPosition in commaPositions)
            {
                ExtractArrayParameter(typeNode, states, startPos, commaPosition - 1, functionBlockNode);
                startPos = commaPosition + 1;
            }
            astNode.RemoveFromParent();
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