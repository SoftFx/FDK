using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class FixIdentifierNames : SemanticFixForToken
    {
        static readonly Dictionary<string, string> WordsMapping = new Dictionary<string, string>();
        static FixIdentifierNames()
        {
            WordsMapping["NULL"] = "null";
            WordsMapping["goto"] = "goto_";
            WordsMapping["TRUE"] = "true";
            WordsMapping["True"] = "true";
            WordsMapping["FALSE"] = "false";
            WordsMapping["False"] = "false";
        }

        public FixIdentifierNames()
            : base(TokenKind.Identifier)
        {
        }

        protected override void FixLogic(ParseNode node)
        {
            var tokenContent = node.GetTokenData();
            string replaceWord;
            if (WordsMapping.TryGetValue(tokenContent.Content, out replaceWord))
            {
                tokenContent.Content = replaceWord;
            }
        
        }
    }
}