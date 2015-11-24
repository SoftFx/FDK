using System;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    class NewLineCodeGenForTokenKind : CodeGenForTokenKind
    {
        public NewLineCodeGenForTokenKind(TokenKind tokenKind) : base(tokenKind)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            return node.Content + Environment.NewLine;
        }
    }
}