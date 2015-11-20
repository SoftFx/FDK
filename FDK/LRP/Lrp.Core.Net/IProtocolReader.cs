namespace SoftFX.Lrp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProtocolReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        void Parse(TextStream stream);
    }
}
