namespace SoftFX.AutomaticTrading.Core.Export
{
    using System;
    using System.IO;
    using System.Linq;
    using SoftFX.AutomaticTrading.Core.Indicators;

    /// <summary>
    /// Provides functionality for exporting indicator results.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class IndicatorResultsExporter<TInput, TResult>
    {
        readonly IIndicatorResults<TInput, TResult> indicator;

        public IndicatorResultsExporter(IIndicatorResults<TInput, TResult> indicator)
        {
            this.indicator = indicator;
        }

        /// <summary>
        /// Exports indicator results to file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="exporter">Exporter</param>
        public void Export(string fileName, IExporter<TInput, TResult> exporter)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                this.Export(stream, exporter);
            }
        }

        /// <summary>
        /// Exports indicator results to stream.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="exporter">Exporter.</param>
        public void Export(Stream stream, IExporter<TInput, TResult> exporter)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (exporter == null)
                throw new ArgumentException("exporter");

            exporter.BeginDocument(stream);

            foreach (var result in this.indicator.History.Reverse())
            {
                exporter.WriteRow(stream, result);
            }

            exporter.EndDocument(stream);
        }
    }
}
