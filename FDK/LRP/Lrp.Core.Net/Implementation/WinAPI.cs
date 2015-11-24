namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Runtime.InteropServices;
    using System.IO;

    static unsafe class WinAPI
    {
        public const int LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, UInt32 dwFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("Ws2_32.dll")]
        public static extern Int32 send(IntPtr s, void* buf, Int32 len, Int32 flags);

        [DllImport("Ws2_32.dll")]
        public static extern Int32 recv(IntPtr s, void* buf, Int32 len, Int32 flags);

        [DllImport("Ws2_32.dll")]
        public static extern int select(Int32 nfds, fd_set* readfds, fd_set* writefds, fd_set* exceptfds, timeval* timeout);

        [DllImport("kernel32.dll")]
        public static extern bool WaitNamedPipe(string lpNamedPipeName, int nTimeOut);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(IntPtr hFile, void* lpBuffer, int nNumberOfBytesToWrite, int* lpNumberOfBytesWritten, IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(IntPtr hFile, void* lpBuffer, int nNumberOfBytesToRead, int* lpNumberOfBytesRead, IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        public static MachineType GetDllMachineType(string dllPath)
        {
            using (var file = new FileStream(dllPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new BinaryReader(file))
                {
                    file.Seek(0x3c, SeekOrigin.Begin);
                    var peOffset = reader.ReadInt32();
                    file.Seek(peOffset, SeekOrigin.Begin);
                    var peHead = reader.ReadUInt32();
                    if (peHead != 0x00004550) // "PE\0\0", little-endian
                        return MachineType.IMAGE_FILE_MACHINE_UNKNOWN;
                    var machineType = (MachineType)reader.ReadUInt16();
                    return machineType;
                }
            }
        }

        public static readonly IntPtr INVALID_HANDLE = new IntPtr(-1);
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint OPEN_EXISTING = 3;
    }
}
