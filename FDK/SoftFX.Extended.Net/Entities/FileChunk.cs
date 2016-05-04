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

            this.FileId = fileChunk.FileId;
            this.ChunkId = fileChunk.ChunkId;
            this.TotalChunks = fileChunk.TotalChunks;
            this.FileSize = fileChunk.FileSize;
            this.Data = fileChunk.Data;
        }

        /// <summary>
        /// Gets server side file Id.
        /// </summary>
        public string FileId { get; private set; }

        /// <summary>
        /// Gets current chunks Id of the file.
        /// </summary>
        public int ChunkId { get; private set; }

        /// <summary>
        /// Gets total chunks number of the file.
        /// </summary>
        public int TotalChunks { get; private set; }

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
