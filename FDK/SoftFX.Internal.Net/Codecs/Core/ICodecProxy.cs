namespace SoftFX.Internal.Codecs
{
    using System;
    using SoftFX.Extended;

    interface ICodecProxy : IDisposable
    {
        long GetSize();
        long GetCount();
        double GetTime();
        void EncodeRaw(Quote[] quotes);
        void EncodeSlow(Quote[] quotes);
        void EncodeFast(Quote[] quotes);
        void EncodeFast(uint precision, double volumeStep, Quote[] quotes);
        void Clear();
    }
}
