using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FdkRHost.Logging
{
    internal class UniversalDateTime : RollingFileAppender.IDateTime
    {
        /// <summary>
        /// Gets the <b>current</b> time.
        /// </summary>
        /// <value>The <b>current</b> time.</value>
        /// <remarks>
        /// <para>
        /// Gets the <b>current</b> time.
        /// </para>
        /// </remarks>
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }
    }
    /// <summary>
    /// Asynchronous forwarding log appender.
    /// </summary>
    public class AsyncForwardingAppender : ForwardingAppender
    {
        // Limit cache size.
        static readonly int MAXCACHESIZE = 10000;

        private Thread _asyncFlushThread;
        private BlockingCollection<object> _cache = new BlockingCollection<object>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public AsyncForwardingAppender()
        {
            // Start flush thread.
            _asyncFlushThread = new Thread(new ThreadStart(FlushThread))
            {
                Name = "Logging",
                Priority = ThreadPriority.Lowest,
                IsBackground = true
            };
            _asyncFlushThread.Start();
        }

        /// <summary>
        /// Override OnClose() method.
        /// </summary>
        protected override void OnClose()
        {
            _cache.CompleteAdding();
            _asyncFlushThread.Join();
            base.OnClose();
        }

        private void FlushThread()
        {
            foreach (var @event in _cache.GetConsumingEnumerable())
            {
                var loggingEvent = @event as LoggingEvent;
                if (loggingEvent != null)
                {
                    base.Append(loggingEvent);
                    continue;
                }

                var loggingEvents = @event as LoggingEvent[];
                if (loggingEvents != null)
                {
                    base.Append(loggingEvents);
                }
            }
        }

        /// <summary>
        /// Override Append() method.
        /// </summary>
        protected override void Append(LoggingEvent loggingEvent)
        {
            // Limit cache size.
            if (_cache.Count >= MAXCACHESIZE)
                return;

            loggingEvent.Properties["ThreadId"] = Thread.CurrentThread.ManagedThreadId;
            _cache.Add(loggingEvent);
        }

        /// <summary>
        /// Override Append() method.
        /// </summary>
        protected override void Append(LoggingEvent[] loggingEvents)
        {
            // Limit cache size.
            if (_cache.Count >= MAXCACHESIZE)
                return;

            foreach (LoggingEvent loggingEvent in loggingEvents)
                loggingEvent.Properties["ThreadId"] = Thread.CurrentThread.ManagedThreadId;
            _cache.Add(loggingEvents);
        }
    }
}
