namespace SoftFX.Lrp
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LogonHandler(object sender, EventArgs e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LogoutHandler(object sender, EventArgs e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public delegate void LogHandler(string message);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Channel"></typeparam>
    /// <param name="offset"></param>
    /// <param name="buffer"></param>
    /// <param name="channel"></param>
    public delegate void MethodHandler<Channel>(int offset, MemoryBuffer buffer, Channel channel);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Channel"></typeparam>
    /// <param name="offset"></param>
    /// <param name="methodId"></param>
    /// <param name="buffer"></param>
    /// <param name="channel"></param>
    /// <returns></returns>
    public delegate int ComponentHandler<Channel>(int offset, int methodId, MemoryBuffer buffer, Channel channel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="componentId"></param>
    /// <param name="methodId"></param>
    /// <param name="heap"></param>
    /// <param name="pSize"></param>
    /// <param name="ppData"></param>
    /// <param name="pCapacity"></param>
    /// <returns></returns>
    public unsafe delegate int LocalServerInvokeHandler(ushort componentId, ushort methodId, void* heap, int* pSize, void** ppData, int* pCapacity);
}

namespace SoftFX.Lrp
{
    delegate void Empty();
    unsafe delegate void CallBackFunc(void* signature, void* invoke, void* pParam);
}
