using Mql2Fdk.Translator.Common;

namespace Mql2Fdk.Translator.CodeGenerator.Common
{
    abstract class CodeGenForNode
    {
        public abstract bool Accept(ParseNode node);
        public abstract string DoWrite(ParseNode node);
    }
}