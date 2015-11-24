using System;
using System.IO;
using System.Linq;
using Mql2Fdk.Attributes;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.ParseRules
{
    static class SharpPropertyRule
    {
        public static void ParseData(ParseNode node)
        {
            var tokenData = node.Children.First();
            var defineText = tokenData.Content.Remove(0, "#property ".Length);

            var lexer = new Mq4Lexer();
            var defineTokens = lexer.BuildTextTokens(defineText);
            var nodes = defineTokens.Select(token => token.BuildTerminalNode()).ToList();
            var states = new CleanupAstNodeStates(nodes);

            var assemblyOfAttributes = typeof(indicator_color1Attribute).Assembly;
            var fullTypeName = string.Format("Mql2Fdk.Attributes.{0}Attribute", states[0].Content);
            var fullTypeNameReduced = string.Format("Mql2Fdk.Attributes.{0}", states[0].Content);
            var getTypeofAttribute =
                assemblyOfAttributes.GetType(fullTypeName)
                ?? assemblyOfAttributes.GetType(fullTypeNameReduced);
            bool needsQuotes = false;
            var typeName = getTypeofAttribute!=null 
                                  ? ComputeTypenameFromReflection(getTypeofAttribute, states, ref needsQuotes) 
                                  : ComputeTypeFromStatements(states, ref needsQuotes);

            var variableToken = states.MappedNodes[0];
            var valueToken = states.Count != 1
                                 ? states.MappedNodes[1]
                                 : new TokenData(0, 0, TokenKind.Int, "1").BuildTerminalNode();
            if (needsQuotes)
            {
                var finalTokenData = valueToken.GetTokenData();
                finalTokenData.Token = TokenKind.QuotedString;
                finalTokenData.Content = string.Format("\"{0}\"", finalTokenData.Content);
            }
          
            node.Children.Clear();
            var insertTokenType = new TokenData(0, 0, TokenKind.TypeName, typeName);

            var buildTerminalToken = insertTokenType.BuildTerminalNode();
            node.Add(buildTerminalToken);
            node.AddTerminalToken(new TokenData(0, 0, TokenKind.Space, " "));
            node.Add(variableToken);
            node.AddTerminalToken(new TokenData(0, 0, TokenKind.Assign, "="));
           
            node.Add(valueToken);
            var colon = new TokenData(0, 0, TokenKind.SemiColon, ";").BuildTerminalNode();
            node.Children.Add(colon);
        }

        static string ComputeTypeFromStatements(CleanupAstNodeStates states, ref bool needsQuotes)
        {
            var typeName = "int";

            if (states.Count != 1)
            {
                var definedConstantType = states[1].Token;
                switch (definedConstantType)
                {
                    case TokenKind.Int:
                        typeName = "int";
                        break;
                    case TokenKind.Float:
                        typeName = "double";
                        break;
                    case TokenKind.QuotedString:
                        typeName = "string";
                        break;
                    case TokenKind.Identifier:
                        typeName = "string";
                        needsQuotes = true;
                        break;
                    default:
                        throw new InvalidDataException("Type not supported");
                }
            }
            return typeName;
        }

        static string ComputeTypenameFromReflection(Type getTypeofAttribute, CleanupAstNodeStates states,
                                                            ref bool needsQuotes)
        {
            var typeName = "int";
            var valueProperty = getTypeofAttribute.GetProperty("Value");
            if (valueProperty == null)
                return "bool";
            var paramType = valueProperty.PropertyType;

            needsQuotes = states[1].Token == TokenKind.Identifier;
            if (paramType == typeof(int))
            {
                typeName = "int";
            }
            else if (paramType == typeof(color))
            {
                typeName = "color";
            }
            else if (paramType == typeof(string))
            {
                typeName = "string";
            }
            else if (paramType == typeof(double))
            {
                typeName = "double";
            }
            else if (paramType == typeof(datetime))
            {
                typeName = "datetime";
            }
            return typeName;
        }
    }
}