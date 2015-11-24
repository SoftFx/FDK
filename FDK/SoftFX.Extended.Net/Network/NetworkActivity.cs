namespace SoftFX.Extended
{
    /// <summary>
    /// The class contains statistics of a client connection.
    /// </summary>
    public class NetworkActivity
    {
        internal NetworkActivity(long dataBytesSent, long sslBytesSent, long dataBytesReceived, long sslBytesReceived)
        {
            this.DataBytesSent = dataBytesSent;
            this.SslBytesSent = sslBytesSent;
            this.DataBytesReceived = dataBytesReceived;
            this.SslBytesReceived = sslBytesReceived;
        }

        /// <summary>
        /// Returns number of unencrypted bytes, which have been sent;
        /// this value represents quantity of logical data.
        /// </summary>
        public long DataBytesSent { get; private set; }

        /// <summary>
        /// Returns number of encrypted bytes, which have been sent;
        /// this value represents quantity of physical data.
        /// </summary>
        public long SslBytesSent { get; private set; }

        /// <summary>
        /// Returns number of unencrypted bytes, which have been received;
        /// this value represents quantity of logical data.
        /// Zero for non-secure connection.
        /// </summary>
        public long DataBytesReceived { get; private set; }

        /// <summary>
        /// Returns number of encrypted bytes, which have been received;
        /// this value represents quantity of physical data.
        /// Zero for non-secure connection.
        /// </summary>
        public long SslBytesReceived { get; private set; }
    }
}
