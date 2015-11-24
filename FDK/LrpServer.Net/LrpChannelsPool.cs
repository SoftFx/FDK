namespace LrpServer.Net
{
    using System;
    using LrpServer.Net.LocalCpp;
    using SoftFX.Lrp;

    /// <summary>
    /// 
    /// </summary>
    public class LrpChannelsPool : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public LrpChannelsPool(LrpParams parameters)
        {
            this.m_proxy = new LocalChannelsPoolProxy(Native.Client, parameters);
        }

        internal LPtr Handle
        {
            get
            {
                return this.m_proxy.Handle;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            var proxy = this.m_proxy;
            if (null != proxy)
            {
                this.m_proxy = null;
                proxy.Dispose();
            }
        }

        #region Members

        LocalChannelsPoolProxy m_proxy;

        #endregion
    }
}
