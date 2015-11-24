namespace FdkImport.Engine.Lexemes
{
    class BaseLexeme : ILexeme
    {
        protected BaseLexeme(string replacing, string replacement)
        {
            this.pattern = new RegexEx(replacing, replacement);
        }

        public virtual string Process(string code)
        {
            var result = this.pattern.Replace(code);
            return result;
        }

        #region Members

        readonly RegexEx pattern;

        #endregion
    }
}
