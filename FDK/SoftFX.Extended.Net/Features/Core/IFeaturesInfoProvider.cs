namespace SoftFX.Extended
{
    /// <summary>
    /// Defines method for obtaining features information for specific protocol version.
    /// </summary>
    /// <typeparam name="TInfo"></typeparam>
    public interface IFeaturesInfoProvider<TInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocolVersion"></param>
        /// <returns></returns>
        TInfo GetInfo(FixProtocolVersion protocolVersion);
    }
}
