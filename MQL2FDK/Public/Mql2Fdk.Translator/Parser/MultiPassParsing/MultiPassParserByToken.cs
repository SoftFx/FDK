using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    abstract class MultiPassParserByToken : MultiPassParserBase
    {
        readonly TokenKind _tokenKind;

        protected MultiPassParserByToken(TokenKind tokenKind)
        {
            _tokenKind = tokenKind;
        }

        public override void Visit(List<ParseNode> astNodes)
        {
            foreach (var astNode in astNodes)
            {
                var visitor = new AstTokenVisitor(astNode, _tokenKind)
                    {
                        CallOnMatch = OnVisitMatch
                    };
                visitor.Visit();
            }
        }
    }
}