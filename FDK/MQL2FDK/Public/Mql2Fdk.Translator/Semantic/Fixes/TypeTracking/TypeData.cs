using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    public class TypeData
    {
        public List<ParseNode> TokenList { get; private set; }

        public int Count
        {
            get { return TokenList.Count; }
        }

        public TypeData()
        {
            TokenList = new List<ParseNode>();
        }

        public override string ToString()
        {
            return string.Join(" ", TokenList.Select(token => token.Content));
        }

        public void AddTypeNode(ParseNode mappedNode)
        {
            var tokenData = mappedNode;
            ValidateToken(tokenData);
            TokenList.Add(tokenData);
        }

        public static void ValidateToken(ParseNode tokenData)
        {
            switch (tokenData.Token)
            {
                case TokenKind.TypeName:
                case TokenKind.Ref:
                case TokenKind.OpenSquared:
                case TokenKind.CloseSquared:
                case TokenKind.Comma:
                    break;
                default:
                    throw new InvalidDataException("Unknown type kind");
            }
        }

        public bool Equals(TypeData typeData)
        {
            if (typeData == null)
                return false;
            if (TokenList.Count != typeData.TokenList.Count)
                return false;
            for (var index = 0; index < TokenList.Count; index++)
            {
                var data = TokenList[index].Content;
                var otherData = typeData.TokenList[index].Content;
                if (data != otherData)
                    return false;
            }
            return true;
        }

        public ParseNode this[int i]
        {
            get { return TokenList[i]; }
        }
    }
}