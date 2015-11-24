using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.MultiPassParsing;

namespace Mql2Fdk.Translator.Parser
{
    public class Mq4Parser
    {
        readonly List<MultiPassParserBase> _simpleParseMergers = new List<MultiPassParserBase>();

        public Mq4Parser()
        {
            _simpleParseMergers = new List<MultiPassParserBase>
                {
                    new ReturnBlockParser(),
                    new BreakBlockParser(),
                    new ContinueBlockParser(),
                    new ForBlockParser(),
                    new ElseBlockParser(),
                    new SwitchBlockParser(),
                    new IfBlockParser(),
                    new WhileBlockParser(),
                    new FunctionBodyParser(),
                };
        }

        public ParseNode Parse(List<ParseNode> resultTokens)
        {
            var parserTree = new ParserTree {Nodes = resultTokens};
            parserTree.HighLevelParse();

            foreach (var merger in _simpleParseMergers)
            {
                merger.Visit(parserTree.Nodes);
            }


            var result = parserTree.Root;
            return result;
        }
    }
}