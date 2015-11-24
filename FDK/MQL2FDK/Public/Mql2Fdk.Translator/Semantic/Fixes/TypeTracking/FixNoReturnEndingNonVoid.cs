using System;
using System.IO;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixNoReturnEndingNonVoid : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.GetFunctionBodies();
            foreach (var functionNode in functionNodes)
            {
                EvaluateFunctionReturn(functionNode);
            }
        }

        void EvaluateFunctionReturn(ParseNode functionNode)
        {
            var parentFunctionDeclaration = functionNode.Parent.States;
            if (parentFunctionDeclaration[0].Token != TokenKind.TypeName)
                return;
            var functionTypeName = parentFunctionDeclaration[0].Content;
            if (functionTypeName == "void")
                return;
            var blockStates = functionNode.States;
            var expectedReturnToken = blockStates[blockStates.Count - 2];
            if (expectedReturnToken.Rule == RuleKind.Return)
                return;
            var returnNode = new ParseNode(RuleKind.Return);

            ParseNode defaultValue;
            switch (functionTypeName)
            {
                case TypeNames.Bool:
                    defaultValue = TokenKind.Identifier.BuildTokenFromId("false");
                    break;
                case TypeNames.Int:
                    defaultValue = TokenKind.Int.BuildTokenFromId("0");
                    break;
                case TypeNames.Double:
                    defaultValue = TokenKind.Float.BuildTokenFromId("0.0");
                    break;
                case TypeNames.String:
                    defaultValue = TokenKind.QuotedString.BuildTokenFromId("\"\"");
                    break;
                case TypeNames.Color:
                    defaultValue = TokenKind.Identifier.BuildTokenFromId("Black");
                    break;
                default:
                    throw new InvalidDataException("case not handled");
            }
            var nodesToAdd = new[]
                {
                    TokenKind.Comment.BuildTokenFromId("// this is added by the translator. Consider it adding in your script "),
                    TokenKind.Return.BuildTokenFromId(),
                    TokenKind.OpenParen.BuildTokenFromId(),
                    defaultValue,
                    TokenKind.CloseParen.BuildTokenFromId(),
                    TokenKind.SemiColon.BuildTokenFromId(),
                };
            returnNode.AddRange(nodesToAdd);
            blockStates.Insert(blockStates.Count - 1, returnNode);
        }
    }
}