using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mq4RewriterTesting.Common;
using Mql2Fdk.Translator.CodeGenerator.Formatter;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Lexer.Preprocessor;
using Mql2Fdk.Translator.Translator;
using NUnit.Framework;

namespace Mq4RewriterTesting.CodeGen
{
    [TestFixture]
    public class TestVariousFiles
    {
        [Test]
        public void TestSimpleTicksMq4()
        {
            TestFileSemantic("SimpleTicks.mq4");
        }

        [Test]
        public void Test8020Mq4()
        {
            TestFileSemantic(@"Internet\8020.mq4");
        }

        [Test]
        public void Test6257()
        {
            TestFileSemantic(@"Internet\6257.mq4");
        }

        [Test]
        public void Test6141()
        {
            TestFileSemantic(@"Internet\6141.mq4");
        }

        [Test]
        public void Test6161()
        {
            TestFileSemantic(@"Internet\6161.mq4");
        }

        [Test]
        public void Test6243()
        {
            TestFileSemantic(@"Internet\6243.mq4");
        }

        [Test]
        public void Test7744()
        {
            TestFileSemantic(@"Internet\7744.mq4");
        }

        [Test]
        public void Test7814()
        {
            TestFileSemantic(@"Internet\7814.mq4");
        }


        [Test]
        public void Test8068Mq4()
        {
            TestFileSemantic(@"Internet\8068.mq4");
        }

        [Test]
        public void Test8342Mq4()
        {
            TestFileSemantic(@"Internet\8342.mq4");
        }

        [Test]
        public void Test8757Mq4()
        {
            TestFileSemantic(@"Internet\8757.mq4");
        }


        [Test]
        public void TestOneClickTradingLevel2()
        {
            TestFileSemantic("OneClickTradingLevel2.mq4");
        }

        [Test]
        public void TestCyberiaTrader()
        {
            TestFileSemantic("CyberiaTrader.mq4");
        }

        [Test]
        public void TestArtificialIntelligence()
        {
            TestFileSemantic("ArtificialIntelligence.mq4");
        }

        [Test]
        public void TestLuckyFast()
        {
            TestFileSemantic("LuckyFast.mq4");
        }

        [Test]
        public void TestMarketDepths()
        {
            TestFileSemantic("MarketDepths.mq4");
        }

        [Test]
        public void TestMarketDepthsSimplified()
        {
            TestFileSemantic("MarketDepthsSimplified.mq4");
        }


        public static List<string> GetInternetFiles()
        {
            var filesPath = Path.Combine(ScriptUtils.PathToScripts, "Internet");
            var files = Directory.GetFiles(filesPath, "*.mq4", SearchOption.TopDirectoryOnly);
            var combinedPaths = new List<string>();
            foreach (var file in files)
            {
                var info = new FileInfo(file);
                var combinedPath = Path.Combine("Internet", info.Name);
                combinedPaths.Add(combinedPath);
            }
            return combinedPaths;
        }

        public static void TestFileParsing(string fileToTest)
        {
            IncludePaths.BlackListIncludeFile("stdlib.mqh");
            IncludePaths.AddDirectoryInclude(@"C:\ProgramData\MetaTrader ECN - FXOpen\experts\include\");
            var translator = new Mq4Translator();
            var fileText = fileToTest.CodeFromExtras(Encoding.Default);
            var resultParse = translator.Parse(fileText, false);
            Assert.IsTrue(resultParse);
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
        }

        public static void TestFileSemantic(string fileToTest)
        {
            IncludePaths.BlackListIncludeFile("stdlib.mqh");
            IncludePaths.AddDirectoryInclude(@"C:\ProgramData\MetaTrader ECN - FXOpen\experts\include\");
            var translator = new Mq4Translator();
            var fileText = fileToTest.CodeFromExtras(Encoding.Default);
            var resultParse = translator.Parse(fileText);
            Assert.IsTrue(resultParse);
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
            var formattedCode = code.BeautifyCsFileSource();
            var fileErrors = code.CsFileErrors().Select(err =>
                                                        string.Format("Error ({0}:{1}): {2} ", err.Region.BeginLine,
                                                                      err.Region.BeginColumn, err.Message))
                .ToList();

            Assert.IsFalse(string.IsNullOrEmpty(code));
            Assert.IsTrue(fileErrors.Count == 0);
        }

        public static string GetFileResultScript(string fileToTest)
        {
            IncludePaths.BlackListIncludeFile("stdlib.mqh");
            IncludePaths.AddDirectoryInclude(@"C:\ProgramData\MetaTrader ECN - FXOpen\experts\include\");
            var translator = new Mq4Translator();
            var fileText = fileToTest.CodeFromExtras(Encoding.Default);
            var resultParse = translator.Parse(fileText);
            Assert.IsTrue(resultParse);
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
            var formattedCode = code.BeautifyCsFileSource();
            var fileErrors = code.CsFileErrors().Select(err =>
                                                        string.Format("Error ({0}:{1}): {2} ", err.Region.BeginLine,
                                                                      err.Region.BeginColumn, err.Message))
                .ToList();

            Assert.IsFalse(string.IsNullOrEmpty(code));
            Assert.IsTrue(fileErrors.Count == 0);
            return formattedCode;
        }

        public static void TestFileLexing(string fileToTest)
        {
            var translator = new Mq4Lexer();
            var fileText = fileToTest.CodeFromExtras(Encoding.Default);
            var resultParse = translator.BuildTextTokens(fileText);
            Assert.IsTrue(resultParse.Count != 0);
        }
    }
}