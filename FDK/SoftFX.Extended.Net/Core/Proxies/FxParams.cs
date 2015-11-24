namespace SoftFX.Extended.Core
{
    using System;
    using SoftFX.Lrp;

    class FxParams : IDisposable
    {
        #region Construction

        public FxParams()
        {
            this.handle = Native.Params.Create();
        }

        public void Dispose()
        {
            if (this.handle != LPtr.Zero)
            {
                Native.Handle.Delete(this.handle);
                this.handle = LPtr.Zero;
            }
        }

        ~FxParams()
        {
            this.Dispose();
        }

        #endregion

        public void SetString(string key, string value)
        {
            Native.Params.SetString(this.handle, key, value);
        }

        public void SetBool(string key, bool value)
        {
            Native.Params.SetBoolean(this.handle, key, value);
        }

        public void SetInt32(string key, int value)
        {
            Native.Params.SetInt32(this.handle, key, value);
        }

        public void SetReal(string key, double value)
        {
            Native.Params.SetDouble(this.handle, key, value);
        }

        public override string ToString()
        {
            var result = Native.Params.ToText(this.handle);
            return result;
        }

        #region Members

        LPtr handle;

        #endregion
    }
}
