using System.Runtime.InteropServices;

namespace Mql2Fdk.EncodingTools.Multilang
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct _ULARGE_INTEGER
    {
        public ulong QuadPart;
    }
}