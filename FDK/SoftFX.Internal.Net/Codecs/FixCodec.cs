namespace SoftFX.Internal.Codecs
{
    public class FixCodec : Codec
    {
        public FixCodec()
            : base(new FixCodecProxyAdapter())
        {
        }
    }
 }
