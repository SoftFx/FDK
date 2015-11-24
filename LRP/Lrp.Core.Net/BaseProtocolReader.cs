namespace SoftFX.Lrp
{
    using System;
    using SoftFX.Lrp.Implementation;

    /// <summary>
    /// Implements IProtocolReader interface, supports matching components for different protocols.
    /// </summary>
    public abstract class BaseProtocolReader : IProtocolReader
    {
        /// <summary>
        /// Parses a text stream.
        /// </summary>
        /// <param name="stream">a text stream for parsing</param>
        public void Parse(TextStream stream)
        {
            stream.ValidateVerbatimText('[');
            var componentId = stream.ReadInt32();
            stream.ValidateVerbatimText(']');
            var componentName = stream.ReadAStringForCharacter('[');
            if (string.IsNullOrEmpty(componentName))
            {
                var message = string.Format("Invalid component name = {0}", componentName);
                throw new ArgumentException(message);
            }
            var component = this.components.Find(componentId, componentName);

            if (component != null)
                component.Parse(stream);
        }

        /// <summary>
        /// The method adds a new component reader.
        /// </summary>
        /// <param name="name">the name of adding component</param>
        /// <param name="component">an adding component</param>
        protected void Add(string name, IComponentReader component)
        {
            this.components.Add(name, component);
        }

        #region Fields

        readonly Matching<IComponentReader> components = new Matching<IComponentReader>();

        #endregion
    }
}
