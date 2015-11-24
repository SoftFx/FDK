namespace SoftFX.Lrp
{
    /// <summary>
    /// Local/Remote Protocol throws the exception, if an invalid method ID is detected.
    /// </summary>
    public class InvalidMethodException : ProtocolException
    {
        /// <summary>
        /// Creates a new instance of InvalidComponentException class.
        /// </summary>
        /// <param name="componentName">name of component</param>
        /// <param name="methodName">name of invalid method</param>
        /// <param name="componentId">a component ID.</param>
        /// <param name="methodId">an invalid method ID.</param>
        public InvalidMethodException(string componentName, string methodName, int componentId, int methodId)
        {
            this.ComponentName = componentName;
            this.MethodName = methodName;
            this.ComponentId = componentId;
            this.MethodId = methodId;
        }

        /// <summary>
        /// Gets component name.
        /// </summary>
        public string ComponentName { get; private set; }

        /// <summary>
        /// Gets name of invalid com
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// Gets component ID.
        /// </summary>
        public int ComponentId { get; private set; }

        /// <summary>
        /// Gets an invalid method ID.
        /// </summary>
        public int MethodId { get; private set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            var result = string.Format("Invalid method = {0}/{1} for componet = {2}/{3}", this.MethodName, this.MethodId, this.ComponentName, this.ComponentId);
            return result;
        }
    }
}
