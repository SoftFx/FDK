using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    abstract class MultiPassParserByRule : MultiPassParserBase
    {
        readonly RuleKind _rule;

        protected MultiPassParserByRule(RuleKind rule)
        {
            _rule = rule;
        }

        public override void Visit(List<ParseNode> astNodes)
        {
            foreach (var astNode in astNodes)
            {
                var visitor = new AstTreeVisitorRule(astNode, _rule)
                    {
                        CallOnMatch = OnVisitMatch
                    };
                visitor.Visit();
            }
        }
    }
}