using System;
using System.IO;
using System.Text;
using Mq4RewriterTesting.Common;
using Mql2Fdk.Translator.Lexer;
using NUnit.Framework;

namespace Mq4RewriterTesting.Lexer
{
    [TestFixture]
    public class LexerSimpleTests
    {
        public const string FileToScan = @"..\..\..\Extras\Scripts\SimpleTicks.mq4";

        [Test]
        public void TestMethod1()
        {
            var lexer = new Mq4Lexer();
            var resultTokens = lexer.BuildTextTokens("SimpleTicks.mq4".CodeFromExtras(Encoding.Default));
        }

        [Test]
        public void ProblemFile()
        {
            var fileName = @"C:\ProgramData\MetaTrader ECN - FXOpen\experts\MySmartAdviser.mq4";

            var lexer = new Mq4Lexer();
            lexer.BuildTextTokens(fileName.CodeFromFile());
        }

        [Test]
        public void ScanAllTokens()
        {
            var pathToSearch = @"C:\ProgramData\MetaTrader ECN - FXOpen\";
            var lexer = new Mq4Lexer();
            var allFiles = Directory.GetFiles(pathToSearch, "*.mqh", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                try
                {
                    lexer.BuildFileTokens(file);
                }
                catch (Exception)
                {
                    Assert.Fail("Files could not be tokenized");
                }
            }
        }

        [Test]
        public void ScanAllTokensInLibs()
        {
            var pathToSearch = @"C:\ProgramData\MetaTrader ECN - FXOpen\";
            var lexer = new Mq4Lexer();
            var allFiles = Directory.GetFiles(pathToSearch, "*.mq4", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                try
                {
                    lexer.BuildFileTokens(file);
                }
                catch (Exception)
                {
                    Assert.Fail("Files could not be tokenized");
                }
            }
        }
    }
}