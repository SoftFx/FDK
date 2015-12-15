namespace SoftFX.Lrp.Implementation
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ComponentMethodSize
    {
        public ushort ComponentId;
        public ushort MethodId;
        public int Size;

        public ComponentMethodSize(ushort componentId, ushort methodId, int size)
            : this()
        {
            this.ComponentId = componentId;
            this.MethodId = methodId;
            this.Size = size;
        }
    }
}
