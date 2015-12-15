namespace QuotesCsvExporter
{
    using System;

    class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(int value)
        {
            this.Value = value;
        }

        public int Value { get; private set; }
    }
}
