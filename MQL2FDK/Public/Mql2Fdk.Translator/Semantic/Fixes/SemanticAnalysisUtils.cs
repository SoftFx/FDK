using System;
using System.Collections.Generic;
using System.Linq;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Fixes.TypeTracking;

namespace Mql2Fdk.Translator.Semantic.Fixes
{
    static class SemanticAnalysisUtils
    {
        public static ParseNode[] ExtractFunctionNodes(this ParseNode node)
        {
            var functionNodes = node.Children.Where(child => child.Rule == RuleKind.FunctionDeclaration).ToArray();

            return functionNodes;
        }

        public static ParseNode GetParentFunctionDeclaration(this ParseNode node)
        {
            while (node != null && node.Rule != RuleKind.FunctionDeclaration)
            {
                node = node.Parent;
            }
            return node;
        }

        public static ParseNode GetFunctionBody(this ParseNode functionNode)
        {
            var functionBodyPosition = functionNode.Children.GetNextOfRule(RuleKind.BlockCode);
            var functionBody = functionNode.Children[functionBodyPosition];
            return functionBody;
        }

        public static ParseNode[] GetFunctionBodies(this ParseNode node)
        {
            var functionNodes = node.ExtractFunctionNodes();
            var result = functionNodes.Select(functionNode
                                              => functionNode.GetFunctionBody()).ToArray();
            return result;
        }


        public static void AddExplicitCastAtPosition(CleanupAstNodeStates states, string typeName, int positionCast)
        {
            var explicitCastList = new List<ParseNode>
                {
                    TokenKind.OpenParen.BuildTokenFromId(),
                    TokenKind.TypeName.BuildTokenFromId(typeName),
                    TokenKind.CloseParen.BuildTokenFromId()
                };

            states.InsertRange(positionCast, explicitCastList);
        }

        public static TypeData GetTypeOfName(this Dictionary<string, TypeData> variableTypes, string name)
        {
            TypeData result;
            if (variableTypes.TryGetValue(name, out result))
                return result;
            if (FunctionTypeData.HasFunction(name))
            {
                var functionData = FunctionTypeData.GetFunctionData(name);
                return functionData.ReturnType;
            }
            if (FunctionTypeData.HasGlobalVariable(name))
            {
                var globalVariableData = FunctionTypeData.GetGlobalVariable(name);
                return globalVariableData;
            }
            return new TypeData();
        }

        public static Dictionary<string, TypeData> GetDeclarations(this ParseNode functionBody)
        {
            var variableTypes = new Dictionary<string, TypeData>();
            var visitorDeclarations = new AstTokenVisitor(functionBody, TokenKind.TypeName)
                {
                    CallOnMatch = node => EvaluateDeclaration(node, variableTypes)
                };
            visitorDeclarations.Visit();
            ExtractParameterData(functionBody.Parent.States[2], variableTypes);
            return variableTypes;
        }

        static void ExtractParameterData(ParseNode parent, Dictionary<string, TypeData> variableTypes)
        {
            var states = parent.States;
            var openParam = 1;
            if (states.Count == 2)
                return;
            var pos = openParam;
            do
            {
                var typeData = new TypeData();
                var i = pos;
                for (; states[i].Token != TokenKind.Identifier; i++)
                {
                    typeData.AddTypeNode(states[i]);
                }
                variableTypes[states[i].Content] = typeData;
                pos = states.GeNextTokenKind(TokenKind.Comma, pos);
                if (pos == 0)
                    return;
                pos++;

            } while (true);
        }

        static void EvaluateDeclaration(ParseNode node, Dictionary<string, TypeData> variableTypes)
        {
            var states = node.Parent.States;

            var typeIndex = states.MappedNodes.IndexOf(node);
            if (typeIndex != 0)
                return;
            var variableNameIndex = states.GeNextTokenKind(TokenKind.Identifier);
            if (variableNameIndex == 0)
                return;

            var variableNode = states[variableNameIndex];
            var variableContent = variableNode.Content;

            var typeData = new TypeData();
            for (var index = typeIndex; index < variableNameIndex; index++)
            {
                var mappedNode = states[index];
                TypeData.ValidateToken(mappedNode);
                typeData.AddTypeNode(mappedNode);
            }
            variableTypes[variableContent] = typeData;
            do
            {
                var assignIndex = states.GeNextTokenKind(TokenKind.Assign, variableNameIndex);
                var commaIndex = states.GeNextTokenKind(TokenKind.Comma, variableNameIndex);
                var semiColonIndex = states.GeNextTokenKind(TokenKind.Comma, variableNameIndex);
                var separatorIndex = Math.Max(commaIndex, semiColonIndex);

                variableNameIndex = states.GeNextTokenKind(TokenKind.Identifier, variableNameIndex + 1);

                if (assignIndex < variableNameIndex)
                    continue;
                if (variableNameIndex == 0)
                    return;
                variableNode = states[variableNameIndex];
                variableContent = variableNode.Content;
                variableTypes[variableContent] = typeData;

            } while (true);
        }
    }
}