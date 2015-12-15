namespace SoftFX.Internal.Codecs
{
    using System;
    using System.Globalization;
    using SoftFX.Extended;

    public abstract class Codec : IDisposable
    {
        #region Members

        ICodecProxy proxy;

        #endregion

        #region Construction and Destruction

        internal Codec(ICodecProxy proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException(nameof(proxy));

            this.proxy = proxy;
        }

        public void Dispose()
        {
            if (this.proxy == null)
                return;

            this.proxy.Dispose();
            this.proxy = null;
        }

        #endregion

        #region Properties

        public long Size
        {
            get
            {
                return this.proxy.GetSize();
            }
        }

        public long Count
        {
            get
            {
                return this.proxy.GetCount();
            }
        }

        public double Time
        {
            get
            {
                return this.proxy.GetTime();
            }
        }

        #endregion

        #region Methods

        public void EncodeRaw(Quote[] quotes)
        {
            this.proxy.EncodeRaw(quotes);
        }

        public void EncodeSlow(Quote[] quotes)
        {
            this.proxy.EncodeSlow(quotes);
        }

        public void EncodeFast(Quote[] quotes)
        {
            this.proxy.EncodeFast(quotes);
        }

        public void EncodeFast(int precision, double volumeStep, Quote[] quotes)
        {
            this.proxy.EncodeFast((uint)precision, volumeStep, quotes);
        }

        public void Clear()
        {
            this.proxy.Clear();
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            if (this.proxy == null)
                return base.ToString();

            var count = proxy.GetCount();
            var size = proxy.GetSize();
            var time = proxy.GetTime();
            var speed = "N/A";

            if (time > 0)
                speed = (count / time).ToString(CultureInfo.InvariantCulture);

            var result = string.Format("Count = {0}; Size = {1}; Time = {2}; Speed = {3}", count.ToString(), size.ToString(), time.ToString(), speed);
            return result;
        }

        #endregion
    }
}
