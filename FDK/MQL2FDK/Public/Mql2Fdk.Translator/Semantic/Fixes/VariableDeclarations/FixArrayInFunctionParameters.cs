using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations
{
    class FixArrayInFunctionParameters : SemanticFixForRule
    {
        public FixArrayInFunctionParameters()
            : base(RuleKind.FunctionSignature)
        {
        }

        protected override void FixRuleProblem(ParseNode astNode)
        {
            var states = astNode.States;
            var isArrayDefined = states.Any(node => node.GetTokenKind() == TokenKind.OpenSquared);
            if (!isArrayDefined)
                return;
            SetRequiredParen(states);
        }


        public static void SetRequiredParen(CleanupAstNodeStates states)
        {
            for (var i = 2; i < states.Count; i++)
            {
                var closeToken = states[i];
                if (closeToken.Token != TokenKind.CloseSquared)
                    continue;
                var openToken = states[i];
                if (openToken.Token != TokenKind.OpenSquared)
                    continue;
                closeToken.Token = TokenKind.Comma;
                closeToken.Content = ",";
            }
            for (var i = states.Count - 1; i > 0; i--)
            {
                var openToken = states[i];
                if (openToken.Token != TokenKind.OpenSquared)
                    continue;

                var idToken = states[i - 1];
                if (idToken.Token != TokenKind.Identifier)
                    continue;
                var closeTokenId = states.GetNextMachingTokenKind(TokenKind.CloseSquared, TokenKind.OpenSquared, i);

                var toAdd = new List<ParseNode>();
                for (var addRange = i; addRange <= closeTokenId; addRange++)
                {
                    toAdd.Add(states[addRange].Clone());
                }
                states.InsertRange(i - 1, toAdd);
                i += toAdd.Count;

                closeTokenId = states.GetNextMachingTokenKind(TokenKind.CloseSquared, TokenKind.OpenSquared, i);
                states.RemoveRange(i, closeTokenId);
            }
        }
    }
}