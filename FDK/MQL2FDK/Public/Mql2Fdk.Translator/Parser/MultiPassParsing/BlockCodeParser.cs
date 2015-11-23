using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    class BlockCodeParser : MultiPassParserByToken
    {
        public BlockCodeParser()
            : base(TokenKind.OpenCurly)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            var cleanStates = new CleanupAstNodeStates(node.Parent.Children);
            var advance = cleanStates.MappedNodes.IndexOf(node);
            if (advance == 0)
                return;
            cleanStates.ShiftBlock(advance);
        }
    }
}