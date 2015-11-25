using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mql2Fdk.Translator.CodeGenerator.CodeWriters;
using Mql2Fdk.Translator.CodeGenerator.Common;
using Mql2Fdk.Translator.Common;
using Mql2Fdk.Translator.Semantic.Fixes;

namespace Mql2Fdk.Translator.CodeGenerator
{
    public class CsCodeGenerator
    {
        public CsCodeGenerator()
        {
            Initialize();
        }

        readonly List<CodeGenForNode> _codeGenerators = new List<CodeGenForNode>();

        public string GenerateCodeForNode(ParseNode node)
        {
            var sb = new StringBuilder();

            Visit(node, sb);

            return sb.ToString();
        }

        public string GenerateAttributeData()
        {
            var result = new StringBuilder();
            VisitForAttribute(result);
            return result.ToString();
        }

        void VisitForAttribute(StringBuilder result)
        {
            var propertyValues = PropertyDictionary.PropertyContents;
            foreach (var value in propertyValues)
            {
                result.AppendFormat("[{0} ({1})]", value.Key, value.Value).AppendLine();
            }
        }

        void Initialize()
        {
            AddDirectTokenWriters(new[]
                {
                    TokenKind.Space,
                    TokenKind.TypeName,
                    TokenKind.Static,
                    TokenKind.Continue,
                    TokenKind.Identifier,
                    TokenKind.Assign,
                    TokenKind.OpenParen,
                    TokenKind.CloseParen,
                    TokenKind.Comma,
                    TokenKind.Int,
                    TokenKind.Float,
                    TokenKind.PredefinedVariable,
                    TokenKind.Operator,
                    TokenKind.If,
                    TokenKind.Else,
                    TokenKind.Char,
                    TokenKind.QuotedString,
                    TokenKind.For,
                    TokenKind.While,
                    TokenKind.Break,
                    TokenKind.Continue,
                    TokenKind.Return,
                    TokenKind.Ref,
                    TokenKind.New,
                    TokenKind.Switch,
                    TokenKind.Case,
                    TokenKind.Default,
                    TokenKind.Colon,
                    TokenKind.Question,
                    TokenKind.OpenSquared,
                    TokenKind.CloseSquared,
                    TokenKind.Dot,
                    TokenKind.SharpProperty,
                    TokenKind.Throw,
                });

            AddNewLineTokenWriter(TokenKind.OpenCurly);
            AddNewLineTokenWriter(TokenKind.CloseCurly);
            AddNewLineTokenWriter(TokenKind.SemiColon);

            _codeGenerators.Add(new CommentTokenKindCodeGen());


            AddEmtpyTokenWriter(TokenKind.Extern);

            AddEmtpyTokenWriter(TokenKind.SharpImport);
            AddEmtpyTokenWriter(TokenKind.SharpDefine);
            AddEmtpyTokenWriter(TokenKind.Input);

            AddWriter<ExternWriter>();
            AddWriter<InputTokenKindCodeGen>();
            AddWriter<BlockWriter>();
            AddWriter<DefineDeclarationWriter>();


            AddWriter<ImportFunctionDeclarationWriter>();


            AddEmtpyWriter(RuleKind.SharpProperty);
            AddEmtpyWriter(RuleKind.ConditionalCode);
            AddEmtpyWriter(RuleKind.If);
            AddEmtpyWriter(RuleKind.Else);
            AddEmtpyWriter(RuleKind.InstructionCode);
            AddEmtpyWriter(RuleKind.For);
            AddEmtpyWriter(RuleKind.While);
            AddEmtpyWriter(RuleKind.Static);
            AddEmtpyWriter(RuleKind.DeclareVariable);
            AddEmtpyWriter(RuleKind.FunctionSignature);
            AddEmtpyWriter(RuleKind.FunctionDeclaration);
            AddEmtpyWriter(RuleKind.Assignment);
            AddEmtpyWriter(RuleKind.Switch);

            
            AddEmtpyWriter(RuleKind.Break);
            AddEmtpyWriter(RuleKind.Return);
            AddEmtpyWriter(RuleKind.Root);

            AddEmtpyWriter(RuleKind.Case);
            AddEmtpyWriter(RuleKind.CaseBlock);
            AddEmtpyWriter(RuleKind.ImportLibraryDeclaration);


            AddWriter<NotSupportedFinder>();
        }

        void AddEmtpyTokenWriter(TokenKind tokenKind)
        {
            var newCodeGen = new CodeGenForTokenKindEmtpy(tokenKind);
            _codeGenerators.Add(newCodeGen);
        }

        void AddWriter<T>() where T : CodeGenForNode, new()
        {
            _codeGenerators.Add(new T());
        }

        void AddEmtpyWriter(RuleKind rule)
        {
            _codeGenerators.Add(new EmtpyCodeGenForT(rule));
        }

        void AddDirectTokenWriters(IEnumerable<TokenKind> tokens)
        {
            foreach (var tokenKind in tokens)
            {
                AddDirectTokenWriter(tokenKind);
            }
        }

        void AddDirectTokenWriter(TokenKind tokenKind)
        {
            var newCodeGen = new CodeGenForTokenKindWrite(tokenKind);
            _codeGenerators.Add(newCodeGen);
        }

        void AddNewLineTokenWriter(TokenKind tokenKind)
        {
            var newCodeGen = new NewLineCodeGenForTokenKind(tokenKind);
            _codeGenerators.Add(newCodeGen);
        }

        void Visit(ParseNode node, StringBuilder sb)
        {
            var skipChildren = WriteNode(sb, node);
            if (node.Count == 0 || skipChildren)
                return;
            foreach (var child in node.Children)
            {
                Visit(child, sb);
            }
        }

        bool WriteNode(StringBuilder sb, ParseNode child)
        {
            var acceptCodeGen = GetCodeGenerator(child);
            if (acceptCodeGen == null) return false;
            sb.Append(acceptCodeGen.DoWrite(child));
            return acceptCodeGen.SkipChildrenNode;
        }

        CodeGenForNode GetCodeGenerator(ParseNode child)
        {
            return _codeGenerators.FirstOrDefault(codeGenForNode => codeGenForNode.Accept(child));
        }
    }
}