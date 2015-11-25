namespace Mql2Fdk.Translator.Common
{
    public enum TokenKind
    {
        None = 0,
        Comment,

        SharpProperty,
        SharpDefine,
        SharpImport,
        SharpInclude,

        QuotedString,
        Float,
        Int,
        Identifier,
        Dot,
        OpenParen,
        CloseParen,
        Space,
        Assign,
        Operator,

        SemiColon,
        OpenCurly,
        CloseCurly,
        Comma,
        OpenSquared,
        CloseSquared,

        TypeName,
        Static,
        Extern,
        Return,
        If,
        While,
        Else,
        For,
        Break,
        Continue,
        Switch,
        Case,
        PredefinedVariable,
        Tilde,

        //C# keywords mapped from Mq4
        Ref,
        Out,
        New,
        Default,
        Colon,
        Var,
        Char,
        Question,
        Throw,
        Input
    }
}