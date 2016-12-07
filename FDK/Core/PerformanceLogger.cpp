#include "stdafx.h"
#include "PerformanceLogger.h"

#include <string>

using namespace std;

namespace Performance
{
    Logger::Logger() : 
        opened_(false)
    {
    }

    Logger::Logger(const char* name, const char* description, const char* logDirectory) :
        opened_(false)
    {
        open(name, description, logDirectory);
    }

    Logger::~Logger()
    {
        close();
    }

    bool Logger::opened() const
    {
        return opened_;
    }

    void Logger::open(const char* name, const char* description, const char* logDirectory)
    {
        if (opened_)
            throw runtime_error("Performance logger is already opened");

        name_ = name;        

        timestampVector_.reserve(32);

        LARGE_INTEGER frequency;
        if (! QueryPerformanceFrequency(&frequency))
            throw runtime_error("Could not get high resolution timer frequency");

        timerFrequency_ = frequency.QuadPart;        

        InitializeCriticalSection(&cs_);

        try
        {
            event_ = CreateEvent(NULL, FALSE, FALSE, NULL);

            if (event_ == NULL)
                throw runtime_error("Could not create event object");

            try
            {                
                string path = string(logDirectory) + "\\" + name_ + ".log";
                file_ = _fsopen(path.c_str(), "w", _SH_DENYWR);

                if (file_ == NULL)
                    throw runtime_error("Could not open log file : " + path);

                try
                {
                    fprintf(file_, "%s\r\n", description);
                    fflush(file_);

                    stop_ = false;
                    thread_ = CreateThread(NULL, 0, thread, this, 0, NULL);

                    if (thread_ == NULL)
                        throw runtime_error("Could not start a thread");

                    opened_ = true;
                }
                catch (...)
                {
                    fclose(file_);                    

                    throw;
                }
            }
            catch (...)
            {
                CloseHandle(event_);

                throw;
            }
        }
        catch (...)
        {
            DeleteCriticalSection(&cs_);

            throw;
        }
    }

    void Logger::close()
    {
        if (opened_)
        {
            opened_ = false;

            EnterCriticalSection(&cs_);

            try
            {
                stop_ = true;
                SetEvent(event_);

                LeaveCriticalSection(&cs_);
            }
            catch (...)
            {
                LeaveCriticalSection(&cs_);
            }

            WaitForSingleObject(thread_, INFINITE);
            CloseHandle(thread_);

            fclose(file_);

            CloseHandle(event_);

            DeleteCriticalSection(&cs_);
        }
    }

    void Logger::logTimestamp(const char* id, uint64_t timestamp, const char* memo)
    {
        EnterCriticalSection(&cs_);

        try
        {
            if (timestampVector_.size() < 10000)
            {
                timestampVector_.resize(timestampVector_.size() + 1);
                TimestampItem& timestampItem = timestampVector_.back();

                strncpy(timestampItem.id, id, sizeof(timestampItem.id));
                timestampItem.timestamp = timestamp;
                strncpy(timestampItem.memo, memo, sizeof(timestampItem.memo));

                SetEvent(event_);
            }

            LeaveCriticalSection(&cs_);            
        }
        catch (...)
        {
            LeaveCriticalSection(&cs_);

            throw;
        }
    }

    uint64_t Logger::getTimestamp() const
    {
        LARGE_INTEGER counter;
        QueryPerformanceCounter(&counter);

        return (uint64_t) (counter.QuadPart * 1000000 / timerFrequency_);
    }
    
    DWORD WINAPI Logger::thread(LPVOID parameter)
    {
        try
        {
            Logger* logger = (Logger*) parameter;

            logger->process();

            return 0;
        }
        catch (...)
        {
            return 1;
        }
    }

    void Logger::process()
    {
        TimestampVector timestampVector;
        timestampVector.reserve(32);
        bool stop;

        while (true)
        {
            DWORD result = WaitForSingleObject(event_, INFINITE);

            if (result != WAIT_OBJECT_0)
                throw runtime_error("Could not wait for an event");

            timestampVector.clear();

            EnterCriticalSection(&cs_);

            try
            {
                timestampVector_.swap(timestampVector);
                stop = stop_;

                LeaveCriticalSection(&cs_);
            }
            catch (...)
            {
                LeaveCriticalSection(&cs_);

                throw;
            }

            for (size_t index = 0; index < timestampVector.size(); ++index)
            {
                const TimestampItem& timestampItem = timestampVector[index];

                fprintf(file_, "%s;%I64u;%s\r\n", timestampItem.id, timestampItem.timestamp, timestampItem.memo);
            }

            fflush(file_);

            if (stop)
                break;
        }
    }
}