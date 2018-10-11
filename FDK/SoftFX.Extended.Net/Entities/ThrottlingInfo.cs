namespace SoftFX.Extended
{
    /// <summary>
    /// This class contains the information about throttling limits for methods.
    /// </summary>
    public class ThrottlingMethodInfo
    {
        /// <summary>
        /// Gets or sets method's name.
        /// </summary>
        public ThrottlingMethod Method { get; set; }

        /// <summary>
        /// Gets or sets  the allowed amount of requests per second for the particular method.
        /// </summary>
        public int RequestsPerSecond { get; set; }

        /// <summary>
        /// Creates a new empty instance of ThrottlingMethodInfo.
        /// </summary>
        public ThrottlingMethodInfo()
        {
        }
    }
}
