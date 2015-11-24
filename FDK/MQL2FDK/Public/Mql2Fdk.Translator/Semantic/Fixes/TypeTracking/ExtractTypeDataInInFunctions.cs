using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    class ExtractTypeDataInInFunctions : SemanticFixForRule
    {
        public ExtractTypeDataInInFunctions()
            : base(RuleKind.FunctionDeclaration)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var cleanStates = node.States;
            var functionName = cleanStates[1].GetTokenData().Content;
            var paramsStates = cleanStates[2].States;
            if (paramsStates.MappedNodes.Count == 2)
            {
                FunctionTypeData.DefineFunction(functionName, 0).ReturnType.AddTypeNode(cleanStates[0]);
                return;
            }

            var countCommas = paramsStates.MappedNodes.Count(paramNode => paramNode.GetTokenKind() == TokenKind.Comma);
            var function = FunctionTypeData.DefineFunction(functionName, countCommas + 1);
            var indexParam = 0;
            var nameFound = false;
            foreach (var mappedNode in paramsStates.MappedNodes)
            {
                var tokenKind = mappedNode.GetTokenKind();
                if (nameFound && tokenKind != TokenKind.Comma)
                    continue;
                nameFound = false;
                switch (tokenKind)
                {
                    case TokenKind.CloseParen:
                        return;
                    case TokenKind.Comma:
                        indexParam++;
                        continue;
                    case TokenKind.Identifier:
                        nameFound = true;
                        continue;
                    case TokenKind.OpenParen:
                        continue;
                }
                function[indexParam].AddTypeNode(mappedNode);
            }
        }
    }
}