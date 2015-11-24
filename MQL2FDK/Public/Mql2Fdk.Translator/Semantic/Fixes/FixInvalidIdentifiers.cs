using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class FixInvalidIdentifiers : SemanticFixForToken
    {
        public FixInvalidIdentifiers()
            : base(TokenKind.Identifier)
        {
        }

        protected override void FixLogic(ParseNode node)
        {
            var tokenContent = node.GetTokenData();
            var content = tokenContent.Content
                .Replace('$', '_')
                .Replace('.', '_')
                ;
            tokenContent.Content = content;
        }
    }
}