using System;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Parser.Comon
{
    class AstTreeVisitor
    {
        readonly ParseNode _node;
        public Action<ParseNode> CallOnMatch { get; set; }

        public AstTreeVisitor(ParseNode node)
        {
            _node = node;
        }

        protected virtual void VisitAstNode(ParseNode parentNode)
        {
            for (var index = parentNode.Count - 1; index >= 0; index--)
            {
                var node = parentNode.Children[index];
                VisitAstNode(node);
            }
        }

        protected void VisitMatch(ParseNode parentNode)
        {
            if (CallOnMatch != null)
                CallOnMatch(parentNode);
        }

        public void Visit()
        {
            VisitAstNode(_node);
        }
    }
}