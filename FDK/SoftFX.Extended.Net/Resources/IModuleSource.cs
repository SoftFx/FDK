namespace SoftFX.Extended.Resources
{
    interface IModuleSource
    {
        string Name { get; }
        byte[] Data { get; }
    }
}
