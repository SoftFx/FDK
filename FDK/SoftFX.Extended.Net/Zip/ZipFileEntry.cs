using System;
using System.IO;

namespace SoftFX.Extended.Zip
{
    internal class ZipFileEntry : ZipEntry
    {
        #region Private Variables
        
        private string fileName;
        private Stream data;
        private uint crc;

        #endregion

        #region Constructors

        public ZipFileEntry(string fileName, Stream data)
        {
            this.fileName = fileName;
            this.data = data;
            ComputeCrc();
        }
       
        #endregion

        #region Public Properties

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public Stream Data
        {
            get { return data; }
            set
            {
                data = value;
                ComputeCrc();
            }
        }

        internal uint Crc
        {
            get { return crc; }
        }

        #endregion

        #region Private Methods

        private void ComputeCrc()
        {
            if (data != null && data.Length != 0)
            {
                crc = Crc32.Compute(data);
            }
            else
            {
                crc = 0;
            }
        }

        #endregion
    }
}
