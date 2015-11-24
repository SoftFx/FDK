namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    struct timeval
    {
        /// <summary>
        /// Time interval, in seconds.
        /// </summary>
        public int tv_sec;

        /// <summary>
        /// Time interval, in microseconds.
        /// </summary>
        public int tv_usec;
    };
}
