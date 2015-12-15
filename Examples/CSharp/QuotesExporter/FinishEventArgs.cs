namespace QuotesCsvExporter
{
    using System;

    class FinishEventArgs : EventArgs
    {
        public FinishEventArgs(bool status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public bool Status { get; private set; }
        public string Message { get; private set; }
    }
}
