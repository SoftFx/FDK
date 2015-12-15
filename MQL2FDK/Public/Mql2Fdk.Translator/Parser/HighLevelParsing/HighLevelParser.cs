using System.Collections.Generic;
using System.IO;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.HighLevelParsing
{
    class HighLevelParser
    {
        readonly ParseNode _root;

        public HighLevelParser(ParseNode root)
        {
            _root = root;
        }

        static void FoldBlocks(CleanupAstNodeStates cleanStates)
        {
            for (var pos = 0; pos < cleanStates.Count; pos++)
            {
                var currentNode = cleanStates[pos];
                var tokenKind = currentNode.GetTokenKind();
                switch (tokenKind)
                {
                    case TokenKind.OpenCurly:
                        cleanStates.ShiftBlock(pos);
                        break;
                }
            }
        }

        public void Interpret()
        {
            var cleanStates = new CleanupAstNodeStates(_root);
            FoldBlocks(cleanStates);
            InterpretGlobal(cleanStates);
        }

        void InterpretGlobal(CleanupAstNodeStates cleanStates)
        {
            for (var advancePosition = 0; advancePosition < cleanStates.Count; advancePosition++)
            {
                var currentNode = cleanStates[advancePosition];
                var tokenKind = currentNode.GetTokenKind();
                switch (tokenKind)
                {
                    case TokenKind.TypeName:
                        var isVariableDeclaration = CheckIfTypeDefinesVariable(advancePosition,
                                                                               cleanStates.MappedNodes);
                        if (isVariableDeclaration)
                            cleanStates.ShiftVariableDeclaration(advancePosition);
                        else
                        {
                            cleanStates.ShiftFunctionDeclaration(advancePosition);
                            var blockStates = cleanStates[advancePosition].States[3].States;
                            InterpretBlock(blockStates);
                        }

                        break;
                    case TokenKind.SharpDefine:
                        cleanStates.ShiftSharpDefineDeclaration(advancePosition);
                        break;

                    case TokenKind.SharpImport:
                        cleanStates.ShiftSharpImports(advancePosition);
                        break;
                    case TokenKind.SharpProperty:
                        break;
                    case TokenKind.Static:
                        ShiftStaticDefinition(cleanStates, advancePosition);
                        break;
                    case TokenKind.Extern:
                        ShiftExternDefinition(cleanStates, advancePosition);

                        break;

                    case TokenKind.Input:
                        cleanStates.ShiftInputs(advancePosition);
                        break;
                    default:
                        throw new InvalidDataException("Input type not handled");
                }
            }
        }

        void InterpretBlock(CleanupAstNodeStates cleanStates)
        {
            if (cleanStates[0].Token != TokenKind.OpenCurly)
                return;
            for (var advancePosition = 1; advancePosition < cleanStates.Count - 1; advancePosition++)
            {
                var currentNode = cleanStates.MappedNodes[advancePosition];
                var tokenKind = currentNode.GetTokenKind();
                switch (tokenKind)
                {
                    case TokenKind.TypeName:
                        cleanStates.ShiftVariableDeclaration(advancePosition);
                        break;

                    case TokenKind.Identifier:
                        cleanStates.ShiftInstructionDeclaration(advancePosition);
                        break;
                    case TokenKind.SharpDefine:
                        break;

                    case TokenKind.SharpImport:
                        cleanStates.ShiftSharpImports(advancePosition);
                        break;
                    case TokenKind.SharpProperty:
                    case TokenKind.CloseCurly:
                        break;
                    case TokenKind.None:
                        switch (currentNode.Rule)
                        {
                            case RuleKind.BlockCode:
                                var blockStates = currentNode.States;
                                InterpretBlock(blockStates);
                                break;
                        }
                        break;

                    case TokenKind.Break:
                        cleanStates.ShiftBreak(advancePosition);
                        break;
                    case TokenKind.Continue:
                        cleanStates.ShiftContinue(advancePosition);
                        break;

                    case TokenKind.If:
                    case TokenKind.For:
                    case TokenKind.While:
                        InterpretIf(cleanStates, advancePosition);
                        break;
                    case TokenKind.Switch:
                        InterpretIf(cleanStates, advancePosition);
                        break;
                    case TokenKind.Default:
                        InterpretCase(cleanStates, advancePosition);
                        break;
                    case TokenKind.Case:
                        InterpretCase(cleanStates, advancePosition);
                        break;
                    case TokenKind.Else:
                        InterpretElseWhile(cleanStates, advancePosition);
                        break;
                    case TokenKind.Static:
                        ShiftStaticDefinition(cleanStates, advancePosition);
                        break;
                    case TokenKind.Extern:
                        ShiftExternDefinition(cleanStates, advancePosition);
                        break;
                    case TokenKind.Return:
                        ShiftReturn(cleanStates, advancePosition);
                        break;
                    default:
                        throw new InvalidDataException("Input type not handled");
                }
            }
        }

        void InterpretIf(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var resultNode = cleanStates.ShiftByNodeType(advancePosition).States;
            InterpretBlock(resultNode[2].States);
        }

        void InterpretElseWhile(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var resultNode = cleanStates.ShiftByNodeType(advancePosition).States;
            InterpretBlock(resultNode[1].States);
        }

        void InterpretCase(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            cleanStates.ShiftByNodeType(advancePosition);
        }

        public static bool CheckIfTypeDefinesVariable(int advancePosition, List<ParseNode> cleanStates)
        {
            for (var i = advancePosition + 2; i < cleanStates.Count; i++)
            {
                var node = cleanStates[i];
                switch (node.Token)
                {
                    case TokenKind.Assign:
                    case TokenKind.OpenSquared:
                    case TokenKind.SemiColon:
                        return true;
                    case TokenKind.OpenParen:
                        return false;
                }
            }
            return false;
        }

        static void ShiftExternDefinition(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            cleanStates.ShiftVariableDeclaration(advancePosition + 1);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, advancePosition + 1, RuleKind.Extern);
        }

        static void ShiftStaticDefinition(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            cleanStates.ShiftVariableDeclaration(advancePosition + 1);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, advancePosition + 1, RuleKind.Static);
        }

        static void ShiftReturn(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.MappedNodes.GeNextTokenKind(TokenKind.SemiColon, advancePosition);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon, RuleKind.Return);
        }
    }
}