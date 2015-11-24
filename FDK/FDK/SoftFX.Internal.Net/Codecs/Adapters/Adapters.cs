namespace SoftFX.Internal.Codecs
{
    using System;
    using SoftFX.Extended;
    using SoftFX.Internal.Generated.FixProvider;
    using SoftFX.Internal.Generated.LrpProvider;

    sealed class FixCodecProxyAdapter : FixCodecProxy, ICodecProxy
    {
        public FixCodecProxyAdapter()
            : base(Native.FixProvider)
        {
        }

        public void EncodeRaw(Extended.Quote[] quotes)
        {
            throw new NotSupportedException();
        }

        public void EncodeFast(uint precision, double volumeStep, Quote[] quotes)
        {
            throw new NotSupportedException();
        }
    }

    sealed class LrpCodecProxyAdapter : LrpCodecProxy, ICodecProxy
    {
        public LrpCodecProxyAdapter()
            : base(Native.LrpProvider)
        {
        }

        public void EncodeFast(Quote[] quotes)
        {
            throw new NotSupportedException();
        }
    }
}
