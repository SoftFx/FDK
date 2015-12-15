namespace SoftFX.AutomaticTrading.Core.Export
{
    using System.IO;
    using SoftFX.AutomaticTrading.Core.Indicators;

    /// <summary>
    /// Defines methods for indicator exporter.
    /// </summary>
    /// <typeparam name="TInput">Input type.</typeparam>
    /// <typeparam name="TResult">Result type.</typeparam>
    public interface IExporter<TInput, TResult>
    {
        /// <summary>
        /// Writes prolog of new document.
        /// </summary>
        /// <param name="stream">Stream.</param>
        void BeginDocument(Stream stream);

        /// <summary>
        /// Writes result row.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="result">Result.</param>
        void WriteRow(Stream stream, IResultContainer<TInput, TResult> result);

        /// <summary>
        /// Writes epilog of document.
        /// </summary>
        /// <param name="stream">Stream.</param>
        void EndDocument(Stream stream);
    }
}
