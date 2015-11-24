using System.IO;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Parser.Comon;

namespace Mql2Fdk.Translator.Parser.MultiPassParsing
{
    class FunctionBodyParser : MultiPassParserByRule
    {
        public FunctionBodyParser() : base(RuleKind.BlockCode)
        {
        }

        public override void OnVisitMatch(ParseNode node)
        {
            if (!node.Parent.NodeMatchesRule(RuleKind.FunctionDeclaration))
                return;
            var cleanStates = new CleanupAstNodeStates(node.Children);
            var advancePosition = 1;
            while (advancePosition < cleanStates.Count - 1)
            {
                var currentNode = cleanStates.MappedNodes[advancePosition];
                var tokenKind = currentNode.GetTokenKind();
                switch (tokenKind)
                {
                    case TokenKind.TypeName:
                        cleanStates.ShiftVariableDeclaration(advancePosition);
                        break;

                    case TokenKind.Static:
                        cleanStates.ShiftStaticVariableDeclaration(advancePosition);
                        break;
                    case TokenKind.Identifier:
                        var isAssignment = CheckIsAssignment(cleanStates, advancePosition);
                        if (isAssignment)
                        {
                            cleanStates.ShiftAssignment(advancePosition);
                        }
                        else
                        {
                            cleanStates.ShiftCall(advancePosition);
                        }
                        break;
                    case TokenKind.CloseCurly:
                        throw new InvalidDataException("Unmatched closed curly");
                    case TokenKind.None:
                        break;
                    default:
                        throw new InvalidDataException("Input type not handled");
                }
                advancePosition++;
            }
        }

        static bool CheckIsAssignment(CleanupAstNodeStates cleanStates, int advancePosition)
        {
            var colonPos = cleanStates.MappedNodes.GeNextTokenKind(TokenKind.SemiColon, advancePosition);

            var assign = cleanStates.MappedNodes.GeNextTokenKind(TokenKind.Assign, advancePosition);
            return (assign > 0) && (assign < colonPos);
        }
    }
}