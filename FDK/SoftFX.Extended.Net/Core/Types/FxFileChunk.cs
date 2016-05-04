namespace SoftFX.Extended
{
    using System;

    /// <summary>
    /// Contains information about downloading file and data chunk.
    /// </summary>
    class FxFileChunk
    {
        #region Properties

        /// <summary>
        /// Gets server side file Id.
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Gets current chunk Id of the file.
        /// </summary>
        public int ChunkId { get; set; }

        /// <summary>
        /// Gets total chunks number of the file.
        /// </summary>
        public int TotalChunks { get; set; }

        /// <summary>
        /// Gets size of the file in bytes.
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// Gets data buffer of the current file chunk.
        /// </summary>
        public byte[] Data { get; set; }

        #endregion
    }
}
