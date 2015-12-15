namespace SoftFX.Extended
{
    using System;
    using System.IO;

    /// <summary>
    /// The class provides methods for reading server side files.
    /// </summary>
    public class DataStream : Stream
    {
        /// <summary>
        /// Creates a new data stream object.
        /// </summary>
        /// <param name="dataClient">A data client; can not be null.</param>
        /// <param name="fileId">A server file Id; can not be null.</param>
        /// <exception cref="System.ArgumentNullException">If dataClient or fileId is null.</exception>
        public DataStream(DataClient dataClient, string fileId)
            : this(dataClient, fileId, 0)
        {
        }

        /// <summary>
        /// Creates a new data stream object.
        /// </summary>
        /// <param name="dataClient">A data client; can not be null.</param>
        /// <param name="fileId">A server file Id; can not be null.</param>
        /// <param name="timeoutInMilliseconds">
        /// Timeout of server requests in milliseconds.
        /// If the value is not positive, then default data client timeout will be used.
        /// </param>
        public DataStream(DataClient dataClient, string fileId, int timeoutInMilliseconds)
        {
            if (dataClient == null)
                throw new ArgumentNullException(nameof(dataClient), "Data feed can not be null.");

            if (fileId == null)
                throw new ArgumentNullException(nameof(fileId), "File ID can not be null.");

            this.timeoutInMilliseconds = timeoutInMilliseconds;
            this.server = dataClient.DataServer;
            this.fileId = fileId;

            FxFileChunk chunk;
            if (timeoutInMilliseconds > 0)
                chunk = this.server.GetFileChunkEx(this.fileId, this.chunkId, timeoutInMilliseconds);
            else
                chunk = this.server.GetFileChunk(this.fileId, this.chunkId);

            this.FileName = chunk.FileName;
            this.fileSize = chunk.FileSize;
            this.data = chunk.Data;
            this.chunksCount = chunk.ChunksNumber;
            this.chunkId++;
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, the buffer contains the specified
        /// byte array with the values between offset and (offset + count - 1) replaced
        /// by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        ///  The zero-based byte offset in buffer at which to begin storing the data read from the current stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <exception cref="System.IO.IOException">If data stream detects that the reading file has been changed.</exception>
        /// <returns>Number of</returns>
        public override int Read(Byte[] buffer, int offset, int count)
        {
            var result = 0;
            for (; count > 0; count--, result++)
            {
                if (this.position >= this.data.Length)
                {
                    if (!this.ReadBuffer())
                    {
                        return result;
                    }
                }
                buffer[offset] = this.data[this.position++];
                this.filePosition++;
                offset++;
            }

            return result;
        }

        bool ReadBuffer()
        {
            if (this.chunkId == this.chunksCount)
                return false;

            this.position = 0;
            FxFileChunk chunk;
            if (this.timeoutInMilliseconds > 0)
                chunk = this.server.GetFileChunkEx(this.fileId, this.chunkId, this.timeoutInMilliseconds);
            else
                chunk = this.server.GetFileChunk(this.fileId, this.chunkId);

            this.chunkId++;
            if (this.chunksCount != chunk.ChunksNumber)
            {
                var message = string.Format("Mismatch chunks number: expected = {0}, but received = {1}", this.chunksCount, chunk.ChunksNumber);
                throw new IOException(message);
            }
            if (this.fileSize != chunk.FileSize)
            {
                var message = string.Format("Mismatch file size: expected = {0}, but received = {1}", fileSize, chunk.FileSize);
                throw new IOException(message);
            }
            if (this.FileName != chunk.FileName)
            {
                var message = string.Format("Mismatch file name: expected = {0}, but received = {1}", this.FileName, chunk.FileName);
                throw new IOException(message);
            }

            this.data = chunk.Data;
            var result = this.data.Length > 0;
            return result;
        }

        /// <summary>
        /// Reads all data from the current stream position to the end of the stream and returns as array.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public byte[] ToArray()
        {
            var count = this.fileSize - this.filePosition;
            var result = new byte[count];
            var status = this.Read(result, 0, count);
            if (status != count)
            {
                var message = string.Format("End of the stream has been reached unexpectedly; reading bytes = {0}; read bytes = {1}", count, status);
                throw new IOException(message);
            }
            return result;
        }

        #region Properties

        /// <summary>
        /// Gets server side file name.
        /// </summary>
        public string FileName { get; private set; }

        #endregion

        #region Not supported and Simple Implementation methods

        /// <summary>
        /// Gets always true.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets always false.
        /// </summary>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// Gets always false.
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Always throw exception.
        /// </summary>
        /// <exception cref="System.IO.IOException">Always.</exception>
        public override void Flush()
        {
            throw new IOException("Data stream can not be flashed.");
        }

        /// <summary>
        /// Always throw exception.
        /// </summary>
        /// <exception cref="System.IO.IOException">Always.</exception>
        public override long Length
        {
            get
            {
                return this.fileSize;
            }
        }

        /// <summary>
        /// The property get current stream position.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Always for set operation.</exception>
        public override long Position
        {
            get
            {
                return this.filePosition;
            }
            set
            {
                throw new NotSupportedException("Data stream doesn't support Position property setter.");
            }
        }

        /// <summary>
        ///  The method doesn't supported by DataStream.
        /// </summary>
        /// <param name="offset">The parameter is ignored.</param>
        /// <param name="origin">The parameter is ignored.</param>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Data stream doesn't support Seek method.");
        }

        /// <summary>
        /// The method doesn't supported by DataStream.
        /// </summary>
        /// <param name="value">The parameter is ignored.</param>
        /// <exception cref="System.NotImplementedException">Always.</exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("Data stream doesn't support SetLength method.");
        }

        /// <summary>
        /// The method doesn't supported by DataStream.
        /// </summary>
        /// <param name="buffer">The parameter is ignored.</param>
        /// <param name="offset">The parameter is ignored.</param>
        /// <param name="count">The parameter is ignored</param>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Data stream doesn't support Write method.");
        }

        #endregion

        #region Members

        readonly DataServer server;
        readonly string fileId;
        readonly int timeoutInMilliseconds;
        int chunkId;
        int chunksCount;
        int filePosition;
        int position;
        int fileSize;
        byte[] data;

        #endregion
    }
}
