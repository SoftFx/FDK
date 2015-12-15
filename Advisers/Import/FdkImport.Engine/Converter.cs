namespace FdkImport.Engine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using FdkImport.Engine.Lexemes;

    /// <summary>
    /// The class provides methods for converting advisers/indicators/scripts to FDk/C#.
    /// This class is single threaded.
    /// </summary>
    public class Converter
    {
        #region Construction

        /// <summary>
        /// Initializes converter class:
        /// 1) creates lexeme parsers
        /// </summary>
        public Converter()
        {
            this.lexemes.Add(new DefineLexeme());
            this.lexemes.Add(new ExternLexeme());
            this.lexemes.Add(new NullLexeme());
            this.lexemes.Add(new ForLoop());
            this.lexemes.Add(new FalseLexeme());
            this.lexemes.Add(new False2Lexeme());
            this.lexemes.Add(new TrueLexeme());
            this.lexemes.Add(new True2Lexeme());
            this.lexemes.Add(new ImportLexeme());
            this.lexemes.Add(new PropertyLexeme());
            this.lexemes.Add(new RefLexeme());
            this.lexemes.Add(new Array1Lexeme());
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts an existing file to C# file.
        /// </summary>
        /// <param name="namesapceName">a namespace name, which should be used; can be null or empty</param>
        /// <param name="className">a class name, which should be used</param>
        /// <param name="intputPath">relative or absolute path of an input file</param>
        /// <param name="outputPath">relative or absolute path of an output C# file</param>
        /// <exception cref="System.ArgumentNullException">if className, inputPath or outputPath are null</exception>
        public void Process(string namesapceName, string className, string intputPath, string outputPath)
        {
            if (className == null)
                throw new ArgumentNullException("className");

            if (intputPath == null)
                throw new ArgumentNullException("intputPath");

            if (outputPath == null)
                throw new ArgumentNullException("outputPath");

            using (var reader = new StreamReader(intputPath, Encoding.Default))
            {
                using (var writer = new StreamWriter(outputPath, false, Encoding.Default))
                {
                    this.Process(namesapceName, className, reader, writer);
                }
            }
        }

        /// <summary>
        /// Converts an existing source code to C# code.
        /// </summary>
        /// <param name="namesapceName">a namespace name, which should be used; can be null or empty</param>
        /// <param name="className">a class name, which should be used</param>
        /// <param name="source">an existing source code</param>
        /// <returns>C# code</returns>
        public string Process(string namesapceName, string className, string source)
        {
            if (className == null)
                throw new ArgumentNullException("className");
            if (source == null)
                throw new ArgumentNullException("source");

            var input = new MemoryStream();
            var output = new MemoryStream();
            {
                var writer = new StreamWriter(input);
                writer.Write(source);
                writer.Flush();
                input.Seek(0, SeekOrigin.Begin);
            }
            {
                var reader = new StreamReader(input);
                var writer = new StreamWriter(output);
                this.Process(namesapceName, className, reader, writer);
                writer.Flush();
                output.Seek(0, SeekOrigin.Begin);
            }
            {
                var reader = new StreamReader(output);
                var result = reader.ReadToEnd();
                return result;
            }
        }

        /// <summary>
        /// Converts an existing file to C# file.
        /// </summary>
        /// <param name="namesapceName">a namespace name, which should be used; can be null or empty</param>
        /// <param name="className">a class name, which should be used</param>
        /// <param name="reader">an input stream, which contains adviser/indicator or script</param>
        /// <param name="writer">an output stream for C# code</param>
        /// <exception cref="System.ArgumentNullException">if className, reader or writer are null</exception>
        public void Process(string namesapceName, string className, StreamReader reader, StreamWriter writer)
        {
            try
            {
                this.stream = new StreamEx(reader, writer);
                this.namespaceName = namesapceName;
                this.className = className;
                this.WriteProlog();
                this.Loop();
                this.WriteEpilog();
            }
            finally
            {
                this.className = null;
                this.namespaceName = null;
                this.stream = null;
            }
        }

        #endregion

        #region Private Methods

        private void WriteProlog()
        {
            this.stream.WriteLine("using System;");
            this.stream.WriteLine("using SoftFX;");
            this.stream.WriteLine("using SoftFX.Adapters.C;");
            this.stream.WriteLine("using System.Runtime.InteropServices;");
            this.stream.WriteLine();
            if (!string.IsNullOrEmpty(this.namespaceName))
            {
                this.stream.WriteLine("namespace {0}", this.namespaceName);
                this.stream.WriteLine("{");
#if !DEBUG
                this.stream.Indent++;
#endif
            }
            this.stream.WriteLine("public class {0} : BaseCAdapter", this.className);
            this.stream.WriteLine("{");
#if !DEBUG
            this.stream.Indent++;
#endif
        }

        void Loop()
        {
            for (var line = this.stream.ReadLine(); null != line; line = this.stream.ReadLine())
            {
                this.Proces(line);
            }
        }

        void Proces(string line)
        {
            this.stream.BeginLine();
            for (var index = 0; index < line.Length;)
            {
                if (this.isComment)
                {
                    index = ProcessComment(line, index);
                }
                else
                {
                    index = ProcessText(line, index);
                }
            }
            this.stream.EndLine();
        }

        int ProcessComment(string line, int index)
        {
            var result = line.IndexOf("*/", index);
            if (-1 == result)
            {
                result = line.Length;
            }
            else
            {
                this.isComment = false;
            }
            var st = line.Substring(index, result - index);
            this.stream.Write(st);
            return result;
        }

        int ProcessText(string line, int index)
        {
            var indexOfBlockComment = line.IndexOf("/*", index);
            if (-1 == indexOfBlockComment)
            {
                indexOfBlockComment = line.Length;
            }

            var indexOfLineComment = FindIndexOfLineComment(line, index);
            if (-1 == indexOfLineComment)
            {
                indexOfLineComment = line.Length;
            }

            var result = Math.Min(indexOfBlockComment, indexOfLineComment);

            var code = line.Substring(index, result - index);
            this.ProcessCode(code);

            if (indexOfLineComment < indexOfBlockComment)
            {
                var comment = line.Substring(result, line.Length - result);
                this.stream.Write(comment);
                result = line.Length;
            }
            else if (indexOfBlockComment < line.Length)
            {
                this.isComment = true;
            }
            return result;
        }
        
        int FindIndexOfLineComment(string line, int start)
        {
            var counter = -1;
            var isPreviousSlash = false;

            for (var index = start; index < line.Length; ++index)
            {
                var ch = line[index];
                if (counter == -1)
                {
                    if ('/' == ch)
                    {
                        if (isPreviousSlash)
                        {
                            return (index - 1);
                        }
                        else
                        {
                            isPreviousSlash = true;
                        }
                    }
                    else
                    {
                        isPreviousSlash = false;
                    }
                    if ('"' == ch)
                    {
                        counter = 0;
                    }
                }
                else if (counter == 0)
                {
                    if ('\\' == ch)
                    {
                        counter = 1;
                    }
                    else if ('"' == ch)
                    {
                        counter = -1;
                    }
                }
                else if (counter == 1)
                {
                    if ('\\' == ch)
                    {
                        counter = 0;
                    }
                    else if ('"' == ch)
                    {
                        counter = 0;
                    }
                }
            }
            return -1;
        }

        void ProcessCode(string code)
        {
            foreach (var element in this.lexemes)
            {
                code = element.Process(code);
            }
            this.stream.Write(code);
        }

        void WriteEpilog()
        {
#if !DEBUG
            this.stream.Indent--;
#endif
            this.stream.WriteLine("}");
            if (!string.IsNullOrEmpty(this.namespaceName))
            {
#if !DEBUG
                this.stream.Indent--;
#endif
                this.stream.WriteLine("}");
            }
        }

        #endregion

        #region Members

        bool isComment;
        StreamEx stream;
        string namespaceName;
        string className;
        readonly List<ILexeme> lexemes = new List<ILexeme>();

        #endregion
    }
}
