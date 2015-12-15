namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class FileChunk
    {
        internal FileChunk(FxFileChunk fileChunk)
        {
            if (fileChunk == null)
                throw new ArgumentNullException(nameof(fileChunk));

            this.FileName = fileChunk.FileName;
            this.ChunksNumber = fileChunk.ChunksNumber;
            this.FileSize = fileChunk.FileSize;
            this.Data = fileChunk.Data;
        }

        /// <summary>
        /// Gets server side file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets total chunks number of the file.
        /// </summary>
        public int ChunksNumber { get; private set; }

        /// <summary>
        /// Gets size of the file in bytes.
        /// </summary>
        public int FileSize { get; private set; }

        /// <summary>
        /// Gets data buffer of the current file chunk.
        /// </summary>
        public byte[] Data { get; private set; }
    }
}
