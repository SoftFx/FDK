using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Lexer
{
    static class TypeNames
    {
        public const string Unknown = "Unknown";
        public const string Int = "int";
        public const string Double = "double";
        public const string String = "string";
        public const string DateTime = "datetime";
        public const string Color = "color";
        public const string Void = "void";
        public const string Bool = "bool";
        public const string Char = "char";

        public const string ConvStr= "ConvStr";

        public static string ToInt = "ToInt";
        public const string ToBool = "ToBool";

        public static string ToDouble = "ToDouble";

        public static string NameOfType(this TokenKind tokenKind)
        {
            var typeName = "Unknown";
            switch (tokenKind)
            {
                case TokenKind.Int:
                    typeName = Int;
                    break;
                case TokenKind.Float:
                    typeName = Double;
                    break;
                case TokenKind.QuotedString:
                    typeName = String;
                    break;
            }
            return typeName;
        }
    }
}