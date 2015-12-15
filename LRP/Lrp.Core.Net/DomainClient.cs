namespace SoftFX.Lrp
{
    using System;
    using SoftFX.Lrp.Implementation;

    /// <summary>
    /// 
    /// </summary>
    public class DomainClient : BaseClient
    {
        /// <summary>
        /// Creates a new instance of LocalClient.
        /// </summary>
        /// <param name="path">An absolute or relative path to external .NET library.</param>
        /// <param name="type">a full name of .NET type</param>
        /// <param name="signature">A protocol signature.</param>
        public DomainClient(string path, string type, string signature)
            : base(signature)
        {
            this.domain = AppDomain.CreateDomain("SoftFX.Lrp.DomainClient", null, null);
            try
            {
                var assembly = typeof(DomainClient).Assembly.Location;
                this.host = (DomainHost)this.domain.CreateInstanceFromAndUnwrap(assembly, "SoftFX.Lrp.Implementation.DomainHost");
                this.host.Construct(path, type);

                var remoteSignature = this.host.Signature();
                this.Initialize(remoteSignature);
            }
            catch
            {
                this.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override MemoryBuffer Create()
        {
            var result = MemoryBuffer.CreateLocal();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="methodId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public unsafe override int Invoke(ushort componentId, ushort methodId, MemoryBuffer data)
        {
            var pHeap = data.Heap;
            var pData = data.Data;
            var size = data.Position;
            var capacity = data.Capacity;
            var result = this.host.Invoke(componentId, methodId, pHeap, &size, &pData, &capacity);
            data.ReInitialize(capacity, size, pData);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            if (this.domain != null)
            {
                AppDomain.Unload(this.domain);
                this.domain = null;
                this.host = null;
            }
        }

        #region Fields

        AppDomain domain;
        DomainHost host;

        #endregion
    }
}
