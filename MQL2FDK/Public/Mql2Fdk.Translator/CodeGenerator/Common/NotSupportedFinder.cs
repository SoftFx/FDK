using System;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    class NotSupportedFinder : CodeGenForNode
    {
        public override bool Accept(ParseNode node)
        {
            return node.Rule != RuleKind.Terminal
                   || node.Token != TokenKind.None;
        }

        public override string DoWrite(ParseNode node)
        {
            throw new NotImplementedException(
                string.Format("The AST node: {0} has no specific code generator", node));
        }
    }
}