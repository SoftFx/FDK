using System.Text;
using Mql2Fdk.Translator.CodeGenerator;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Lexer;
using Mql2Fdk.Translator.Lexer.Preprocessor;
using Mql2Fdk.Translator.Parser;
using Mql2Fdk.Translator.Semantic;

namespace Mql2Fdk.Translator.Translator
{
    public class Mq4Translator
    {
        readonly Mq4Lexer _lexer;
        readonly Mq4Parser _parser;
        readonly CsCodeGenerator _codeGenerator;

        public ParseNode AstTree { get; set; }

        public Mq4Translator()
        {
            _lexer = new Mq4Lexer();
            _parser = new Mq4Parser();
            _codeGenerator = new CsCodeGenerator();
        }

        public bool Parse(string code, bool peformSemanticFixes=true)
        {
            var resultTokens = _lexer.BuildTextTokens(code);
            FileIncludeLogic.ScanAndImportIncludes(resultTokens);
            AstTree = _parser.Parse(resultTokens);
            if (peformSemanticFixes)
            {
                var semantic = new SemanticAnalysis(AstTree);
                semantic.Perform();
            }
            return true;
        }

        public string GenerateCode()
        {
            var generateCodeForNode = _codeGenerator.GenerateCodeForNode(AstTree);
            var generateAttributeForNode = _codeGenerator.GenerateAttributeData();
            var finalCode = new StringBuilder();
            const string usingBlock = @"using System;
using Mql2Fdk;
using Mql2Fdk.Attributes;
using System.Runtime.InteropServices;
";

            const string classHeader = @"
public class MyAdviser : MqlAdapter
{";

            finalCode.AppendLine(usingBlock);
            finalCode.Append(generateAttributeForNode);
            finalCode.AppendLine(classHeader);
            finalCode.AppendLine(generateCodeForNode);
            finalCode.AppendLine("}");
            return finalCode.ToString();
        }
    }
}