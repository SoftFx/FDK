using System.Collections.Generic;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer
{
    public class LexerDefinitions
    {
        readonly Mq4Lexer _mq4Lexer;

        public LexerDefinitions(Mq4Lexer mq4Lexer)
        {
            _mq4Lexer = mq4Lexer;
            Definitions = new List<LexDefinition>();
            Definitions.AddRange(new[]
                {
                    new LexDefinition(new MatchMultiLineCommentText(_mq4Lexer), TokenKind.Comment),
                    new LexDefinition(new MatchStartText("//"), TokenKind.Comment),
                    new LexDefinition(new MatchStartText("#property"), TokenKind.SharpProperty),
                    new LexDefinition(new MatchStartText("#define"), TokenKind.SharpDefine),
                    new LexDefinition(new MatchStartText("#import"), TokenKind.SharpImport),
                    new LexDefinition(new MatchStartText("#include"), TokenKind.SharpInclude),
                    new LexDefinition(@"\s", TokenKind.Space),

                    // Thanks to http://www.regular-expressions.info/floatingpoint.html
                    new LexDefinition(@"[-+]?\d*\.\d+([eE][-+]?\d+)?", TokenKind.Float),
                    new LexDefinition(new HexMatcher(), TokenKind.Int),
                    new LexDefinition(@"[-+]?\d+", TokenKind.Int),
                    new LexDefinition(new UnicodeIdentifierMatcher(), TokenKind.Identifier),
                    new LexDefinition(new MatchQuotedString(), TokenKind.QuotedString),
                    new LexDefinition(new MatchChar(), TokenKind.Char),
                    new LexDefinition(new MatchExactTextInList(
                                          new[]
                                              {
                                                  "+", "-", "*", "/", "%",
                                                  "<=", ">=", "<", ">",
                                                  "!=", "!",
                                                  "&&", "||",
                                                  "&", "|",
                                                  "^",
                                                  "=="
                                              }), TokenKind.Operator),
                });
        }

        public List<LexDefinition> Definitions { get; private set; }
    }
}