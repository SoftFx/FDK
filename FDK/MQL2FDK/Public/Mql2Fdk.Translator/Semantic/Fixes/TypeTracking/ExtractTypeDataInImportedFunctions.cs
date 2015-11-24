using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class ExtractTypeDataInImportedFunctions : SemanticFixForRule
    {
        public ExtractTypeDataInImportedFunctions()
            : base(RuleKind.ImportFunction)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var cleanStates = node.States;
            var functionName = cleanStates[1].GetTokenData().Content;
            var openParen = cleanStates.GeNextTokenKind(TokenKind.OpenParen);
            var closeParen = cleanStates.GeNextTokenKind(TokenKind.CloseParen);
            if (openParen + 1 == closeParen)
            {
                FunctionTypeData.DefineFunction(functionName, 0).ReturnType.AddTypeNode(cleanStates[0]);
                return;
            }
            var countCommas = cleanStates.MappedNodes.Count(paramNode => paramNode.GetTokenKind() == TokenKind.Comma);
            var function = FunctionTypeData.DefineFunction(functionName, countCommas + 1);
            function.ReturnType.AddTypeNode(cleanStates[0]);
            
            var indexParam = 0;
            var foundAssign = false;
            for (var index = 3; index < cleanStates.MappedNodes.Count; index++)
            {
                var mappedNode = cleanStates.MappedNodes[index];
                var tokenKind = mappedNode.GetTokenKind();
                switch (tokenKind)
                {
                    case TokenKind.CloseParen:
                        return;
                    case TokenKind.Comma:
                        foundAssign = false;
                        indexParam++;
                        continue;
                    case TokenKind.Assign:
                        foundAssign = true;
                        break;
                    case TokenKind.Identifier:
                        continue;
                }
                if(!foundAssign)
                    function[indexParam].AddTypeNode(mappedNode);
            }
        }
    }
}