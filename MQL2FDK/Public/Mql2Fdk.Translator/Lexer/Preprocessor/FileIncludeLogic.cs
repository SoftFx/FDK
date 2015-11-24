using System.Collections.Generic;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer.Preprocessor
{
    static class FileIncludeLogic
    {
        public static void ScanAndImportIncludes(List<ParseNode> tokens)
        {
            bool found;
            do
            {
                found = false;
                tokens.ReverseEachWithIndex((token, index) =>
                    {
                        if (token.Token != TokenKind.SharpInclude)
                            return;
                        found = true;
                        SharpIncludeTokenAsContent(tokens, index, token);
                    });
            } while (found);
        }

        static void SharpIncludeTokenAsContent(List<ParseNode> tokens, int index, ParseNode token)
        {
            var tokenContent = token.Content.Remove(0, "#include".Length);
            if (tokenContent.Contains("<"))
            {
                var startIndex = tokenContent.IndexOf("<");
                tokenContent = tokenContent.Substring(startIndex + 1, tokenContent.Length - startIndex - 1);
                startIndex = tokenContent.IndexOf(">");
                tokenContent = tokenContent.Substring(0, startIndex);
            }
            else if (tokenContent.Contains("\""))
            {
                var startIndex = tokenContent.IndexOf("\"");
                tokenContent = tokenContent.Substring(startIndex + 1, tokenContent.Length - startIndex - 1);
                startIndex = tokenContent.IndexOf("\"");
                tokenContent = tokenContent.Substring(0, startIndex);
            }
            var content = SearchPathsIncluder.IncludeFile(tokenContent);
            tokens.RemoveAt(index);
            tokens.InsertRange(index, content);
        }
    }
}