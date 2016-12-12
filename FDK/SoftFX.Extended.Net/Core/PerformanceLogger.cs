using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace SoftFX.Extended.Core.Performance
{
    internal class Service : IDisposable
    {
        public Service()
        {
            started_ = false;
        }
        public Service(int capacity)
        {
            started_ = false;

            Start(capacity);
        }

        void Start(int capacity)
        {
            if (started_)
                throw new Exception("Performance service is already started");

            timestampList_ = new List<TimestampItem>(32);

            if (!QueryPerformanceFrequency(out frequency_))
                throw new Exception("Could not get high resolution timer frequency");

            mutex_ = new Mutex();

            try
            {
                event_ = new AutoResetEvent(false);

                try
                {
                    stop_ = false;
                    thread_ = new Thread(new ThreadStart(this.Process));
                    thread_.Start();

                    started_ = true;
                }
                catch
                {
                    event_.Dispose();

                    throw;
                }
            }
            catch
            {
                mutex_.Dispose();

                throw;
            }
        }

        public virtual void Dispose()
        {
            if (started_)
            {
                started_ = false;

                using (mutex_)
                {
                    stop_ = true;
                    event_.Set();
                }

                thread_.Join();

                event_.Dispose();

                mutex_.Dispose();
            }
        }
        void Process()
        {
            try
            {
                List<TimestampItem> timestampList = new List<TimestampItem>(32);
                bool stop;

                while (true)
                {
                    event_.WaitOne();

                    timestampList.Clear();

                    using (mutex_)
                    {
                        List<TimestampItem> tempTimestampList = timestampList_;
                        timestampList_ = timestampList;
                        timestampList = tempTimestampList;

                        stop = stop_;
                    }

                    for (int index = 0; index < timestampList.Count; ++index)
                    {
                        TimestampItem timestampItem = timestampList[index];

                        if (timestampItem.type == TimestampItemType.Log)
                        {
                            timestampItem.logger.streamWriter_.WriteLine("{0};{1};{2}", timestampItem.id, timestampItem.timestamp, timestampItem.memo);
                            timestampItem.logger.streamWriter_.Flush();
                        }
                        else if (timestampItem.type == TimestampItemType.Signal)
                        {
                            timestampItem.logger.event_.Set();
                        }
                    }

                    if (stop)
                        break;
                }
            }
            catch
            {
            }
        }

        internal enum TimestampItemType
        {
            Log,
            Signal
        }

        internal struct TimestampItem
        {
            public TimestampItemType type;
            public Logger logger;
            public string id;
            public ulong timestamp;
            public string memo;
        }

        [DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceFrequency(out ulong frequency);

        bool started_;

        internal ulong frequency_;

        internal List<TimestampItem> timestampList_;
        internal Mutex mutex_;
        internal AutoResetEvent event_;
        bool stop_;
        Thread thread_;
    }

    internal class Logger : IDisposable
    {
        public Logger(Service service)
        {
            service_ = service;
            opened_ = false;
        }

        public Logger(Service service, string name, string description, string logDirectory)
        {
            service_ = service;
            opened_ = false;

            Open(name, description, logDirectory);
        }

        public bool Opened()
        {
            return opened_;
        }

        public void Open(string name, string description, string logDirectory)
        {
            if (opened_)
                throw new Exception("Performance logger is already opened");

            name_ = name;

            event_ = new AutoResetEvent(false);

            try
            {
                string path = logDirectory + "\\" + name + ".log";
                streamWriter_ = new StreamWriter(path, false);

                try
                {
                    streamWriter_.WriteLine(description);
                    streamWriter_.Flush();

                    opened_ = true;
                }
                catch
                {
                    streamWriter_.Dispose();

                    throw;
                }
            }
            catch
            {
                event_.Dispose();

                throw;
            }
        }

        public void Dispose()
        {
            if (opened_)
            {
                opened_ = false;

                using (service_.mutex_)
                {
                    Service.TimestampItem timestampItem = new Service.TimestampItem();
                    timestampItem.type = Service.TimestampItemType.Signal;
                    timestampItem.logger = this;

                    service_.timestampList_.Add(timestampItem);

                    service_.event_.Set();
                }

                event_.WaitOne();

                streamWriter_.Dispose();

                event_.Dispose();
            }
        }

        public void LogTimestamp(string id, ulong timestamp, string memo)
        {
            using (service_.mutex_)
            {
                if (service_.timestampList_.Count < 10000)
                {
                    Service.TimestampItem timestampItem = new Service.TimestampItem();
                    timestampItem.type = Service.TimestampItemType.Log;
                    timestampItem.logger = this;
                    timestampItem.id = id;
                    timestampItem.timestamp = timestamp;
                    timestampItem.memo = memo;

                    service_.timestampList_.Add(timestampItem);

                    service_.event_.Set();
                }
            }
        }

        public ulong GetTimestamp()
        {
            ulong counter;
            QueryPerformanceCounter(out counter);

            return (ulong) ((double) counter * 1000000 / service_.frequency_);
        }

        [DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceCounter(out ulong сounter);

        Service service_;

        string name_;
        bool opened_;

        internal AutoResetEvent event_;
        internal StreamWriter streamWriter_;
    }
}
