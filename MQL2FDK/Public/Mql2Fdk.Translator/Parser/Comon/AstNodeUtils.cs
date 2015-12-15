using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;

namespace Mql2Fdk.Translator.Parser.Comon
{
    public static class AstNodeUtils
    {
        public static TokenKind GetTokenKind(this ParseNode node)
        {
            return node.Token;
        }

        public static ParseNode GetTokenData(this ParseNode node)
        {
            return node;
        }

        public static ParseNode Clone(this ParseNode node)
        {
            var result = new ParseNode(node.Rule)
                {
                    Content = node.Content,
                    Parent = node.Parent,
                    Token = node.Token
                };
            return result;
        }

        public static void RemoveFromParent(this ParseNode node)
        {
            var index = node.Parent.IndexOf(node);
            if (index == -1)
            {
                return;
            }
            var parent = node.Parent;
            if (parent.Children[index] != node)
            {
                throw new InvalidDataException("Wrong algorithm");
            }
            parent.RemoveAt(index);
            if (parent.NodeMatchesRule(RuleKind.Static))
                parent.RemoveFromParent();
        }


        public static void Insert(this ParseNode _this, int index, ParseNode node)
        {
            _this.Children.Insert(index, node);
            node.Parent = _this;
        }

        public static int IndexOf(this ParseNode _this, ParseNode astNode)
        {
            return _this.Children.IndexOf(astNode);
        }

        public static void RemoveAt(this ParseNode _this, int posInParent)
        {
            _this.Children.RemoveAt(posInParent);
        }

        public static bool IsTerminal(this ParseNode node)
        {
            return node.Rule == RuleKind.Terminal;
        }

        public static string GetTokenContent(this ParseNode node)
        {
            return node.Content;
        }

        public static int GeNextTokenKind(this List<ParseNode> nodes, TokenKind tokenKind, int startPos = 0)
        {
            for (var i = startPos + 1; i < nodes.Count; i++)
            {
                var astNode = nodes[i];
                var nodesKind = astNode.Token;
                if (nodesKind == tokenKind)
                    return i;
            }
            return 0;
        }

        public static ParseNode GetParentFunctionBlockNode(this ParseNode astNode)
        {
            while (true)
            {
                if (astNode == null)
                    return null;
                if (astNode.Parent == null)
                    return null;
                if (astNode.Parent.NodeMatchesRule(RuleKind.FunctionDeclaration))
                    return astNode;
                astNode = astNode.Parent;
            }
        }


        public static ParseNode GetRootNode(this ParseNode astNode)
        {
            while (true)
            {
                if (astNode.Parent == null)
                    return astNode;
                astNode = astNode.Parent;
            }
        }

        public static ParseNode NewNodeOfRule(this RuleKind rule)
        {
            var result = new ParseNode(rule);
            return result;
        }

        public static int PositionInParent(this ParseNode node)
        {
            if (node.Parent == null)
                return -1;
            var cleanupStates = node.Parent.Children.GetCleanStates();
            return cleanupStates.MappedNodes.IndexOf(node);
        }

        public static CleanupAstNodeStates GetCleanStates(this List<ParseNode> nodes)
        {
            return new CleanupAstNodeStates(nodes);
        }

        public static CleanupAstNodeStates GetCleanStates(ParseNode node)
        {
            return new CleanupAstNodeStates(node);
        }

        public static TokenKind GetTokenKind(this CleanupAstNodeStates cleanStates, int advance)
        {
            return cleanStates.MappedNodes[advance].GetTokenKind();
        }

        public static int GeNextTokenKind(this CleanupAstNodeStates cleanStates, TokenKind tokenKind, int startPos = 0)
        {
            return cleanStates.MappedNodes.GeNextTokenKind(tokenKind, startPos);
        }

        public static int GeNextTokenOfAny(this CleanupAstNodeStates cleanStates, IEnumerable<TokenKind> tokenKinds, int startPos = 0)
        {
            var resultList = tokenKinds
                .Select(tokenKind => cleanStates.GeNextTokenKind(tokenKind, startPos))
                .Where(it => it != 0).ToArray();
            return resultList.Length == 0 ? 0 : resultList.Min();
        }

        public static int GetNextMachingTokenKind(this CleanupAstNodeStates cleanStates,
                                                  TokenKind tokenKind, TokenKind openingKind,
                                                  int startPos = 0)
        {
            return cleanStates.MappedNodes.GetNextMachingTokenKind(tokenKind, openingKind, startPos);
        }


        public static bool NodeMatchesRule(this ParseNode node, RuleKind ruleKind)
        {
            return node.Rule == ruleKind;
        }

        public static bool NodeMatchesRule(this List<ParseNode> nodes, RuleKind ruleKind, int pos = 0)
        {
            return nodes[pos].NodeMatchesRule(ruleKind);
        }

        public static int GetNextOfRule(this List<ParseNode> nodes, RuleKind ruleKind, int startPos = 0)
        {
            for (var i = startPos + 1; i < nodes.Count; i++)
            {
                var astNode = nodes[i];
                if (astNode.Rule == ruleKind)
                    return i;
            }
            return 0;
        }

        public static int GetNextOfRule(this CleanupAstNodeStates cleanStates, RuleKind ruleKind, int startPos = 0)
        {
            return cleanStates.MappedNodes.GetNextOfRule(ruleKind);
        }


        public static List<int> GetAllOfToken(this CleanupAstNodeStates cleanStates, TokenKind tokenKind, int startPos = 0, int endPos =0)
        {
            var result = new List<int>();
            if (endPos == 0)
                endPos = cleanStates.Count;
            for (var i = startPos; i < endPos; i++)
            {
                var node = cleanStates[i];
                if (node.Token == tokenKind)
                    result.Add(i);
            }

            return result;
        }


        public static ParseNode AddTerminalToken(this ParseNode parentNode, TokenData tokenData)
        {
            var assignToken = tokenData.BuildTerminalNode();
            parentNode.Add(assignToken);
            return assignToken;
        }

        public static int GetNextMachingTokenKind(this CleanupAstNodeStates cleanStates, RuleKind ruleKind,
                                                  int startPos = 0)
        {
            return cleanStates.MappedNodes.GetNextOfRule(ruleKind);
        }

        public static int GetNextMachingTokenKind(this List<ParseNode> nodes,
                                                  TokenKind tokenKind, TokenKind openingKind,
                                                  int startPos = 0)
        {
            var itemsToClose = 1;
            for (var i = startPos + 1; i < nodes.Count; i++)
            {
                var astNode = nodes[i];
                var nodesKind = astNode.Token;
                if (nodesKind == tokenKind)
                    itemsToClose--;
                if (nodesKind == openingKind)
                    itemsToClose++;
                if (itemsToClose == 0)
                    return i;
            }
            return 0;
        }
    }
}