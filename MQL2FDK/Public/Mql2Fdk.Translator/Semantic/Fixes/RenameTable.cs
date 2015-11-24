using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    class RenameTable
    {
        readonly string _functionName;
        public Dictionary<string, string> DeclarationName { get; private set; }

        public RenameTable(string functionName)
        {
            _functionName = functionName;
            DeclarationName = new Dictionary<string, string>();
        }

        public void ScanNames(ParseNode staticDeclaration)
        {
            var staticStates = staticDeclaration.States.MappedNodes;
            var declarationNode = staticStates[staticStates.Count - 1];
            var declarationStates = declarationNode.States;
            for (var index = 0; index < declarationStates.Count; index++)
            {
                var nameState = declarationStates[index];
                if (nameState.GetTokenKind() != TokenKind.Identifier)
                    continue;
                var nextState = declarationStates[index + 1];
                if (nextState.GetTokenKind() != TokenKind.Assign)
                    continue;
                var tokenName = nameState.GetTokenData();
                DeclarationName[tokenName.Content] = string.Format("{0}_{1}", _functionName, tokenName.Content);
            }
        }

        public void RenameInTree(ParseNode parent)
        {
            var astVisitor = new AstTokenVisitor(parent, TokenKind.Identifier)
            {
                CallOnMatch = OnIdentifier
            };
            astVisitor.Visit();
        }

        void OnIdentifier(ParseNode node)
        {
            var tokenData = node.GetTokenData();
            var content = tokenData.Content;
            string newContent;
            if (!DeclarationName.TryGetValue(content, out newContent))
                return;
            tokenData.Content = newContent;
        }
    }
}