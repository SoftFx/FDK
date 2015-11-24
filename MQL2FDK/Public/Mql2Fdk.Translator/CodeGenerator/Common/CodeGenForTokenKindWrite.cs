using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    class CodeGenForTokenKindWrite : CodeGenForTokenKind
    {
        public CodeGenForTokenKindWrite(TokenKind tokenKind) : base(tokenKind)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            return node.Content;
        }
    }
}