using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations
{
    public class FixUninitializedVariableDeclarationsInInnerBlocks : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var functionNodes = node.ExtractFunctionNodes();
            foreach (var functionNode in functionNodes)
            {
                FixDeclarationsInFunctionBody(functionNode);
            }
        }

        void FixDeclarationsInFunctionBody(ParseNode functionNode)
        {
            var functionBodyPosition = functionNode.Children.GetNextOfRule(RuleKind.BlockCode);
            var functionBody = functionNode.Children[functionBodyPosition];
            var astVisitor = new AstTokenVisitor(functionBody, TokenKind.TypeName)
            {
                CallOnMatch = EvaluateDeclaration
            };
            astVisitor.Visit();
        }

        static void EvaluateDeclaration(ParseNode astNode)
        {
            var states = astNode.Parent.States;
            var typeIndex = states.MappedNodes.IndexOf(astNode);
            if (states.MappedNodes[typeIndex + 1].GetTokenKind() != TokenKind.Identifier)
                return;

            var squarePos = states.GeNextTokenKind(TokenKind.OpenSquared);
            if (squarePos != 0)
                return;
            var assingPos = states.GeNextTokenKind(TokenKind.Assign);
            if (assingPos != 0)
                return;
            var typeName = astNode.Content;

            var identifierPositions = states.GetAllOfToken(TokenKind.Identifier);
            if (identifierPositions.Count == 0)
                return;
            identifierPositions.ReverseEach(identifierPosition =>
                {
                    var nextTokenKind = states[identifierPosition + 1].Token;
                    if (nextTokenKind != TokenKind.Comma && nextTokenKind != TokenKind.SemiColon)
                        return;
                    var openParen = states.GeNextTokenKind(TokenKind.OpenParen);
                    if (openParen != 0)
                        return;
                    var insertNodes = BuildInsertNodes(typeName);
                    states.InsertRange(identifierPosition + 1, insertNodes);
                });
        }

        static List<ParseNode> BuildInsertNodes(string typeName)
        {
            var insertNodes = new List<ParseNode> { TokenKind.Assign.BuildTokenFromId() };
            switch (typeName)
            {
                case "bool":
                    insertNodes.Add(TokenKind.Identifier.BuildTokenFromId("false"));
                    break;
                case "int":
                    insertNodes.Add(TokenKind.Int.BuildTokenFromId("0"));
                    break;

                case "double":
                    insertNodes.Add(TokenKind.Float.BuildTokenFromId("0.0"));
                    break;
                case "string":
                    insertNodes.Add(TokenKind.QuotedString.BuildTokenFromId("\"\""));
                    break;
                case "color":
                    insertNodes.Add(TokenKind.New.BuildTokenFromId());
                    insertNodes.Add(TokenKind.TypeName.BuildTokenFromId(typeName));
                    insertNodes.Add(TokenKind.OpenParen.BuildTokenFromId());
                    insertNodes.Add(TokenKind.CloseParen.BuildTokenFromId());
                    break;
                case "datetime":
                    insertNodes.Add(TokenKind.New.BuildTokenFromId());
                    insertNodes.Add(TokenKind.TypeName.BuildTokenFromId(typeName));
                    insertNodes.Add(TokenKind.OpenParen.BuildTokenFromId());
                    insertNodes.Add(TokenKind.CloseParen.BuildTokenFromId());
                    break;
                default:
                    insertNodes.Add(TokenKind.Float.BuildTokenFromId("Unknown"));
                    break;
            }
            return insertNodes;
        }
    }
}