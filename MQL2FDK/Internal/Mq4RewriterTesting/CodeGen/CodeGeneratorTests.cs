using Mq4RewriterTesting.Common;
using Mql2Fdk.Translator.CodeGenerator;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser;
using Mql2Fdk.Translator.Semantic;
using NUnit.Framework;

namespace Mq4RewriterTesting.CodeGen
{
    [TestFixture]
    internal class CodeGeneratorTests
    {
        [Test]
        public static void GenerateCode()
        {
            var lexer = new Mq4Lexer();
            var resultTokens = lexer.BuildTextTokens(ScriptUtils.ComplexCode);

            var parser = new Mq4Parser();
            var astTree = parser.Parse(resultTokens);
            var semantic = new SemanticAnalysis(astTree);
            semantic.Perform();

            var codeGenerator = new CsCodeGenerator();
            codeGenerator.GenerateCodeForNode(astTree.Children[0]);
        }
    }
}