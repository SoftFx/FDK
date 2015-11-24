namespace FdkImport.Engine.Grammatics
{
    using System.Collections.Generic;
    using System.IO;

    class Code
    {
        #region construction

        public Code(string text)
        {
            this.Functions = new Dictionary<string, Function>();
            this.GlobalVarialbes = new Dictionary<string, Variable>();

            //this.Parse(stream);
        }

        #endregion

        #region Private Methods

        void Parse(StreamReader reader)
        {
            
        }

        #endregion

        #region properties

        public Dictionary<string, Function> Functions { get; private set; }
        public Dictionary<string, Variable> GlobalVarialbes { get; private set; }
        public Dictionary<string, Variable> StaticLocalVariables { get; private set; }

        #endregion
    }
}
