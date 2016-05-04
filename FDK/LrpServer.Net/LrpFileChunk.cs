namespace LrpServer.Net
{
    /// <summary>
    ///
    /// </summary>
    public class LrpFileChunk
    {
        /// <summary>
        ///
        /// </summary>
        public string FileId;

        /// <summary>
        ///
        /// </summary>
        public int ChunkId;

        /// <summary>
        ///
        /// </summary>
        public int TotalChunks;

        /// <summary>
        ///
        /// </summary>
        public int FileSize;

        /// <summary>
        ///
        /// </summary>
        public byte[] Data;
    }
}
