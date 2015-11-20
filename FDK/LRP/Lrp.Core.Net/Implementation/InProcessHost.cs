namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    unsafe class InProcessHost : IHost
    {
        #region Types

        delegate int LrpInvoke(UInt16 componentId, UInt16 methodId, void* heap, int* size, void** ppData, int* pCapacity);
        delegate sbyte* LrpSignature();

        #endregion

        public InProcessHost(string path)
        {
            this.library = WinAPI.LoadLibraryEx(path, IntPtr.Zero, WinAPI.LOAD_WITH_ALTERED_SEARCH_PATH);
            if (this.library == IntPtr.Zero)
            {
                ThrowCanNotLoadDll(path, Marshal.GetLastWin32Error());
            }

            var funcSignature = WinAPI.GetProcAddress(this.library, "LrpSignature");
            if (funcSignature == IntPtr.Zero)
            {
                WinAPI.FreeLibrary(this.library);
                var message = string.Format("Can not find LrpSignature function for dll = {0}", path);
                throw new Exception(message);
            }

            var funcInvoke = WinAPI.GetProcAddress(this.library, "LrpInvoke");
            if (funcInvoke == IntPtr.Zero)
            {
                WinAPI.FreeLibrary(this.library);
                var message = string.Format("Can not find LrpInvoke function for dll = {0}", path);
                throw new Exception(message);
            }

            lrpInvoke = (LrpInvoke)Marshal.GetDelegateForFunctionPointer(funcInvoke, typeof(LrpInvoke));
            lrpSignature = (LrpSignature)Marshal.GetDelegateForFunctionPointer(funcSignature, typeof(LrpSignature));
        }

        static void ThrowCanNotLoadDll(string dllPath, int error)
        {
            var type = WinAPI.GetDllMachineType(dllPath);
            if (type == MachineType.IMAGE_FILE_MACHINE_I386)
            {
                if (sizeof(long) == IntPtr.Size)
                {
                    var message = string.Format("x64 process tries to load x86 dll = {0}; code = 0x{1:X}", dllPath, error);
                    throw new Exception(message);
                }
            }
            else if (type == MachineType.IMAGE_FILE_MACHINE_AMD64)
            {
                if (sizeof(int) == IntPtr.Size)
                {
                    var message = string.Format("x86 process tries to load x64 dll = {0}; code = 0x{1:X}", dllPath, error);
                    throw new Exception(message);
                }
            }
            else
            {
                var message = string.Format("Unsupported machine type = {0} of dll = {1}; error = 0x{2:X}.", type, dllPath, error);
                throw new Exception(message);
            }

            {
                var ex = new Win32Exception(error);
                var message = string.Format("Can not load library = {0}", dllPath);
                throw new Exception(message, ex);
            }
        }

        public string Signature()
        {
            return new string(this.lrpSignature());
        }

        public MemoryBuffer CreateLocal()
        {
            var result = MemoryBuffer.CreateLocal();
            return result;
        }

        public int Invoke(ushort componentId, ushort methodId, MemoryBuffer data)
        {
            var pHeap = data.Heap;
            var pData = data.Data;
            var size = data.Position;
            var capacity = data.Capacity;
            var result = this.lrpInvoke(componentId, methodId, pHeap, &size, &pData, &capacity);
            data.ReInitialize(capacity, size, pData);
            return result;
        }

        ~InProcessHost()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.lrpSignature = null;
            this.lrpInvoke = null;

            var library = this.library;
            this.library = IntPtr.Zero;
            this.lrpInvoke = null;
            WinAPI.FreeLibrary(library);
        }

        #region Fields

        IntPtr library;
        LrpSignature lrpSignature;
        LrpInvoke lrpInvoke;

        #endregion
    }
}
