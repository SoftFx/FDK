namespace SoftFX.Lrp
{
    using System;
    using SoftFX.Lrp.Implementation;

    /// <summary>
    /// 
    /// </summary>
    public unsafe class LocalClient : BaseClient, IClient
    {
        /// <summary>
        /// Creates a new instance of LocalClient.
        /// </summary>
        /// <param name="path">An absolute or relative path to external native library.</param>
        /// <param name="signature">A protocol signature.</param>
        public LocalClient(string path, string signature)
            : this(path, signature, Mode.Auto)
        {
        }

        /// <summary>
        /// Creates a new instance of LocalClient.
        /// </summary>
        /// <param name="path">An absolute or relative path to external native library.</param>
        /// <param name="signature">A protocol signature.</param>
        /// <param name="mode">A mode of dll using.</param>
        public LocalClient(string path, string signature, Mode mode)
            : this(path, signature, null, mode)
        {
        }

        /// <summary>
        /// Creates a new instance of LocalClient.
        /// </summary>
        /// <param name="path">An absolute or relative path to external native library.</param>
        /// <param name="signature">A protocol signature.</param>
        /// <param name="currentDirectory">A directory, which should be used as current; it is used for out of process mode</param>
        /// <param name="mode">A mode of dll using.</param>
        public LocalClient(string path, string signature, string currentDirectory, Mode mode)
            : base(signature)
        {
            if (path == null)
                throw new ArgumentNullException("path", "Library path can not be null");

            var type = WinAPI.GetDllMachineType(path);
            if (mode == Mode.Auto)
            {
                mode = Mode.InProcess;
                if (type == MachineType.IMAGE_FILE_MACHINE_I386)
                {
                    if (sizeof(int) != IntPtr.Size)
                    {
                        mode = Mode.OutProcess;
                    }
                }
                else if (type == MachineType.IMAGE_FILE_MACHINE_AMD64)
                {
                    if (sizeof(int) == IntPtr.Size)
                    {
                        mode = Mode.OutProcess;
                    }
                }
            }

            if (mode == Mode.InProcess)
            {
                this.host = new InProcessHost(path);
            }
            else if (mode == Mode.OutProcess)
            {
                this.host = new OutProcessHost(type, currentDirectory, path);
            }
            else
            {
                var message = string.Format("Unsupported mode = {0}", mode);
                throw new ArgumentException(message);
            }

            try
            {
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
        /// Creates a new memory buffer. Don't use MemoryBuffer.CreateLocal() method directly, because it works incorrect for host processes.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override MemoryBuffer Create()
        {
            var result = this.host.CreateLocal();
            return result;
        }

        /// <summary>
        /// Releases local/remote host.
        /// </summary>
        public override void Dispose()
        {
            if (this.host != null)
            {
                this.host.Dispose();
                this.host = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="methodId"></param>
        /// <param name="data"></param>
        public override int Invoke(ushort componentId, ushort methodId, MemoryBuffer data)
        {
            this.Translate(ref componentId, ref methodId);
            var result = this.host.Invoke(componentId, methodId, data);

            if (result == MagicNumbers.LRP_INVALID_COMPONENT_ID)
                throw new Exception("Mismatch client/server protocol detecting: invalid component ID");

            if (result == MagicNumbers.LRP_INVALID_METHOD_ID)
                throw new Exception("Mismatch client/server protocol detecting: invalid method ID");

            return result;
        }

        #region Fields

        IHost host;

        #endregion
    }
}
