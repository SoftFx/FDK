using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace SoftFX.Extended.Core.Performance
{
    internal class Logger : IDisposable
    {
        public Logger()
        {
            opened_ = false;
        }

        public Logger(string name, string description, string logDirectory)
        {
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

            timestampList_ = new List<TimestampItem>(32);

            if (! QueryPerformanceFrequency(out frequency_))
                throw new Exception("Could not get high resolution timer frequency");

            mutex_ = new Mutex();

            try
            {
                event_ = new AutoResetEvent(false);

                try
                {
                    string path = logDirectory + "\\" + name + ".log";
                    streamWriter_ = new StreamWriter(path, false);

                    try
                    {
                        streamWriter_.WriteLine(description);
                        streamWriter_.Flush();

                        stop_ = false;
                        thread_ = new Thread(new ThreadStart(this.Process));
                        thread_.Start();

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
            catch
            {
                mutex_.Dispose();

                throw;
            }
        }

        public void Dispose()
        {
            if (opened_)
            {
                opened_ = false;

                using (mutex_)
                {
                    stop_ = true;
                    event_.Set();
                }

                thread_.Join();

                streamWriter_.Dispose();
                
                event_.Dispose();

                mutex_.Dispose();
            }
        }

        public void LogTimestamp(string id, ulong timestamp, string memo)
        {
            using (mutex_)
            {
                if (timestampList_.Count < 10000)
                {
                    TimestampItem timestampItem = new TimestampItem();
                    timestampItem.id = id;
                    timestampItem.timestamp = timestamp;
                    timestampItem.memo = memo;

                    timestampList_.Add(timestampItem);

                    event_.Set();
                }
            }
        }

        public ulong GetTimestamp()
        {
            ulong counter;
            QueryPerformanceCounter(out counter);

            return counter * 1000000 / frequency_;
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

                    for (int index = 0; index < timestampList.Count; ++ index)
                    {
                        TimestampItem timestampItem = timestampList[index];

                        streamWriter_.WriteLine("{0};{1};{2}", timestampItem.id, timestampItem.timestamp, timestampItem.memo);                        
                    }

                    streamWriter_.Flush();

                    if (stop)
                        break;
                }
            }
            catch
            {
            }
        }
        struct TimestampItem
        {
            public string id;
            public ulong timestamp;
            public string memo;
        }

        [DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceFrequency(out ulong frequency);

        [DllImport("Kernel32.dll")]
        static extern bool QueryPerformanceCounter(out ulong сounter);

        string name_;

        bool opened_;
        ulong frequency_;

        List<TimestampItem> timestampList_;
        Mutex mutex_;
        AutoResetEvent event_;

        StreamWriter streamWriter_;

        bool stop_;
        Thread thread_;
    }
}
