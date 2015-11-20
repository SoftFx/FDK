namespace SoftFX.Lrp
{
    /// <summary>
    /// Local/Remote Protocol throws the exception, if an invalid component ID is detected.
    /// </summary>
    public class InvalidComponentException : ProtocolException
    {
        /// <summary>
        /// Creates a new instance of InvalidComponentException class.
        /// </summary>
        /// <param name="componentName">name of invalid component</param>
        /// <param name="componentId">an invalid component ID.</param>
        public InvalidComponentException(string componentName, int componentId)
        {
            this.ComponentName = componentName;
            this.ComponentId = componentId;
        }

        /// <summary>
        /// Gets name of invalid component.
        /// </summary>
        public string ComponentName { get; private set; }

        /// <summary>
        /// Gets invalid component ID.
        /// </summary>
        public int ComponentId { get; private set; }

        /// <summary>
        /// Returns formatted string for the class instance.
        /// </summary>
        /// <returns>Can not be null.</returns>
        public override string ToString()
        {
            var result = string.Format("Invalid component = {0}/{1}", this.ComponentName, this.ComponentId);
            return result;
        }
    }
}
