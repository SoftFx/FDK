namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    struct fd_set
    {
        uint fd_count;
        IntPtr fd_array;

        public static fd_set Null
        {
            get
            {
                return new fd_set();
            }
        }

        public static fd_set Create(IntPtr socket)
        {
            var handle = new fd_set
            {
                fd_count = 1,
                fd_array = socket
            };

            return handle;
        }
    }
}
