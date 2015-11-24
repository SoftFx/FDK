using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class FixAssignmentWithConstants : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var fullVisitor = new AstTreeVisitorAllNodes(node) {CallOnMatch = AnalyzeNode};
            fullVisitor.Visit();
        }

        void AnalyzeNode(ParseNode node){
        var states = node.States;
            if (states.All(it => it.Token != TokenKind.Assign))
                return;
            if(states.Count<4)
                return;
            var lastIndex = states.Count - 1;
            if (states[lastIndex].Token != TokenKind.SemiColon)
                return;
            if (states[lastIndex-1].Token == TokenKind.Identifier)
                return;
            if (states[lastIndex - 2].Token != TokenKind.Assign)
                return;
            if (states[lastIndex - 3].Token != TokenKind.Identifier)
                return;
            if(node.Rule==RuleKind.DeclareVariable)
            {
                FixDeclarationProblem(states);
                return;
            }

            FixSimpleAssignmentInFunctionProblem(node, states);
        }

        void FixSimpleAssignmentInFunctionProblem(ParseNode node, CleanupAstNodeStates states)
        {
            if (states.Count != 4) return;
            var functionNode = node.GetParentFunctionBlockNode();
            var declarations = functionNode.GetDeclarations();
            var varName = states[0].Content;
            var typeData = declarations.GetTypeOfName(varName);
            if (typeData.Count != 1)
                return;
            var type = typeData[0].Content;
            FixAssignmentWithConstant(states, 2, type);
        }

        protected void FixDeclarationProblem(CleanupAstNodeStates states)
        {
            if(states.Count!=5)return;
            var type = states[0].Content;

            FixAssignmentWithConstant(states, 3, type);
        }

        void FixAssignmentWithConstant(CleanupAstNodeStates states, int constantPosition, string type)
        {
            var value = states[constantPosition].Content;

            switch (type)
            {
                case TypeNames.Bool:
                    if (states[constantPosition].Token == TokenKind.Int)
                    {
                        FixIntAsBool(states, value);
                        return;
                    }
                    throw new InvalidDataException("case with bool not handled");

                case TypeNames.Int:
                    if (states[constantPosition].Token == TokenKind.Float)
                    {
                        FixFloatAsInt(states, value, constantPosition);
                        return;
                    }
                    if (states[constantPosition].Token == TokenKind.Char)
                    {
                        FixCharAsInt(states, value, constantPosition);
                        return;
                    }
                    if (states[constantPosition].Token != TokenKind.Int)
                        throw new InvalidDataException("case with int32 not handled");
                    break;
                case TypeNames.Double:
                    break;
                case TypeNames.String:
                    if (states[constantPosition].Token != TokenKind.QuotedString)
                        throw new InvalidDataException("case with string not handled");
                    break;

                case TypeNames.DateTime:
                    if (states[constantPosition].Content != "0")
                        throw new InvalidDataException("case with datetime not handled");
                    break;
                case TypeNames.Color:
                    if (states[constantPosition].Content != "0")
                        throw new InvalidDataException("case with color not handled");
                    break;
                default:
                    throw new InvalidDataException("case not handled");
            }
        }

        void FixCharAsInt(CleanupAstNodeStates states, string value, int constantPosition)
        {
            states[constantPosition].Token = TokenKind.Int;
            states[constantPosition].Content = ((int)value[1]).ConvStr();
            var toAdd = new List<ParseNode>
                {
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.Identifier.BuildTokenFromId(string.Format("/*\'{0}\'*/",value) ),
                    TokenKind.CloseParen.BuildTokenFromId(),
                };
            states.InsertRange(constantPosition, toAdd);
        }

        static void FixFloatAsInt(CleanupAstNodeStates states, string value, int constantPosition)
        {
            states[constantPosition].Token = TokenKind.Int;
            states[constantPosition].Content = ((int)double.Parse(value)).ConvStr();
        }

        static void FixIntAsBool(CleanupAstNodeStates states, string value)
        {
            states[3].Token = TokenKind.Identifier;
            states[3].Content = int.Parse(value) != 0 ? "true" : "false";
        }
    }
}