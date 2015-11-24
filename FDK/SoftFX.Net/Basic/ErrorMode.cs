namespace SoftFX.Basic
{
    /// <summary>
    /// Describes possible cases of exceptions processing, which have been thrown within user event handlers.
    /// </summary>
    public enum ErrorMode
    {
        /// <summary>
        /// Throw mode if debugger is attached, otherwise Silent mode.
        /// </summary>
        Default,

        /// <summary>
        /// Manager class doesn't catch exception within user event handlers. In this case program will crash, if an exception is encountered
        /// </summary>
        Throw,

        /// <summary>
        /// Manager class catches any exception within user event handlers and raises a special Error event.
        /// </summary>
        Silent
    }
}
