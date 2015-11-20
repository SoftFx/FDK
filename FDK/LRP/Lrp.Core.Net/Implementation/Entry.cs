namespace SoftFX.Lrp.Implementation
{
    using System;
    using System.Threading;

    struct Entry
    {
        public Entry(MemoryBuffer buffer)
            : this()
        {
            this.buffer = buffer;
            this.syncEvent = new AutoResetEvent(false);
        }

        public void Swap(MemoryBuffer buffer)
        {
            MemoryBuffer.Swap(this.buffer, buffer);
        }

        public bool WaitFor(Int32 timeoutInMilliseconds)
        {
            return this.syncEvent.WaitOne(timeoutInMilliseconds);
        }

        public void WakeUp()
        {
            this.syncEvent.Set();
        }

        public void Dispose()
        {
            this.syncEvent.Close();
        }

        #region Fields

        MemoryBuffer buffer;
        AutoResetEvent syncEvent;

        #endregion
    }
}
