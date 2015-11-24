using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    class ElseBlockParser : MultiPassParserByToken
    {
        public ElseBlockParser()
            : base(TokenKind.Else)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            var cleanStates = new CleanupAstNodeStates(node.Parent.Children);
            var advance = cleanStates.MappedNodes.IndexOf(node);
            cleanStates.ShiftElse(advance);
        }
    }

    class SwitchBlockParser : MultiPassParserByToken
    {
        public SwitchBlockParser()
            : base(TokenKind.Switch)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            var cleanStates = new CleanupAstNodeStates(node.Parent.Children);
            var advance = cleanStates.MappedNodes.IndexOf(node);
            cleanStates.ShiftSwitch(advance);
        }
    }
}