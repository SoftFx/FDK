using System.Collections.Generic;
using System.IO;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer.Preprocessor
{
    static class SearchPathsIncluder
    {
        public static List<ParseNode> IncludeFile(string fileName)
        {
            var lexer = new Mq4Lexer();
            if (IncludePaths.Instance.BlackSet.Contains(fileName))
                return new List<ParseNode>();
            if (!File.Exists(fileName))
            {
                var directories = IncludePaths.Instance.DirectoryList;
                foreach (var directory in directories)
                {
                    var fullPath = Path.Combine(directory, fileName);
                    if (!File.Exists(fullPath)) continue;
                    fileName = fullPath;
                    break;
                }
            }
            return lexer.BuildFileTokens(fileName);
        }
    }
}