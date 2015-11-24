namespace SoftFX.Lrp.Implementation
{
    using System;

    unsafe interface IHost : IDisposable
    {
        string Signature();

        MemoryBuffer CreateLocal();

        int Invoke(UInt16 componentId, UInt16 methodId, MemoryBuffer buffer);
    }
}
