namespace LrpServer.Net
{
    using System;

    public class LrpParams
    {
        #region Construction

        public LrpParams()
        {
            this.m_logPath = string.Empty;
        }

        #endregion

        #region Properties

        public bool EnableCodec { get; set; }

        public bool ValidateCodec { get; set; }

        public int ThreadsNumber
        {
            get
            {
                return this.m_threadsNumber;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Threads number limit can not be negative", "value");
                }
                this.m_threadsNumber = value;
            }
        }

        public int MessagesNumberLimit
        {
            get
            {
                return this.m_messagesNumberLimit;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Messages number limit can not be negative", "value");
                }
                this.m_messagesNumberLimit = value;
            }
        }

        public int MessagesSizeLimit
        {
            get
            {
                return this.m_messagesSizeLimit;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Messages size limit can not be negative", "value");
                }
                this.m_messagesSizeLimit = value;
            }
        }

        /// <summary>
        /// Handshake timeout in ms.
        /// </summary>
        public int HandshakeTimeout
        {
            get
            {
                return this.m_handshakeTimeout;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Handshake timeout can not be negative", "value");
                }
                this.m_handshakeTimeout = value;
            }
        }

        /// <summary>
        /// Heartbeat timeout in ms.
        /// </summary>
        public int HeartbeatTimeout
        {
            get
            {
                return this.m_heartbeatTimeout;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Heartbeat timeout can not be negative", "value");
                }
                this.m_heartbeatTimeout = value;
            }
        }

        public string LogPath
        {
            get
            {
                return this.m_logPath;
            }
            set
            {
                if (null == value)
                {
                    value = string.Empty;
                }
                this.m_logPath = value;
            }
        }

        #endregion

        #region Members

        int m_threadsNumber;
        int m_messagesNumberLimit;
        int m_messagesSizeLimit;
        int m_handshakeTimeout;
        int m_heartbeatTimeout;
        string m_logPath;

        #endregion
    }
}
