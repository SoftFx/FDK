using System.Runtime.InteropServices;

namespace Mql2Fdk.EncodingTools.Multilang
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct tagUNICODERANGE
    {
        public ushort wcFrom;
        public ushort wcTo;
    }
}