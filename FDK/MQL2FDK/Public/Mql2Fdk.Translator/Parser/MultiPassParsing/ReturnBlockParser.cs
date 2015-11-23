using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    class ReturnBlockParser : MultiPassParserByToken
    {
        public ReturnBlockParser()
            : base(TokenKind.Return)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            var cleanStates = new CleanupAstNodeStates(node.Parent.Children);
            var advance = cleanStates.MappedNodes.IndexOf(node);
            cleanStates.ShiftReturn(advance);
        }
    }
}