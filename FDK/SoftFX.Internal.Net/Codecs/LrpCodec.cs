namespace SoftFX.Internal.Codecs
{
    public class LrpCodec : Codec
    {
        public LrpCodec()
            : base(new LrpCodecProxyAdapter())
        {
        }
    }
}
