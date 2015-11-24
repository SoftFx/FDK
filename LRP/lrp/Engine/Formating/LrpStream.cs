namespace Lrp.Engine.Formating
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    class LrpStream : IDisposable
    {
        public LrpStream(string path)
        {
            this.path = path;
            this.Construct();
        }

        public LrpStream(string outputDirectory, string name)
        {
            this.path = Path.Combine(outputDirectory, name);
            this.Construct();
        }

        void Construct()
        {
            this.stream = new MemoryStream();
            this.writer = new StreamWriter(this.stream);
            this.WriteLine("// This is always generated file. Do not change anything.");
            this.WriteLine();
        }

        void WriteTabs()
        {
            for (var tab = 0; tab < this.tabs; ++tab)
            {
                this.writer.Write('\t');
            }
        }

        void Increment()
        {
            ++this.tabs;
        }

        void Decrement()
        {
            if (this.tabs == 0)
                throw new InvalidOperationException("Tabs number can not be negative");

            --this.tabs;
        }

        public void Dispose()
        {
            this.writer.Dispose();
            var data = this.stream.ToArray();
            if (File.Exists(this.path))
            {
                using (var reader = new FileStream(this.path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var data2 = new Byte[reader.Length];
                    reader.Read(data2, 0, data2.Length);
                    if (Enumerable.SequenceEqual(data, data2))
                    {
                        return;
                    }
                }
            }
            using (var writer = new FileStream(this.path, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                writer.Write(data, 0, data.Length);
            }
        }

        public void BeginLine()
        {
            this.builder.Clear();
        }

        public void Write(string text)
        {
            this.builder.Append(text);
        }

        public void Write(string format, params object[] args)
        {
            this.builder.AppendFormat(format, args);
        }

        public void EndLine()
        {
            var st = this.builder.ToString();
            var first = '\0';
            var last = '\0';
            if (!string.IsNullOrEmpty(st))
            {
                first = st[0];
                last = st[st.Length - 1];
            }
            if (first == '}')
            {
                this.Decrement();
            }
            if (last == ':')
            {
                this.Decrement();
            }
            this.WriteTabs();
            if (last == ':')
            {
                this.Increment();
            }
            this.writer.WriteLine(st);
            if (first == '{')
            {
                this.Increment();
            }
        }

        public void WriteLine()
        {
            this.writer.WriteLine();
        }

        public void WriteLine(string text)
        {
            this.BeginLine();
            this.Write(text);
            this.EndLine();
        }

        public void WriteLine(string format, params object[] args)
        {
            var text = string.Format(format, args);
            WriteLine(text);
        }

        #region Fields

        int tabs;
        readonly StringBuilder builder = new StringBuilder();
        readonly string path;
        MemoryStream stream;
        StreamWriter writer;

        #endregion
    }
}
