using System;
using System.Linq;
using Mql2Fdk.Translator.Common;
using System.Text;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    class InputTokenKindCodeGen : CodeGenForT
    {
        public InputTokenKindCodeGen()
            : base(RuleKind.Input)
        {
            SkipChildrenNode = true;
        }

        public override string DoWrite(ParseNode node)
        {
            var inputTokens = node.Children.Where(child => child.Token != TokenKind.Space)
                .Skip(1)
                .ToArray();
            bool isEnum = inputTokens[0].Token == TokenKind.Identifier;
            var sb = new StringBuilder();
            sb.AppendFormat("[IndicatorInput] {0} {1} = ", inputTokens[0].Content, inputTokens[1].Content );
            if (isEnum)
            {
                sb.AppendFormat("{0}.{1}", inputTokens[0].Content, inputTokens[3].Content);
            }
            else
            {
                sb.Append(inputTokens[3].Content);
            }


            sb.AppendLine(";");

            return sb.ToString();
        }
    }
}