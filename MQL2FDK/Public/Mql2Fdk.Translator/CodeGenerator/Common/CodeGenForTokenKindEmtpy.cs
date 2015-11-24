using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    class CodeGenForTokenKindEmtpy : CodeGenForTokenKind
    {
        public CodeGenForTokenKindEmtpy(TokenKind tokenKind)
            : base(tokenKind)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            return string.Empty;
        }
    }
}