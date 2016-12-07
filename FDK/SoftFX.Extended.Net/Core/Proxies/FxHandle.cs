namespace SoftFX.Extended.Core
{
    using System;
    using SoftFX.Lrp;

    unsafe struct FxHandle
    {
        public FxHandle(LPtr handle)
        {
            this.handle = handle;
        }

        public void Delete()
        {
            Native.Handle.Delete(this.handle);
            this.handle = LPtr.Zero;
        }

        public LPtr Pointer
        {
            get
            {
                return this.handle;
            }
        }

        public bool IsNull
        {
            get
            {
                return handle != LPtr.Zero;
            }
        }

        LPtr handle;
    }
}
