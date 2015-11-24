using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Mq4RewriterTesting.Common;
using Mql2Fdk.Translator.Lexer.Preprocessor;
using NUnit.Framework;
using SharedLogic;

namespace Mq4RewriterTesting.CodeGen
{
    [TestFixture]
    public class InternetSamplesTests
    {
        [Test]
        public void TestInternetFilesParsing()
        {
            var combinedPaths = TestVariousFiles.GetInternetFiles();
            IncludePaths.BlackListIncludeFile("stdlib.mqh");
            IncludePaths.AddDirectoryInclude(@"C:\ProgramData\MetaTrader ECN - FXOpen\experts\include\");
            var total = 0;
            var success = 0;
            var dictionaryErrors = new SortedDictionary<string, Exception>();
            foreach (var combinedPath in combinedPaths)
            {
                total++;
                try
                {
                    TestVariousFiles.TestFileParsing(combinedPath);
                    success++;
                }
                catch (Exception ex)
                {
                    dictionaryErrors[combinedPath] = ex;
                }
            }
            Assert.AreEqual(total, success);
        }

        [Test]
        public void TestInternetFilesSemantic()
        {
            var combinedPaths = TestVariousFiles.GetInternetFiles();
            IncludePaths.BlackListIncludeFile("stdlib.mqh");
            IncludePaths.AddDirectoryInclude(@"C:\ProgramData\MetaTrader ECN - FXOpen\experts\include\");
            var total = 0;
            var success = 0;
            var dictionaryErrors = new SortedDictionary<string, Exception>();
            foreach (var combinedPath in combinedPaths)
            {
                total++;
                try
                {
                    TestVariousFiles.TestFileSemantic(combinedPath);
                    success++;
                }
                catch (Exception ex)
                {
                    dictionaryErrors[combinedPath] = ex;
                }
            }
            Assert.AreEqual(total, success);
        }

        [Test]
        public void TestInternetFilesAgainstCompiler()
        {
            var combinedPaths = TestVariousFiles.GetInternetFiles();
            IncludePaths.BlackListIncludeFile("stdlib.mqh");
            IncludePaths.AddDirectoryInclude(@"C:\ProgramData\MetaTrader ECN - FXOpen\experts\include\");
            var total = 0;
            var success = 0;
            var dictionaryErrors = new SortedDictionary<string, Exception>();
            var successfullySemanticalyParsed = new List<string>();
            var csCompiledErrors = new Dictionary<string, string[]>();
            foreach (var combinedPath in combinedPaths)
            {
                total++;
                try
                {
                    var compilerScript = TestVariousFiles.GetFileResultScript(combinedPath);
                    success++;
                    var result = CompileCs(compilerScript, combinedPath);
                    if (result.Length == 0)
                        successfullySemanticalyParsed.Add(combinedPath);
                    else
                    {
                        var bareFile = combinedPath.RemoveIncluding("\\");
                        csCompiledErrors[combinedPath] = result
                            .Select(item => 
                                string.Format("{0} ({1})",
                                item.RemoveIncluding("error").RemoveIncluding(":"),
                                bareFile)
                                )
                            .ToArray();
                    }
                }
                catch (Exception ex)
                {
                    dictionaryErrors[combinedPath] = ex;
                }
            }
            var allCsCompiledErrors = new List<string>();
            foreach (var csCompiledError in csCompiledErrors)
            {
                var collection = csCompiledError.Value;
                allCsCompiledErrors.AddRange(collection);
            }
            allCsCompiledErrors.Sort();

            Assert.AreEqual(total, success);
        }

        private string[] CompileCs(string compilerScript, string combinedPath)
        {
            var csharpCompilerPath = Path.Combine(Path.GetDirectoryName(typeof (object).Assembly.Location),
                                                  "csc.exe");
            var outputExe = Path.ChangeExtension(combinedPath, "exe");
            var paramsToCompiler = new StringBuilder();
            const string originalPathOfDll = @"..\..\..\Debug";
            var mql2FdkDll = Path.Combine(originalPathOfDll, "Mql2Fdk.dll");
            paramsToCompiler.AppendFormat("/reference:{0} ", mql2FdkDll);

            var softFxDll = Path.Combine(originalPathOfDll, "SoftFX.Net.dll");
            paramsToCompiler.AppendFormat("/reference:{0} ", softFxDll);

            var softFxExtDll = Path.Combine(originalPathOfDll, "SoftFX.Extended.Net.dll");
            paramsToCompiler.AppendFormat("/reference:{0} ", softFxExtDll);

            paramsToCompiler.AppendFormat("/out:{0} ", outputExe);

            var fullFilePath = Path.Combine(ScriptUtils.PathToScripts, "example.cs");
            File.WriteAllText(fullFilePath, compilerScript);
            var mainFilePath = Path.Combine(ScriptUtils.PathToScripts, "Program.cs");
            paramsToCompiler.AppendFormat("{0} {1} ", fullFilePath, mainFilePath);

            var output = RunProcessAndGetOutput(csharpCompilerPath, paramsToCompiler.ToString());
            var lines = output.Split(new[] {'\r', '\n'})
                .Where(line => line.Contains("error"))
                .ToArray();
            return lines;
        }

        private string RunProcessAndGetOutput(string compilerPath, string csharpCompilerPath)
        {
            var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                        {
                            FileName = compilerPath,
                            Arguments = csharpCompilerPath,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                };
            proc.Start();
            proc.WaitForExit(5000);
            return proc.StandardOutput.ReadToEnd();
        }

        [Test]
        public void TestInternetFilesLexing()
        {
            var combinedPaths = TestVariousFiles.GetInternetFiles();
            var total = 0;
            var success = 0;
            var dictionaryErrors = new Dictionary<string, Exception>();
            foreach (var combinedPath in combinedPaths)
            {
                total++;
                try
                {
                    TestVariousFiles.TestFileLexing(combinedPath);
                    success++;
                }
                catch (Exception ex)
                {
                    dictionaryErrors[combinedPath] = ex;
                }
            }
            Assert.AreEqual(total, success);
        }
    }
}