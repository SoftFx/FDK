using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    class BreakBlockParser : MultiPassParserByToken
    {
        public BreakBlockParser()
            : base(TokenKind.Break)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            var cleanStates = new CleanupAstNodeStates(node.Parent.Children);
            var advance = cleanStates.MappedNodes.IndexOf(node);
            cleanStates.ShiftBreak(advance);
        }
    }

    internal class ContinueBlockParser : MultiPassParserByToken
    {
        public ContinueBlockParser()
            : base(TokenKind.Continue)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            var cleanStates = new CleanupAstNodeStates(node.Parent.Children);
            var advance = cleanStates.MappedNodes.IndexOf(node);
            cleanStates.ShiftContinue(advance);
        }
    }
}