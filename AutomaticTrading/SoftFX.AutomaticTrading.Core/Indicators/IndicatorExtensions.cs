namespace SoftFX.AutomaticTrading.Core.Indicators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Helper methods for indicators.
    /// </summary>
    public static class IndicatorExtensions
    {
        /// <summary>
        /// Runs source data set over specified indicator.
        /// </summary>
        /// <typeparam name="TValue">Input type.</typeparam>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="indicator">Indicator.</param>
        /// <param name="source">Data.</param>
        public static void Run<TValue, TResult>(this IIndicator<TValue, TResult> indicator, IEnumerable<TValue> source)
        {
            if (source == null)
                throw new ArgumentNullException("indicator");

            foreach (var item in source)
                indicator.Calculate(item);
        }

        /// <summary>
        /// Saves indicator.
        /// </summary>
        /// <param name="indicator">Indicator.</param>
        /// <param name="fileName">File name.</param>
        public static void Save(IIndicator indicator, string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                SaveCore(indicator, stream, StreamingContextStates.File);
            }
        }

        /// <summary>
        /// Saves indicator.
        /// </summary>
        /// <param name="indicator">Indicator.</param>
        /// <param name="stream">Stream. Will not be disposed.</param>
        public static void Save(IIndicator indicator, Stream stream)
        {
            SaveCore(indicator, stream);
        }

        static void SaveCore(IIndicator indicator, Stream stream, StreamingContextStates state = StreamingContextStates.All)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (!indicator.GetType().IsSerializable)
                throw new InvalidOperationException("In order support indicator saving its type must be marked with Serializable attribute.");

            var formatter = new BinaryFormatter
            {
                Context = new StreamingContext(state)
            };

            formatter.Serialize(stream, indicator);
        }

        /// <summary>
        /// Loads indicator.
        /// </summary>
        /// <typeparam name="TIndicator">Indicator type.</typeparam>
        /// <param name="fileName">File name.</param>
        /// <returns>Indicator instance.</returns>
        public static TIndicator Load<TIndicator>(string fileName)
            where TIndicator : IIndicator
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Load<TIndicator>(stream);
            }
        }

        /// <summary>
        /// Loads indicator.
        /// </summary>
        /// <typeparam name="TIndicator">Indicator type.</typeparam>
        /// <param name="stream">Stream.</param>
        /// <returns>Indicator instance.</returns>
        public static TIndicator Load<TIndicator>(Stream stream)
            where TIndicator : IIndicator
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (!typeof(TIndicator).IsSerializable)
                throw new InvalidOperationException("In order support indicator loading its type must be marked with Serializable attribute.");

            var formatter = new BinaryFormatter();
            return (TIndicator)formatter.Deserialize(stream);
        }
    }
}
