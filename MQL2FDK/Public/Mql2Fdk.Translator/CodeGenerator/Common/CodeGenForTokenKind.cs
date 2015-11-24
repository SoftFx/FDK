using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    abstract class CodeGenForTokenKind : CodeGenForNode
    {
        readonly TokenKind _tokenKind;

        protected CodeGenForTokenKind(TokenKind tokenKind)
        {
            _tokenKind = tokenKind;
        }

        public override bool Accept(ParseNode node)
        {
            return node.GetTokenKind() == _tokenKind;
        }
    }
}