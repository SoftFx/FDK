namespace SoftFX.Extended.Core
{
    using System;
    using SoftFX.Lrp;

    unsafe struct FxQueue : IDisposable
    {
        public FxQueue(LPtr handle)
            : this()
        {
            if (handle == LPtr.Zero)
                throw new ArgumentNullException(nameof(handle), "Queue handle can not be null");

            this.handle = handle;
        }

        public void Dispose()
        {
            Native.Handle.Delete(this.handle);
        }

        #region Members

        readonly LPtr handle;

        #endregion
    }
}
