using System;
using System.Collections.Generic;
using System.Diagnostics;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Semantic.Common;
using Mql2Fdk.Translator.Semantic.Fixes;
using Mql2Fdk.Translator.Semantic.Fixes.ReferenceParameterTracking;
using Mql2Fdk.Translator.Semantic.Fixes.TypeTracking;
using Mql2Fdk.Translator.Semantic.Fixes.TypeTracking.Parameters;
using Mql2Fdk.Translator.Semantic.Fixes.VariableDeclarations;

namespace Mql2Fdk.Translator.Semantic
{
    public class SemanticAnalysis
    {
        readonly ParseNode _node;
        readonly List<SemanticAnalysisFixBase> _semanticFixes = new List<SemanticAnalysisFixBase>();

        public SemanticAnalysis(ParseNode node)
        {
            _node = node;
        }

        public void Perform()
        {
            AddFix<SemanticAnalysisFixFunctionSignature>();


            AddFix<SemanticAnalysisFixDefines>();

            AddFix<FixCloseOpenSquareAsComma>();
            AddFix<FixProperties>();

            AddFix<SemanticAnalysisFixFunctionSignature>();
            AddFix<FixInvalidIdentifiers>();
            AddFix<FixIdentifierNames>();
            AddFix<SemanticAnalysisFixFunctionSignature>();

            AddFix<SemanticAnalysisFixFunctionSignature>();
            AddFix<SemanticStaticVariableDeclaration>();

            AddFix<SemanticAnalysisFixConflictFunctionVariableNames>();



            AddFix<FixVariableDeclarationsInInnerBlocks>();
            AddFix<FixMultipleVarsWithArray>();

            AddFix<FixDeclaredArray>();


            AddFix<FixArrayInFunctionParameters>();
            AddFix<FixUninitializedVariableDeclarationsInInnerBlocks>();

            AddFix<FixDeclaredArrayArgument>();
            AddFix<FixDeclaredRefArgument>();

            AddFix<FunctionExtractingReferenceDataInFunctions>();
            AddFix<FunctionExtractingReferenceInImportedFunctions>();
            AddFix<FunctionExtractingRefsInSoftFxApis>();
            AddFix<FixFunctionReferenceParamCall>();

            AddFix<ExtractTypeDataInImportedFunctions>();
            AddFix<ExtractTypeDataInInFunctions>();
            AddFix<ExtractTypesInSoftFxApis>();

            FunctionTypeData.Clear();

            AddFix<ExtractTypeDataInInDefinedVariables>();
            AddFix<ExtractTypeDataInGlobalVariables>();

            AddFix<FixIncompatibleAssignmentsInGlobalSpace>();
            AddFix<FixIncompatibleAssignments>();
            AddFix<FixIncompatibleParameters>();
            AddFix<FixIncompatibleReturns>();

            AddFix<FixAssignmentWithConstants>();
            AddFix<FixParametersWithConstants>();
            AddFix<FixParametersByCasting>();

            AddFix<FixEmptyReturnForNonVoidMethod>();
            AddFix<FixNoReturnEndingNonVoid>();
            AddFix<FixReturnWithParamsForVoidFunctions>();

            foreach (var fix in _semanticFixes)
            {
                try
                {
                    fix.Perform(_node);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fix: '{0}' was not applied successfully", fix.GetType().Name);
                    Debug.WriteLine("Exception: '{0}' ", ex);

                }
            }
        }

        void AddFix<T>() where T : SemanticAnalysisFixBase, new()
        {
            _semanticFixes.Add(new T());
        }
    }
}