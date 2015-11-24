using System.Collections.Generic;
using System.IO;

namespace Mql2Fdk.Translator.Lexer.Preprocessor
{
    public class IncludePaths
    {
        public readonly HashSet<string> DirectoryList = new HashSet<string>();
        public readonly HashSet<string> BlackSet = new HashSet<string>();
        public static IncludePaths Instance { get; private set; }

        static IncludePaths()
        {
            Instance = new IncludePaths();
        }

        public static void BlackListIncludeFile(string fileName)
        {
            Instance.BlackSet.Add(fileName);
        }

        public static void AddIncludeDirectoryOfFile(string fileName)
        {
            var info = new FileInfo(fileName);
            var directoryName = info.DirectoryName;
            AddDirectoryInclude(directoryName);
        }

        public static void AddDirectoryInclude(string directoryName)
        {
            var includePaths = Instance.DirectoryList;

            if (!includePaths.Contains(directoryName))
                includePaths.Add(directoryName);
        }
    }
}