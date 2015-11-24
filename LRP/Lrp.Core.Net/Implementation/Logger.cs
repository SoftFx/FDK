namespace SoftFX.Lrp.Implementation
{
    class Logger
    {
        #region Construction

        public Logger(LogHandler logHandler)
        {
            this.logHandler = logHandler;
        }

        #endregion

        #region Output Methods

        public void Output(string message)
        {
            if (this.logHandler != null)
            {
                this.logHandler(message);
            }
        }

        public void Output<P0>(string format, P0 a0)
        {
            if (this.logHandler != null)
            {
                var message = string.Format(format, a0);
                this.logHandler(message);
            }
        }

        public void Output<P0, P1>(string format, P0 a0, P1 a1)
        {
            if (this.logHandler != null)
            {
                var message = string.Format(format, a0, a1);
                this.logHandler(message);
            }
        }

        public void Output<P0, P1, P2>(string format, P0 a0, P1 a1, P2 a2)
        {
            if (this.logHandler != null)
            {
                var message = string.Format(format, a0, a1, a2);
                this.logHandler(message);
            }
        }

        public void Output<P0, P1, P2, P3>(string format, P0 a0, P1 a1, P2 a2, P3 a3)
        {
            if (this.logHandler != null)
            {
                var message = string.Format(format, a0, a1, a2, a3);
                this.logHandler(message);
            }
        }

        public void Output<P0, P1, P2, P3, P4>(string format, P0 a0, P1 a1, P2 a2, P3 a3, P4 a4)
        {
            if (this.logHandler != null)
            {
                var message = string.Format(format, a0, a1, a2, a3, a4);
                this.logHandler(message);
            }
        }

        #endregion

        #region Fields

        readonly LogHandler logHandler;

        #endregion
    }
}
