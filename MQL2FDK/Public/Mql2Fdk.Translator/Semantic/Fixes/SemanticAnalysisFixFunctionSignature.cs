using System.Collections.Generic;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    /// <summary>
    /// Fixes code like 
    /// 'int & a' 
    /// to become: 
    /// 'ref int a'
    /// </summary>
    class SemanticAnalysisFixFunctionSignature : SemanticFixForRule
    {
        public SemanticAnalysisFixFunctionSignature() : base(RuleKind.FunctionSignature)
        {
        }

        protected override void FixRuleProblem(ParseNode node)
        {
            var states = node.States;
            // functions with no parameters don't need to be fixed
            if (states.Count == 2)
                return;
            var declarationStarts = new List<int>();
            var declarationEnds = new List<int>();
            declarationStarts.Add(1);
            for (var i = 2; i < states.MappedNodes.Count - 1; i++)
            {
                var token = states.MappedNodes[i].GetTokenKind();
                if (token != TokenKind.Comma) continue;
                declarationStarts.Add(i + 1);
                declarationEnds.Add(i - 1);
            }
            declarationEnds.Add(states.MappedNodes.Count - 2);

            for (var i = declarationStarts.Count - 1; i >= 0; i--)
            {
                ClearRange(states, declarationStarts[i], declarationEnds[i]);
            }
        }

        static void ClearRange(CleanupAstNodeStates states, int declarationStart, int declarationEnd)
        {
            if (declarationEnd == declarationStart + 1)
                return;
            var tokenData = states.MappedNodes[declarationStart + 1].GetTokenData();
            if (tokenData.Token == TokenKind.Operator && tokenData.Content == "&")
            {
                FixAmpersandAsRef(states, declarationStart);
            }
        }

        static void FixAmpersandAsRef(CleanupAstNodeStates states, int declarationStart)
        {
            var refToken = TokenKind.Ref.BuildTokenFromId();
            var spaceToken = TokenKind.Space.BuildTokenFromId(" ");
            states.RemoveAt(declarationStart + 1);

            states.Insert(declarationStart, spaceToken);
            states.Insert(declarationStart, refToken);
            states.Remap();
        }
    }
}