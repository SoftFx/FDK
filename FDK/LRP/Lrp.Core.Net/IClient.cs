namespace SoftFX.Lrp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        MemoryBuffer Create();

        /// <summary>
        /// A client implementation should pass the buffer to another environment and receive the answer.
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="methodId"></param>
        /// <param name="data">The parameter can not be null.</param>	
        /// <returns></returns>
        int Invoke(ushort componentId, ushort methodId, MemoryBuffer data);

        /// <summary>
        /// The method checks, if the corresponded component and method are supported by server.
        /// You should not use the method directly.
        /// </summary>
        /// <param name="componentId">a component ID</param>
        /// <param name="methodId">a method ID</param>
        /// <returns>true, if the component and method are supported by server, otherwise false</returns>
        bool IsSupported(ushort componentId, ushort methodId);

        /// <summary>
        /// The method checks, if the corresponded component is supported by server.
        /// You should not use the method directly.
        /// </summary>
        /// <param name="componentId">a component ID</param>
        /// <returns>true, if the component is supported by server, otherwise false</returns>
        bool IsSupported(ushort componentId);
    }
}
