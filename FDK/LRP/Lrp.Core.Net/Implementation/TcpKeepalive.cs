namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    struct TcpKeepalive
    {
        public TcpKeepalive(bool enable, uint keepaliveTime, uint keepaliveInterval)
        {
            OnOff = enable ? 1U : 0;
            KeepaliveTime = keepaliveTime;
            KeepaliveInterval = keepaliveInterval;
        }

        public int Apply(Socket socket)
        {
            var data = this.ToArray();
            var result = socket.IOControl(IOControlCode.KeepAliveValues, data, null);
            return result;
        }

        Byte[] ToArray()
        {
            var data = new List<byte>();
            var temp = BitConverter.GetBytes(OnOff);
            data.AddRange(temp);
            temp = BitConverter.GetBytes(KeepaliveTime);
            data.AddRange(temp);
            temp = BitConverter.GetBytes(KeepaliveInterval);
            data.AddRange(temp);
            var result = data.ToArray();
            return result;
        }

        #region Fields

        uint OnOff;
        uint KeepaliveTime;
        uint KeepaliveInterval;

        #endregion
    }
}
