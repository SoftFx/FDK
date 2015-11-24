using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class FixDeclaredRefArgument : SemanticFixForRule
    {
        public FixDeclaredRefArgument() : base(RuleKind.ImportFunction)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var cleanStates = node.States;
            var openParen = cleanStates.MappedNodes.FirstOrDefault(searchNode =>
                                                                   searchNode.GetTokenKind() == TokenKind.Operator
                                                                   && searchNode.GetTokenData().Content == "&");
            if (openParen == null)
                return;

            var starts = new List<int>();
            var ends = new List<int>();

            var firstParamStart = cleanStates.GeNextTokenKind(TokenKind.OpenParen) + 1;
            do
            {
                starts.Add(firstParamStart);
                var nextComma = cleanStates.GeNextTokenKind(TokenKind.Comma, firstParamStart);
                if (nextComma != 0)
                {
                    ends.Add(nextComma - 1);
                }
                else
                {
                    ends.Add(cleanStates.GeNextTokenKind(TokenKind.CloseParen, firstParamStart));
                    break;
                }
                firstParamStart = nextComma + 1;
            } while (true);
            starts.ReverseEachWithIndex((pos, index) => FixArrayInParam(pos, ends[index], cleanStates));
        }

        void FixArrayInParam(int pos, int endRange, CleanupAstNodeStates states)
        {
            var openParen = 0;
            for (var index = pos; index < endRange; index++)
            {
                var searchNode = states[index];
                if (searchNode.GetTokenKind() == TokenKind.Operator
                    && searchNode.GetTokenData().Content == "&")
                    openParen = index;
            }
            if (openParen == 0)
                return;

            states.RemoveAt(openParen);
            states.Insert(pos, TokenKind.Space.BuildTokenFromId(" "));
            states.Insert(pos, TokenKind.Ref.BuildTokenFromId());

            states.Remap();
        }
    }
}