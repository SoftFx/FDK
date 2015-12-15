namespace SoftFX.Lrp
{
    //using System;
    //using System.Collections.Generic;
    //using System.Text;

    ///// <summary>
    ///// Generic interface for all client implementations.
    ///// </summary>
    //public interface IRemoteClient : IDisposable
    //{
    //	/// <summary>
    //	/// A client implementation should pass the buffer to another environment and receive the answer.
    //	/// </summary>
    //	/// <param name="data">
    //	/// The first 8 bytes contains positive UInt64 unique ID.
    //	/// The parameter can not be null.
    //	/// </param>
    //	/// <param name="timeoutInMillisecond">
    //	/// Negative value means endless waiting.
    //	/// </param>
    //	int Invoke(MemoryBuffer data, Int32 timeoutInMillisecond);
    //	/// <summary>
    //	/// The method checks, if the corresponded component and method are supported by server.
    //	/// You should not use the method directly.
    //	/// </summary>
    //	/// <param name="componentId">a component ID</param>
    //	/// <param name="methodId">a method ID</param>
    //	/// <returns>true, if the component and method are supported by server, otherwise false</returns>
    //	bool IsSupported(ushort componentId, ushort methodId);
    //	/// <summary>
    //	/// The method checks, if the corresponded component is supported by server.
    //	/// You should not use the method directly.
    //	/// </summary>
    //	/// <param name="componentId">a component ID</param>
    //	/// <returns>true, if the component is supported by server, otherwise false</returns>
    //	bool IsSupported(ushort componentId);
    //}
}
