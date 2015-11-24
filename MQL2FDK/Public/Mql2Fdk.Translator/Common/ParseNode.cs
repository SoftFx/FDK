using System.Collections.Generic;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Common
{
    public class ParseNode
    {
        public ParseNode(TokenKind token, string tokenContents)
        {
            Token = token;
            Content = tokenContents;
            Rule = RuleKind.Terminal;
        }

        public TokenKind Token { get; set; }
        public RuleKind Rule { get; set; }
        public string Content { get; set; }
        public List<ParseNode> Children { get; private set; }

        public int Count
        {
            get
            {
                if (Children == null)
                    return 0;
                return Children.Count;
            }
        }

        public CleanupAstNodeStates States
        {
            get { return AstNodeUtils.GetCleanStates(this); }
        }

        public override string ToString()
        {
            return Rule == RuleKind.Terminal
                       ? string.Format("T:{1} : '{0}'", Content, Token)
                       : string.Format("R:{0} : '{1}'", Rule, States);
        }

        public ParseNode Parent;

        public ParseNode(RuleKind ruleKind)
        {
            Rule = ruleKind;
            Children = new List<ParseNode>();
        }

        public void Add(ParseNode parseNode)
        {
            if (Children == null)
                Children = new List<ParseNode>();
            Children.Add(parseNode);
            parseNode.Parent = this;
        }

        public void AddRange(IEnumerable<ParseNode> parseNode)
        {
            if (Children == null)
                Children = new List<ParseNode>();
            Children.AddRange(parseNode);
            foreach (var child in Children)
            {
                child.Parent = this;
            }
        }
    }
}