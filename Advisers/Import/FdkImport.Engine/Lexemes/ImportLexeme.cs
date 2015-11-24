namespace FdkImport.Engine.Lexemes
{
    using System.Text.RegularExpressions;

    class ImportLexeme : ILexeme
    {
        public ImportLexeme()
        {
            this.dll = string.Empty;
        }

        public string Process(string code)
        {
            if (!this.TryProcessAsImport(ref code))
            {
                if (!string.IsNullOrEmpty(this.dll) && !string.IsNullOrWhiteSpace(code))
                {
                    this.TryPorcessAsFunction(ref code);
                }
            }
            return code;
        }

        bool TryProcessAsImport(ref string code)
        {
            var match = this.startImport.Match(code);
            if (match.Success)
            {
                this.dll = match.Groups[2].Value;
                code = string.Empty;
                return true;
            }
            match = this.finishImport.Match(code);
            if (match.Success)
            {
                this.dll = string.Empty;
                code = string.Empty;
                return true;
            }
            return false;
        }

        void TryPorcessAsFunction(ref string code)
        {
            code = string.Format("[DllImport(@\"{0}\", CharSet = CharSet.Ansi)]\r\nprivate static extern {1}", this.dll, code);
        }

        #region Members

        string dll;
        readonly Regex startImport = new Regex("(^|\\s+)#import\\s+\"([^\"]*)\"", RegexOptions.Compiled);
        readonly Regex finishImport = new Regex(@"(^|\s+)#import(\s+|$)", RegexOptions.Compiled);

        #endregion
    }
}
