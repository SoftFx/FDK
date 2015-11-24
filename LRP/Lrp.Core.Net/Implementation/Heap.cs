namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;

    unsafe class Heap : CriticalFinalizerObject
    {
        public Heap()
        {
            this.heap = HeapCreate(0, UIntPtr.Zero, UIntPtr.Zero);
        }

        ~Heap()
        {
            HeapDestroy(this.heap);
        }

        #region Properties

        public void* Handle
        {
            get
            {
                return this.heap.ToPointer();
            }
        }

        #endregion

        #region External Methods
        [DllImport("kernel32.dll", SetLastError=true)]
        static extern IntPtr HeapCreate(uint flOptions, UIntPtr dwInitialSize, UIntPtr dwMaximumSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool HeapDestroy(IntPtr hHeap);

        #endregion

        #region Fields

        readonly IntPtr heap;

        #endregion
    }
}
