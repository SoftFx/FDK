using System;
using System.Reflection;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.Comon;
using Mql2Fdk.Translator.Semantic.Common;

namespace Mql2Fdk.Translator.Semantic.Fixes.TypeTracking
{
    public class ExtractTypesInSoftFxApis : SemanticAnalysisFixBase
    {
        public override void Perform(ParseNode node)
        {
            var typeToBeReflected = typeof(MqlAdapter);

            typeToBeReflected.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Each(ExtractMethodTypes);

            typeToBeReflected.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                .Each(ExtractMethodTypes);
        }

        static void ExtractMethodTypes(MethodInfo method)
        {
            var functionName = method.Name;
            var parameters = method.GetParameters();
            var definition = FunctionTypeData.DefineFunction(functionName, parameters.Length);
            SetTypeDataFromType(definition.ReturnType, method.ReturnType);

            parameters.EachWithIndex(
                (param, id) =>
                    {
                        var paramType = param.ParameterType;

                        SetTypeDataFromType(definition[id], paramType);
                    });
        }

        static void SetTypeDataFromType(TypeData typeData, Type paramType)
        {
            switch (paramType.Name)
            {
                case "Int32":
                    AddNodeType(typeData, "int");
                    break;
                case "String":
                    AddNodeType(typeData, "string");
                    break;
                case "Double":
                    AddNodeType(typeData, "double");
                    break;
                case "Boolean":
                    AddNodeType(typeData, TypeNames.Bool);
                    break;
                case "Void":
                    AddNodeType(typeData, TypeNames.Void);
                    break;
                case "datetime":
                    AddNodeType(typeData, TypeNames.DateTime);
                    break;
                case "color":
                    AddNodeType(typeData, TypeNames.Color);
                    break;
                default:
                    AddNodeType(typeData, TypeNames.Unknown);
                    break;
            }
        }

        static void AddNodeType(TypeData typeData, string content)
        {
            typeData.AddTypeNode(TokenKind.TypeName.BuildTokenFromId(content));
        }
    }
}