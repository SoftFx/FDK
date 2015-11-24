using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer
{
    public class TokenData
    {
        readonly int _lineNumber;
        readonly int _position;

        public TokenData(int lineNumber, int position, TokenKind kind, string content)
        {
            _lineNumber = lineNumber;
            _position = position;
            Kind = kind;
            Content = content;
        }

        public int LineNumber
        {
            get { return _lineNumber; }
        }

        public int Position
        {
            get { return _position; }
        }

        public TokenKind Kind { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public TokenData Clone()
        {
            return new TokenData(LineNumber, Position, Kind, Content);
        }
    }
}