using System;
using System.Collections.Generic;
using System.Text;

namespace SoftFX.Extended.Zip
{
    internal class ZipDirectoryEntry : ZipEntry
    {
        #region Private Variables

        private string directoryName;
        
        #endregion

        #region Constructors

        public ZipDirectoryEntry(string directoryName)
        {
            this.directoryName = directoryName;
        }

        #endregion

        #region Public Properties

        public string DirectoryName
        {
            get { return directoryName; }
            set { directoryName = value; }
                
        }

        #endregion
    }
}
