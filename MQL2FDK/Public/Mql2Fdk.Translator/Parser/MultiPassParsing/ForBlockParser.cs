using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    class ForBlockParser : MultiPassParserByToken
    {
        public ForBlockParser()
            : base(TokenKind.For)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            var cleanStates = new CleanupAstNodeStates(node.Parent.Children);
            var advance = cleanStates.MappedNodes.IndexOf(node);
            cleanStates.ShiftFor(advance);
        }
    }
}