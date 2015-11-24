namespace SoftFX.Lrp
{
    using System;
    using SoftFX.Lrp.Implementation;

    /// <summary>
    /// Implements IComponentReader interface, supports matching methods for different protocols.
    /// </summary>
    public class BaseComponentReader : IComponentReader
    {
        /// <summary>
        /// Parses a text stream.
        /// </summary>
        /// <param name="stream">a text stream for parsing</param>
        public void Parse(TextStream stream)
        {
            stream.ValidateVerbatimText('[');
            var methodId = stream.ReadInt32();
            stream.ValidateVerbatimText(']');
            var methodName = stream.ReadAStringForCharacter('(');
            if (string.IsNullOrEmpty(methodName))
            {
                var message = string.Format("Invalid method name = {0}", methodName);
                throw new ArgumentException(message);
            }
            var method = this.methods.Find(methodId, methodName);
            if (method != null)
                method(stream);
        }

        /// <summary>
        /// The method adds a new method reader.
        /// </summary>
        /// <param name="name">the name of adding method</param>
        /// <param name="method">an adding method</param>
        protected void Add(string name, Action<TextStream> method)
        {
            this.methods.Add(name, method);
        }

        #region Fields

        readonly Matching<Action<TextStream>> methods = new Matching<Action<TextStream>>();

        #endregion
    }
}
