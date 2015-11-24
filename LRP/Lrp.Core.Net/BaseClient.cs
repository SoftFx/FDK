namespace SoftFX.Lrp
{
    using System;
    using System.Collections.Generic;
    using SoftFX.Lrp.Implementation;

    /// <summary>
    /// Contains command methods of all client types.
    /// </summary>
    public abstract class BaseClient : IClient, IDisposable
    {
        internal BaseClient(string localSignature)
        {
            Translator[] translators;
            this.exceptions = ParseSignature(localSignature, out translators);
            this.translators = translators;
        }

        internal void Initialize(string remoteSignature)
        {
            Translator[] translators;
            var exceptions = ParseSignature(remoteSignature, out translators);
            this.exceptions.MatchExceptions(exceptions);

            var dict = new Dictionary<string, Translator>();
            foreach (var element in translators)
            {
                dict[element.Name] = element;
            }

            foreach (var element in this.translators)
            {
                Translator translator;
                dict.TryGetValue(element.Name, out translator);
                element.MatchMethods(translator);
            }
        }

        internal static Translator ParseSignature(string singnature, out Translator[] translators)
        {
            var separators = new char[] {'$'};
            var lines = singnature.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(singnature, "Signature contains too much components");

            var result = new Translator(lines[0]);
            translators = new Translator[lines.Length - 1];
            for (var index = 1; index < lines.Length; ++index)
            {
                translators[index - 1] = new Translator((ushort)(index - 1), lines[index]);
            }
            return result;
        }

        internal void Translate(ref ushort componetId, ref ushort methodId)
        {
            this.translators[componetId].Translate(ref componetId, ref methodId);
        }

        /// <summary>
        /// Has no implementation due to abstract class
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract MemoryBuffer Create();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentId"></param>
        /// <param name="methodId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract int Invoke(ushort componentId, ushort methodId, MemoryBuffer data);

        /// <summary>
        /// The method checks, if the corresponded component and method are supported by server.
        /// You should not use the method directly.
        /// </summary>
        /// <param name="componentId">a component ID</param>
        /// <param name="methodId">a method ID</param>
        /// <returns>true, if the component and method are supported by server, otherwise false</returns>
        public bool IsSupported(ushort componentId, ushort methodId)
        {
            if (componentId >= this.translators.Length)
                return false;

            var translator = this.translators[componentId];
            return translator.IsSupported(methodId);
        }

        /// <summary>
        /// The method checks, if the corresponded component is supported by server.
        /// You should not use the method directly.
        /// </summary>
        /// <param name="componentId">a component ID</param>
        /// <returns>true, if the component is supported by server, otherwise false</returns>
        public bool IsSupported(ushort componentId)
        {
            if (componentId >= this.translators.Length)
                return false;

            var translator = this.translators[componentId];
            return translator.IsSupported();
        }

        internal void FlushTranlators(Logger logger)
        {
            logger.Output("Flushing of translators");
            foreach (var element in this.translators)
            {
                element.FlushMethod(logger);
            }
            logger.Output("Translators have been flushed");
        }

        #region Fields

        readonly Translator exceptions;
        readonly Translator[] translators;

        #endregion
    }
}
