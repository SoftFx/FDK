namespace SoftFX.Lrp.Implementation
{
    using System;

    struct Timeout
    {
        public Timeout(int timeoutInMilliseconds)
            : this()
        {
            this.timeoutInTicks = timeoutInMilliseconds;
            this.timeoutInTicks *= 10000;
            this.initialTime = DateTime.UtcNow;
        }

        internal timeval ToTime()
        {
            var now = DateTime.UtcNow;
            var interval = (this.initialTime - now).Ticks + this.timeoutInTicks;
            var result = new timeval();
            if (interval > 0)
            {
                result.tv_sec = (int)(interval / 10000000);
                result.tv_usec = ((int)(interval % 10000000)) / 10;
            }
            return result;
        }

        #region Fields

        long timeoutInTicks;
        DateTime initialTime;

        #endregion
    }
}
