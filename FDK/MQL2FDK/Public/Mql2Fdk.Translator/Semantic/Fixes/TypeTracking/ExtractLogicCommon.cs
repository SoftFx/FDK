using System;
using System.Linq;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    static class ExtractLogicCommon
    {
        public static void ExtractGlobalVariables(ParseNode parentNode, ParseNode[] typeNodes)
        {
            if (typeNodes.Length == 0)
                throw new ArgumentException("typeNodes");
            var cleanStates = parentNode.States;

            var identifiers = cleanStates.Where(idNode => idNode.Token == TokenKind.Identifier).ToArray();
            identifiers.Each(id =>
                {
                    var globalVariable = FunctionTypeData.DefineGlobalVariable(id.Content);
                    typeNodes.Each(globalVariable.AddTypeNode);
                });
        }
    }
}