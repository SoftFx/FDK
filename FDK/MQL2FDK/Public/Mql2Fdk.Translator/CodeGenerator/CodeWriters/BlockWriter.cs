using System;
using Mql2Fdk.Translator.CodeGenerator.Common;
using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.CodeWriters
{
    class BlockWriter : CodeGenForT
    {
        public BlockWriter() : base(RuleKind.BlockCode)
        {
        }

        public override string DoWrite(ParseNode node)
        {
            return Environment.NewLine;
        }
    }
}