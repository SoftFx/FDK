using System.IO;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    /// <summary>
    /// Fixes the case when method is not null and user writes only 'return;'
    /// </summary>
    class FixEmptyReturnForNonVoidMethod : SemanticFixForRule
    {
        public FixEmptyReturnForNonVoidMethod()
            : base(RuleKind.Return)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var states = node.States[0].States;
            if(states.Count!=2)
                return;
            var functionNode = node.GetParentFunctionDeclaration();
            var parentFunctionDeclaration = functionNode.States;
            if (parentFunctionDeclaration[0].Token != TokenKind.TypeName)
                return;
            var functionTypeName = parentFunctionDeclaration[0].Content;
            if (functionTypeName == "void")
                return;
            states.InsertRange(1, new[]
                {
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.CloseParen.BuildTokenFromId(),
                });
            switch (functionTypeName)
            {
                case TypeNames.Int:
                    states.Insert(2, TokenKind.Int.BuildTokenFromId("0"));
                    break;
                case TypeNames.Double:
                    states.Insert(2, TokenKind.Float.BuildTokenFromId("0.0"));
                    break;
                default:
                    throw new InvalidDataException("Invalid return type");
            }
            states.Remap();
        }
    }
}