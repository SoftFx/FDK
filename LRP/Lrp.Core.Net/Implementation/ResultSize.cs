namespace SoftFX.Lrp.Implementation
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ResultSize
    {
        public int Result;
        public int Size;
    }
}
