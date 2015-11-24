using System.IO;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.ParseRules
{
    static class DefineDeclaration
    {
        public static void ParseData(ParseNode node)
        {
            var tokenData = node.Children.First();
            var defineText = tokenData.Content.Remove(0, "#define ".Length);

            var lexer = new Mq4Lexer();
            var defineTokens = lexer.BuildTextTokens(defineText);
            var nodes = defineTokens.Select(token => token.BuildTerminalNode()).ToList();
            var states = new CleanupAstNodeStates(nodes);
            var definedConstantType = states.MappedNodes[1].Token;
            string typeName;
            switch (definedConstantType)
            {
                case TokenKind.Int:
                case TokenKind.Float:
                    typeName = definedConstantType.NameOfType();
                    break;
                case TokenKind.QuotedString:
                    typeName = "string";
                    break;

                case TokenKind.Operator:
                    typeName = states.MappedNodes[2].Token.NameOfType();
                    break;
                case TokenKind.Identifier:
                    typeName = ComputeConstTypeFromPreviousConstants(node, states.MappedNodes[1].Content);
                    break;
                default:
                    throw new InvalidDataException("Type not supported");
            }
            var insertTokenType = new ParseNode(TokenKind.TypeName, typeName);

            var buildTerminalToken = insertTokenType.BuildTerminalNode();
            var variableToken = states[0];
            var valueToken = states[1];
            var valueToken2 = definedConstantType == TokenKind.Operator ? states.MappedNodes[2] : null;
            node.Children.Clear();
            node.Add(buildTerminalToken);
            node.AddTerminalToken(new TokenData(0, 0, TokenKind.Space, " "));
            node.Add(variableToken);
            node.AddTerminalToken(new TokenData(0, 0, TokenKind.Assign, "="));
            node.Add(valueToken);
            if (definedConstantType == TokenKind.Operator)
            {
                node.Add(valueToken2);
            }
            var colon = new ParseNode(TokenKind.SemiColon, ";");
            node.Rule = RuleKind.DeclareConstant;
            node.Children.Add(colon);
        }

        static string ComputeConstTypeFromPreviousConstants(ParseNode node, string mappedNode)
        {
            var declarations = node.Parent.Children
                .Where(child => child.Rule == RuleKind.DeclareConstant)
                .Select(item => item.States)
                .ToArray();
            if (declarations.Length == 0)
            {
                return RegenerateSharpDefines(node, mappedNode);
            }

            foreach (var statesOfDeclaration in declarations)
            {
                var defineVarNameNode = statesOfDeclaration.First(it => it.Token == TokenKind.Identifier);
                if (defineVarNameNode.Content == mappedNode)
                    return statesOfDeclaration[0].Content;
            }
            return RegenerateSharpDefines(node, mappedNode);
        }

        static string RegenerateSharpDefines(ParseNode node, string mappedNode)
        {
            var notYetParsedDefines = node.Parent.Children
                .Where(it =>
                    {
                        return ValidateNodeAsSearchedDefine(mappedNode, it);
                    })
                .ToArray();
            if (notYetParsedDefines.Length == 0)
                return "color";
            foreach (var notYetParsedDefine in notYetParsedDefines)
            {
                ParseData(notYetParsedDefine);
            }

            return ComputeConstTypeFromPreviousConstants(node, mappedNode);
        }

        static bool ValidateNodeAsSearchedDefine(string mappedNode, ParseNode it)
        {
            if (it.Rule != RuleKind.DefineDeclaration) return false;
            if (it.Children.Count <= 1) return false;
            var states = it.States;

            return states.Count >= 1
                && states[1].Content == mappedNode;
        }
    }
}