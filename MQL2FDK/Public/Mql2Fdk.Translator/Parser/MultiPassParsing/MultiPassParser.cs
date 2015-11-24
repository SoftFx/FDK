using System.Collections.Generic;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    abstract class MultiPassParserBase
    {
        public abstract void Visit(List<ParseNode> astNodes);

        public bool Result { get; set; }

        public abstract void OnVisitMatch(ParseNode node);
    }
}