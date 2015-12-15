using System.Collections.Generic;
using System.IO;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser.ParseRules;

namespace Mql2Fdk.Translator.Parser.Comon
{
    static class CleanAstNodesShifts
    {
        public static ParseNode BuildTerminalNode(this TokenData resultToken)
        {
            var newNode = new ParseNode(resultToken.Kind, resultToken.Content);
            return newNode;
        }

        public static ParseNode BuildTerminalNode(this ParseNode resultToken)
        {
            var newNode = new ParseNode(resultToken.Token, resultToken.Content);
            return newNode;
        }

        public static ParseNode BuildTokenFromId(this TokenKind resultToken, string content = "")
        {
            var buildTokenFromId = Mq4ReservedWords.Instance.BuildTokenFromId(resultToken, content);
            return buildTokenFromId;
        }

        public static void ShiftAssignment(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon, RuleKind.Assignment);
        }

        public static void ShiftCall(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon,
                                                    RuleKind.InstructionCode);
        }


        public static CleanupAstNodeStates ShiftBlock(this CleanupAstNodeStates cleanStates, int leftCurly)
        {
            var firstNode = cleanStates[leftCurly];
            if (firstNode.Token != TokenKind.OpenCurly)
                throw new InvalidDataException("Block does not start with open curly");

            var rightCurly = cleanStates.GetNextMachingTokenKind(TokenKind.CloseCurly,
                                                                 TokenKind.OpenCurly,
                                                                 leftCurly);
            var lastNode = cleanStates[rightCurly];
            if (lastNode.GetTokenKind() != TokenKind.CloseCurly)
                throw new InvalidDataException("Block does not end with close curly");
            bool foundShift;
            do
            {
                foundShift = false;
                var nextLeftCurly = cleanStates.GeNextTokenKind(TokenKind.OpenCurly, leftCurly);
                if (nextLeftCurly == 0 || nextLeftCurly >= rightCurly) continue;
                ShiftBlock(cleanStates, nextLeftCurly);
                rightCurly = cleanStates.GetNextMachingTokenKind(TokenKind.CloseCurly,
                                                                 TokenKind.OpenCurly,
                                                                 leftCurly);
                foundShift = true;
            } while (foundShift);

            var parentBlock = cleanStates.ReduceRangeOfTokensAsParent(leftCurly, rightCurly, RuleKind.BlockCode).States;

            return parentBlock;
        }

        public static void ShiftIf(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var leftParent = cleanStates.GeNextTokenKind(TokenKind.OpenParen, advancePosition);
            var rightParen = cleanStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen,
                                                                 leftParent);

            var nextNode = cleanStates.MappedNodes[rightParen + 1];
            var blockAfterIf = nextNode.NodeMatchesRule(RuleKind.BlockCode);

            if (!blockAfterIf && nextNode.IsTerminal())
                nextNode = cleanStates.ShiftByNodeType(rightParen + 1);

            cleanStates.ReduceRangeOfTokensAsParent(leftParent, rightParen,
                                                    RuleKind.ConditionalCode);
            var nextNodePos = cleanStates.MappedNodes.IndexOf(nextNode);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, nextNodePos, RuleKind.If);
        }

        public static void ShiftElse(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var nextNode = cleanStates.MappedNodes[advancePosition + 1];
            var blockAfterIf = nextNode.NodeMatchesRule(RuleKind.BlockCode);
            if (!blockAfterIf && nextNode.IsTerminal())
                nextNode = cleanStates.ShiftByNodeType(advancePosition + 1);
            var nextNodePos = cleanStates.MappedNodes.IndexOf(nextNode);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, nextNodePos, RuleKind.Else);
        }

        public static void ShiftSwitch(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var leftParent = cleanStates.GeNextTokenKind(TokenKind.OpenParen, advancePosition);
            var rightParen = cleanStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen,
                                                                 leftParent);

            var nextNode = cleanStates.MappedNodes[rightParen + 1];
            var blockAfterIf = nextNode.NodeMatchesRule(RuleKind.BlockCode);

            if (!blockAfterIf && nextNode.IsTerminal())
                nextNode = cleanStates.ShiftByNodeType(rightParen + 1);

            cleanStates.ReduceRangeOfTokensAsParent(leftParent, rightParen,
                                                    RuleKind.ConditionalCode);

            var nextNodePos = cleanStates.MappedNodes.IndexOf(nextNode);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, nextNodePos, RuleKind.Switch);
        }

        /// <summary>
        /// TODO: Handle do {...} while (...); block
        /// </summary>
        /// <param name="cleanStates"></param>
        /// <param name="advancePosition"></param>
        public static void ShiftWhile(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var leftParent = cleanStates.GeNextTokenKind(TokenKind.OpenParen, advancePosition);
            var rightParen = cleanStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen,
                                                                 leftParent);


            var nextNode = cleanStates.MappedNodes[rightParen + 1];
            var blockAfterIf = nextNode.NodeMatchesRule(RuleKind.BlockCode);

            if (!blockAfterIf && nextNode.IsTerminal())
            {
                nextNode = cleanStates.ShiftByNodeType(rightParen + 1);
            }
            cleanStates.ReduceRangeOfTokensAsParent(leftParent, rightParen,
                                                    RuleKind.ConditionalCode);

            var nextNodePos = cleanStates.IndexOf(nextNode);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, nextNodePos, RuleKind.While);
        }


        public static void ShiftFor(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var leftParent = cleanStates.GeNextTokenKind(TokenKind.OpenParen, advancePosition);
            var rightParen = cleanStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen,
                                                                 leftParent);
            var nextNode = cleanStates[rightParen + 1];
            var blockAfterIf = nextNode.NodeMatchesRule(RuleKind.BlockCode);
            if (!blockAfterIf && nextNode.IsTerminal())
                nextNode = cleanStates.ShiftByNodeType(rightParen + 1);
            cleanStates.ReduceRangeOfTokensAsParent(leftParent, rightParen,
                                                    RuleKind.ConditionalCode);

            var nextNodePos = cleanStates.MappedNodes.IndexOf(nextNode);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, nextNodePos, RuleKind.For);
        }

        public static ParseNode ShiftByNodeType(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var shiftByNodeType = cleanStates[advancePosition];
            var nodeKind = shiftByNodeType.GetTokenKind();
            switch (nodeKind)
            {
                case TokenKind.If:
                    ShiftIf(cleanStates, advancePosition);
                    break;
                case TokenKind.For:
                    ShiftFor(cleanStates, advancePosition);
                    break;
                case TokenKind.Switch:
                    ShiftSwitch(cleanStates, advancePosition);
                    break;
                case TokenKind.While:
                    ShiftWhile(cleanStates, advancePosition);
                    break;
                case TokenKind.Else:
                    ShiftElse(cleanStates, advancePosition);
                    break;

                case TokenKind.TypeName:
                    ShiftInstructionDeclaration(cleanStates, advancePosition);
                    break;
                case TokenKind.OpenCurly:
                    ShiftBlock(cleanStates, advancePosition);
                    break;
                case TokenKind.Identifier:
                    ShiftInstructionDeclaration(cleanStates, advancePosition);
                    break;
                case TokenKind.Continue:
                    ShiftContinue(cleanStates, advancePosition);
                    break;

                case TokenKind.Default:
                case TokenKind.Case:
                    ShiftCase(cleanStates, advancePosition);
                    break;
                case TokenKind.Break:
                    ShiftBreak(cleanStates, advancePosition);
                    break;
                case TokenKind.None:
                    break;
                case TokenKind.Return:
                    ShiftReturn(cleanStates, advancePosition);
                    break;
                default:
                    throw new InvalidDataException();
            }
            shiftByNodeType = cleanStates[advancePosition];
            return shiftByNodeType;
        }

        static void ShiftCase(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var colon = cleanStates.GeNextTokenKind(TokenKind.Colon, advancePosition);
            var tokenAfterColon = cleanStates[colon + 1].Token;
            switch (tokenAfterColon)
            {
                case TokenKind.Continue:
                case TokenKind.Return:
                case TokenKind.Break:
                case TokenKind.Case:
                    cleanStates.ShiftByNodeType(colon + 1);
                    cleanStates.ReduceRangeOfTokensAsParent(colon + 1, colon + 1, RuleKind.CaseBlock);
                    cleanStates.ReduceRangeOfTokensAsParent(advancePosition, colon + 1, RuleKind.Case);
                    return;
            }
            var breakPos = cleanStates.GeNextTokenOfAny(new[]
                {
                    TokenKind.Break,
                    TokenKind.Return,
                    TokenKind.Continue,
                    TokenKind.Case,
                }, colon);
            if (breakPos == 0)
            {
                cleanStates.ReduceRangeOfTokensAsParent(colon + 1, cleanStates.Count - 2, RuleKind.CaseBlock);
                cleanStates.ReduceRangeOfTokensAsParent(advancePosition, colon + 1, RuleKind.Case);
                return;
            }
            var breakToken = cleanStates[breakPos].Token;
            switch (breakToken)
            {
                case TokenKind.Case:
                    cleanStates.ReduceRangeOfTokensAsParent(colon + 1, breakPos - 1, RuleKind.CaseBlock);
                    cleanStates.ReduceRangeOfTokensAsParent(advancePosition, colon + 1, RuleKind.Case);
                    return;
                case TokenKind.Return:
                    cleanStates.ShiftByNodeType(breakPos);
                    cleanStates.ReduceRangeOfTokensAsParent(colon + 1, breakPos, RuleKind.CaseBlock);
                    cleanStates.ReduceRangeOfTokensAsParent(advancePosition, colon + 1, RuleKind.Case);
                    return;
            }
            cleanStates.ReduceRangeOfTokensAsParent(colon + 1, breakPos + 1, RuleKind.CaseBlock);
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, colon + 1, RuleKind.Case);


        }

        public static void ShiftBreak(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);
            if (advancePosition + 1 != rightColon)
                throw new InvalidDataException("Input code looks like 'break' but is invalid parsed");
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon, RuleKind.Break);
        }

        public static void ShiftContinue(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);
            if (advancePosition + 1 != rightColon)
                throw new InvalidDataException("Input code looks like 'continue' but is invalid parsed");
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon, RuleKind.Break);
        }

        public static void ShiftReturn(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon, RuleKind.Return);
        }

        public static void ShiftVariableDeclaration(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var startNode = cleanStates[advancePosition];
            if(startNode.Token!=TokenKind.TypeName)
            {
                
            }
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon,
                                                    RuleKind.DeclareVariable);
        }

        public static void ShiftInstructionDeclaration(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);
            if (rightColon == 0)
                throw new InvalidDataException("Looks like an instruction but is not");
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon, RuleKind.InstructionCode);
        }


        public static void ShiftSharpImports(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var importPositions = new List<int> {advancePosition};
            var doContinue = true;
            const string importText = "#import";
            var pos = advancePosition;
            while (doContinue)
            {
                pos = cleanStates.GeNextTokenKind(TokenKind.SharpImport, pos + 1);
                if (pos == 0)
                    break;
                var context = cleanStates.MappedNodes[pos].GetTokenData();
                if (context.Token == TokenKind.SharpImport)
                    doContinue = (context.Content.Trim() != importText);
                importPositions.Add(pos);
                pos++;
            }
            var lastImport = importPositions[importPositions.Count - 1];
            cleanStates.RemoveAt(lastImport);
            for (var i = importPositions.Count - 2; i >= 0; i--)
            {
                pos = importPositions[i];
                var tokenData = cleanStates.MappedNodes[pos].GetTokenData().Content;
                var libraryToImport = tokenData.Remove(0, importText.Length).Trim();
                ReduceImportRange(pos, importPositions[i + 1] - 1, libraryToImport, cleanStates);
            }
            cleanStates.Remap();
        }

        public static void ShiftInputs(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var rightColon = cleanStates.GeNextTokenKind(TokenKind.SemiColon, advancePosition);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, rightColon, RuleKind.Input);
        }
        static void ReduceImportRange(int pos, int endImport, string libraryToImport,
                                              CleanupAstNodeStates cleanStates)
        {
            var importSection = cleanStates.ReduceRangeOfTokensAsParent(pos, endImport,
                                                                        RuleKind.ImportLibraryDeclaration);
            importSection.Content = libraryToImport;
            var parentImportNode = cleanStates.MappedNodes[pos];
            var importCleanStates = parentImportNode.States;
            var startFunctions = new List<int>();
            var endFunctions = new List<int>();
            FindImportFunctionRanges(importCleanStates, startFunctions, endFunctions);

            startFunctions.ReverseEachWithIndex((startPos, index) =>
                {
                    var endFunction = endFunctions[index];
                    importCleanStates.ReduceRangeOfTokensAsParent(startPos, endFunction,
                                                                  RuleKind.ImportFunction);
                });
        }

        static void FindImportFunctionRanges(CleanupAstNodeStates importCleanStates, List<int> startFunctions,
                                                     List<int> endFunctions)
        {
            var position = 0;
            do
            {
                var start = importCleanStates.GeNextTokenKind(TokenKind.TypeName, position);
                if (start == 0)
                    break;
                var endPos = importCleanStates.GeNextTokenKind(TokenKind.SemiColon, position + 1);
                startFunctions.Add(start);
                endFunctions.Add(endPos);
                position = endPos;
            } while (true);
        }


        public static void ShiftStaticVariableDeclaration(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            cleanStates.ShiftVariableDeclaration(advancePosition + 1);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, advancePosition + 1,
                                                    RuleKind.Static);
        }

        public static void ShiftFunctionDeclaration(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var leftParent = cleanStates.GeNextTokenKind(TokenKind.OpenParen, advancePosition);
            var rightParen = cleanStates.GetNextMachingTokenKind(TokenKind.CloseParen, TokenKind.OpenParen,
                                                                 leftParent);

            var isForwardFunction = cleanStates[rightParen + 1].Token == TokenKind.SemiColon;
            cleanStates.ReduceRangeOfTokensAsParent(leftParent, rightParen,
                                                    RuleKind.FunctionSignature);
            if (isForwardFunction)
            {
                cleanStates.ReduceRangeOfTokensAsParent(advancePosition, leftParent + 1,
                                                        RuleKind.FunctionForwardDeclaration);
                return;
            }
            var bodyPosition = cleanStates.GetNextOfRule(RuleKind.BlockCode, advancePosition);

            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, bodyPosition,
                                                    RuleKind.FunctionDeclaration);
        }

        public static void ShiftSharpDefineDeclaration(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            cleanStates.ReduceRangeOfTokensAsParent(advancePosition, advancePosition,
                                                    RuleKind.DefineDeclaration);

            var declaration = cleanStates.MappedNodes[advancePosition];
            DefineDeclaration.ParseData(declaration);
        }

        public static ParseNode ShiftSharpDefineProperty(this CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var result = cleanStates.ReduceRangeOfTokensAsParent(advancePosition, advancePosition,
                                                                 RuleKind.SharpProperty);

            var declaration = cleanStates.MappedNodes[advancePosition];
            SharpPropertyRule.ParseData(declaration);
            return result;
        }
    }
}