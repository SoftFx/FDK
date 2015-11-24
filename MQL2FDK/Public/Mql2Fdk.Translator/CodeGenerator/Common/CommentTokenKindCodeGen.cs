using System;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    class CommentTokenKindCodeGen : CodeGenForTokenKind
    {
        public CommentTokenKindCodeGen()
            : base(TokenKind.Comment)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            var contents = node.Content;
            return contents.StartsWith("//")
                       ? contents + Environment.NewLine
                       : contents;
        }
    }
}