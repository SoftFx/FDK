using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.HighLevelParsing;

namespace Mql2Fdk.Translator.Parser
{
    class ParserTree
    {
        public List<ParseNode> Nodes;

        public HighLevelParser HighLevelParser;

        #region Tree handling

        public ParseNode BuildTree()
        {
            var result = new ParseNode(RuleKind.Root);
            result.AddRange(Nodes);
            return result;
        }

        #endregion

        public void HighLevelParse()
        {
            Root = BuildTree();
            HighLevelParser = new HighLevelParser(Root);
            HighLevelParser.Interpret();
        }

        public ParseNode Root { get; set; }
    }
}