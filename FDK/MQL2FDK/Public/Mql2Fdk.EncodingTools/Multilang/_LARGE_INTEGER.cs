using System.Runtime.InteropServices;

namespace Mql2Fdk.EncodingTools.Multilang
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct _LARGE_INTEGER
    {
        public long QuadPart;
    }
}