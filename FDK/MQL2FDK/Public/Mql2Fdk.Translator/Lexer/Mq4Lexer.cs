using System.Collections.Generic;
using System.IO;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer
{
    public class Mq4Lexer
    {
        readonly LexerDefinitions _definitions;
        readonly Mq4ReservedWords _mq4ReservedWords;


        public int IsMultiLineComment;

        public Mq4Lexer()
        {
            _mq4ReservedWords = Mq4ReservedWords.Instance;
            _definitions = new LexerDefinitions(this);
        }

        Lexer BuilderLexer(TextReader reader)
        {
            var lexDefinitions = _definitions.Definitions;
            lexDefinitions.AddRange(_mq4ReservedWords.GetMatchDefinitions());
            return new Lexer(reader, lexDefinitions);
        }

        Lexer BuilderLexer(string text)
        {
            return BuilderLexer(new StringReader(text));
        }

        public List<ParseNode> BuildFileTokens(string fileName)
        {
            var text = File.ReadAllText(fileName);

            return BuildTextTokens(text);
        }

        public List<ParseNode> BuildTextTokens(string text)
        {
            var lexer = BuilderLexer(text);
            var regexTokens = BuildTokens(lexer);
            var resultTokens = _mq4ReservedWords.UpdateTokenStates(regexTokens);
            return resultTokens;
        }

        static List<ParseNode> BuildTokens(Lexer lexer)
        {
            var result = new List<ParseNode>();
            while (lexer.Next())
            {
                var token = new ParseNode(lexer.Token, lexer.TokenContents);
                result.Add(token);
            }

            return result;
        }
    }
}