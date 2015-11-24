using System.Linq;
using Mq4RewriterTesting.Common;
using Mql2Fdk.Translator.CodeGenerator.Formatter;
using Mql2Fdk.Translator.Translator;
using NUnit.Framework;

namespace Mq4RewriterTesting.CodeGen
{
    [TestFixture]
    public class CodeGenTests
    {
        [Test]
        public void TestMethod1()
        {
            var translator = new Mq4Translator();
            translator.Parse(ScriptUtils.ComplexCode);
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
        }

        [Test]
        public void TestArrayCodeGen()
        {
            var translator = new Mq4Translator();
            const string fileText = ScriptUtils.SimpleArray;
            var resultParse = translator.Parse(fileText);
            Assert.IsTrue(resultParse);
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
            var formattedCode = code.BeautifyCsFileSource();
            var fileErrors = code.CsFileErrors().Select(err =>
                                                        string.Format("{0}: {1}", err.Region, err.Message)).ToArray();

            Assert.IsFalse(string.IsNullOrEmpty(code));
            Assert.IsTrue(fileErrors.Length == 0);
        }

        [Test]
        public void FormattingTest()
        {
            var code = @"class MyClass { int OrderScan(  ){
  int Types = new int[2];
}
 }";
            var formattedCode = code.BeautifyCsFileSource();

            Assert.IsFalse(string.IsNullOrEmpty(formattedCode));


            var fileErrors = code.CsFileErrors().Select(err =>
                                                        string.Format("{0}: {1}", err.Region, err.Message)).ToArray();
            Assert.IsTrue(fileErrors.Length == 0);
        }

        [Test]
        public void TestGenIfs()
        {
            var translator = new Mq4Translator();
            Assert.IsTrue(translator.Parse(ScriptUtils.CodeWithIfs));
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
        }

        [Test]
        public void TestGenFunctionFolding()
        {
            var translator = new Mq4Translator();
            Assert.IsTrue(translator.Parse(ScriptUtils.ComplexFunctionWrongFolding));
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
        }


        [Test]
        public void TestGenComplexIfs()
        {
            var translator = new Mq4Translator();
            Assert.IsTrue(translator.Parse(ScriptUtils.ComplexIfsCode));
            var code = translator.GenerateCode();

            Assert.IsFalse(string.IsNullOrEmpty(code));
        }
    }
}