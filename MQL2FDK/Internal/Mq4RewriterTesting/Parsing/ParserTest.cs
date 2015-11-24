using Mq4RewriterTesting.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Parser;
using NUnit.Framework;

namespace Mq4RewriterTesting.Parsing
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void TestMethod1()
        {
            var lexer = new Mq4Lexer();
            var resultTokens = lexer.BuildTextTokens(ScriptUtils.ComplexCode);

            var parser = new Mq4Parser();
            var astTree = parser.Parse(resultTokens);

            Assert.IsTrue(astTree.Children.Count == 8);
        }

        [Test]
        public void TestParseExpression()
        {
            var lexer = new Mq4Lexer();
            var resultTokens = lexer.BuildTextTokens(ScriptUtils.CodeWithFunctionCall);

            var parser = new Mq4Parser();
            var astTree = parser.Parse(resultTokens);

            Assert.IsTrue(astTree.Children.Count == 1);
        }


        [Test]
        public void TestParseIf()
        {
            var lexer = new Mq4Lexer();
            var resultTokens = lexer.BuildTextTokens(ScriptUtils.CodeWithIfs);

            var parser = new Mq4Parser();
            var astTree = parser.Parse(resultTokens);

            Assert.IsTrue(astTree.Children.Count == 1);
        }

        [Test]
        public void TestParseGetMargin()
        {
            var lexer = new Mq4Lexer();
            var resultTokens = lexer.BuildTextTokens(ScriptUtils.GetMarginFunctionWithComplexComments);

            var parser = new Mq4Parser();
            var astTree = parser.Parse(resultTokens);

            Assert.IsTrue(astTree.Children.Count == 1);
        }
    }
}