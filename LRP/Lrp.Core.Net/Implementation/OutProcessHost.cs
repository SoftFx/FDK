namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using SoftFX.Lrp.Resources;

    unsafe class OutProcessHost : IHost
    {
        public OutProcessHost(MachineType type, string currentDirectory, string path)
        {
            if (currentDirectory == null)
                currentDirectory = Directory.GetCurrentDirectory();

            string lrpHostPath = null;
            if (type == MachineType.IMAGE_FILE_MACHINE_I386)
            {
                this.isPtr32Bit = true;
                lrpHostPath = FilesResolver.LrpX86HostPath;
            }
            else if (type == MachineType.IMAGE_FILE_MACHINE_AMD64)
            {
                this.isPtr32Bit = false;
                lrpHostPath = FilesResolver.LrpX64HostPath;
            }
            else
            {
                var message = string.Format("Unsupported machine type = {0}", type);
                throw new ArgumentException(message, "type");
            }

            this.pipId = Guid.NewGuid().ToString();

            using (var semaphore = new Semaphore(0, 1, this.pipId))
            {
                var args = string.Format("\"{0}\" {1} \"{2}\"", currentDirectory, this.pipId, path);
                var process = Process.Start(lrpHostPath, args);
                process.Dispose();
                if (!semaphore.WaitOne(PipeWaitingTimeout))
                    throw new TimeoutException("Timeout of LRP host waiting has been reached.");
            }

            var pipeName = @"\\.\pipe\" + this.pipId;

            if (!WinAPI.WaitNamedPipe(pipeName, PipeWaitingTimeout))
                throw new TimeoutException("Timeout of pipe waiting has been reached.");

            this.pipe = WinAPI.CreateFile(pipeName, WinAPI.GENERIC_READ | WinAPI.GENERIC_WRITE, 0, IntPtr.Zero, WinAPI.OPEN_EXISTING, 0, IntPtr.Zero);
            if (this.pipe == WinAPI.INVALID_HANDLE)
                throw new Exception("Couldn't open pipe");

            using (var transaction = new Transaction(this))
            {
                var buffer = this.ReadBuffer();
                var isValid = buffer.ReadBoolean();
                if (isValid)
                {
                    this.signature = buffer.ReadAString();
                }
                else
                {
                    var error = buffer.ReadInt32();
                    var message = buffer.ReadAString();
                    throw new Win32Exception(error, message);
                }

                transaction.Commit();
            }
        }

        public void Dispose()
        {
            if (this.pipe != WinAPI.INVALID_HANDLE)
            {
                WinAPI.CloseHandle(this.pipe);
                this.pipe = WinAPI.INVALID_HANDLE;
            }
        }

        public string Signature()
        {
            return this.signature;
        }

        public MemoryBuffer CreateLocal()
        {
            var result = MemoryBuffer.CreateLocal();
            result.IsLPtr32Bit = this.isPtr32Bit;
            return result;
        }

        public unsafe int Invoke(ushort componentId, ushort methodId, MemoryBuffer data)
        {
            var prolog = new ComponentMethodSize(componentId, methodId, data.Size);

            this.WriteData(&prolog, sizeof(ComponentMethodSize));
            this.WriteData(data.Data, data.Size);

            var result = this.ReadBuffer(data);
            return result;
        }

        #region Reading

        int ReadBuffer(MemoryBuffer buffer)
        {
            var prolog = new ResultSize();

            this.ReadData(&prolog, sizeof(ResultSize));
            buffer.Construct(prolog.Size);
            this.ReadData(buffer.Data, buffer.Size);
            return prolog.Result;
        }

        MemoryBuffer ReadBuffer()
        {
            var size = 0;
            this.ReadData(&size, sizeof(uint));
            var result = MemoryBuffer.CreateLocal(size);
            result.Construct(size);
            this.ReadData(result.Data, result.Size);
            return result;
        }

        void ReadData(void* buffer, int count)
        {
            var data = (byte*)buffer;
            for (; count > 0;)
            {
                var readCount = 0;
                if (!WinAPI.ReadFile(this.pipe, data, count, &readCount, IntPtr.Zero))
                    throw new ProtocolException("Couldn't read data from pipe");

                data += readCount;
                count -= readCount;
            }
        }

        #endregion

        #region Writing

        void Write(MemoryBuffer buffer)
        {
            this.WriteData(buffer.Data, buffer.Size);
        }

        void WriteData(void* buffer, int count)
        {
            var data = (sbyte*)buffer;
            for (; count > 0; )
            {
                var writtenCount = 0;
                if (!WinAPI.WriteFile(this.pipe, data, count, &writtenCount, IntPtr.Zero) || (0 == writtenCount))
                    throw new ProtocolException("Couldn't write data");

                data += writtenCount;
                count -= writtenCount;
            }
        }

        #endregion

        #region Fields

        readonly bool isPtr32Bit;
        readonly string pipId;
        const int PipeWaitingTimeout = 60000;
        IntPtr pipe;
        string signature;
        const int LargeSize = 1024 * 1024;

        #endregion
    }
}
