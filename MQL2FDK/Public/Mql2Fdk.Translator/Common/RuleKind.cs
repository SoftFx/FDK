namespace Mql2Fdk.Translator.Common
{
    public enum RuleKind
    {
        Terminal = 0,
        Assignment,
        DeclareVariable,
        DefineDeclaration,
        Extern,
        FunctionDeclaration,
        ImportLibraryDeclaration,
        Return,
        SharpProperty,
        Static,
        InstructionCode,
        BlockCode,
        ConditionalCode,
        If,
        ImportFunction,
        While,
        For,
        Break,
        Else,
        FunctionSignature,
        Root,
        Switch,
        FunctionForwardDeclaration,
        Case,
        CaseBlock,
        DeclareConstant,
        Input
    }
}