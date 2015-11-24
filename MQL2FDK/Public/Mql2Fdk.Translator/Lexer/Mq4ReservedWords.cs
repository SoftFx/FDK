using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer
{
    public class Mq4ReservedWords
    {
        Mq4ReservedWords()
        {
            ReservedWords = new Dictionary<string, TokenKind>();
            ReverseKeywords = new Dictionary<TokenKind, string>();
        }

        static Mq4ReservedWords()
        {
            Instance = BuildReservedWords();
        }

        Dictionary<string, TokenKind> ReservedWords { get; set; }
        Dictionary<TokenKind, string> ReverseKeywords { get; set; }
        public static Mq4ReservedWords Instance { get; private set; }

        public static Mq4ReservedWords BuildReservedWords()
        {
            var reservedWords = new Mq4ReservedWords();
            reservedWords.InitializeWithDefaults();
            return reservedWords;
        }

        void InitializeWithDefaults()
        {
            InitializeTypeNames();


            InitializeReservedWords();

            MapReverseWords();
        }

        public LexDefinition[] GetMatchDefinitions()
        {
            return ReverseKeywords.Select(
                reverseKeyword
                => new LexDefinition(new MatchExactText(reverseKeyword.Value), reverseKeyword.Key))
                .ToArray();
        }

        void MapReverseWords()
        {
            foreach (var reserved in ReservedWords)
                ReverseKeywords[reserved.Value] = reserved.Key;
        }

        public ParseNode BuildTokenFromId(TokenKind token, string content = "")
        {
            string reverseKeyword;
            if (!string.IsNullOrEmpty(content))
                return new ParseNode(token, content);
            if (ReverseKeywords.TryGetValue(token, out reverseKeyword))
                return new ParseNode(token, reverseKeyword);
            return new ParseNode(token, content);
        }

        void InitializeReservedWords()
        {
            AddReservedWord("static", TokenKind.Static);
            AddReservedWord("extern", TokenKind.Extern);
            AddReservedWord("return", TokenKind.Return);
            AddReservedWord("if", TokenKind.If);
            AddReservedWord("else", TokenKind.Else);
            AddReservedWord("while", TokenKind.While);
            AddReservedWord("for", TokenKind.For);
            AddReservedWord("break", TokenKind.Break);
            AddReservedWord("continue", TokenKind.Continue);
            AddReservedWord("switch", TokenKind.Switch);
            AddReservedWord("case", TokenKind.Case);
            AddReservedWord("default", TokenKind.Default);

            AddReservedWord("ref", TokenKind.Ref);
            AddReservedWord("out", TokenKind.Out);
            AddReservedWord("new", TokenKind.New);
            AddReservedWord("var", TokenKind.Var);
            AddReservedWord("throw", TokenKind.Throw);

            AddReservedWord("=", TokenKind.Assign);

            AddReservedWord(",", TokenKind.Comma);
            AddReservedWord(".", TokenKind.Dot);

            AddReservedWord("~", TokenKind.Tilde);

            AddReservedWord(";", TokenKind.SemiColon);
            AddReservedWord(":", TokenKind.Colon);

            AddReservedWord("[", TokenKind.OpenSquared);
            AddReservedWord("]", TokenKind.CloseSquared);
            AddReservedWord("(", TokenKind.OpenParen);
            AddReservedWord(")", TokenKind.CloseParen);

            AddReservedWord("?", TokenKind.Question);
            AddReservedWord("{", TokenKind.OpenCurly);
            AddReservedWord("}", TokenKind.CloseCurly);
        }

        void AddReservedWord(string word, TokenKind tokenKind)
        {
            ReservedWords[word] = tokenKind;
        }

        void InitializeTypeNames()
        {
            AddTypeName(TypeNames.Void);
            AddTypeName(TypeNames.Int);
            AddTypeName(TypeNames.Bool);
            AddTypeName(TypeNames.Char);
            AddTypeName(TypeNames.String);
            AddTypeName(TypeNames.Double);
            AddTypeName(TypeNames.Color);
            AddTypeName(TypeNames.DateTime);
        }

        void AddTypeName(string typeName)
        {
            ReservedWords[typeName] = TokenKind.TypeName;
        }

        public static TokenKind GetReservedWordKind(string text)
        {
            TokenKind result;
            if (Instance.ReservedWords.TryGetValue(text, out result))
                return result;
            return TokenKind.None;
        }

        public List<ParseNode> UpdateTokenStates(List<ParseNode> regexTokens)
        {
            var result = new List<ParseNode>();
            foreach (var tokenData in regexTokens)
            {
                var newTokenData = tokenData;
                TokenKind newTokenKind;
                if (tokenData.Token != TokenKind.Comment &&
                    ReservedWords.TryGetValue(tokenData.Content, out newTokenKind))
                {
                    newTokenData = new ParseNode(newTokenKind,
                                                 tokenData.Content);
                }
                result.Add(newTokenData);
            }
            return result;
        }
    }
}