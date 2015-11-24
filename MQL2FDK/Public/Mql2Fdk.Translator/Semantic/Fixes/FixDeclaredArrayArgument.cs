using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class FixDeclaredArrayArgument : SemanticFixForRule
    {
        public FixDeclaredArrayArgument() : base(RuleKind.ImportFunction)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var cleanStates = node.States;
            var openParen = cleanStates.GeNextTokenKind(TokenKind.OpenSquared);
            if (openParen == 0)
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
            var openParen = states.GeNextTokenKind(TokenKind.OpenSquared, pos);
            if (openParen == 0 || openParen > endRange)
                return;
            var closeParen = states.GeNextTokenKind(TokenKind.CloseSquared, openParen);
            var openNode = states[openParen];
            var closeNode = states[closeParen];
            states.RemoveAt(closeParen);
            states.RemoveAt(openParen);
            states.Insert(pos + 1, closeNode);
            states.Insert(pos + 1, openNode);

            states.Remap();
        }
    }
}