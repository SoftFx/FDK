#include "stdafx.h"
#include "PerformanceLogger.h"

#include <string>

using namespace std;

namespace Performance
{
    Service::Service() :
        started_(false)
    {
    }

    Service::Service(int capacity) :
        started_(false)
    {
        start(capacity);
    }

    Service::~Service()
    {
        stop();
    }

    bool Service::started() const
    {
        return started_;
    }

    void Service::start(int capcity)
    {
        if (started_)
            throw runtime_error("Performance service is already started");

        timestampVector_.reserve(32);

        LARGE_INTEGER frequency;
        if (!QueryPerformanceFrequency(&frequency))
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
                stop_ = false;
                thread_ = CreateThread(NULL, 0, thread, this, 0, NULL);

                if (thread_ == NULL)
                    throw runtime_error("Could not start a thread");

                started_ = true;
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

    void Service::stop()
    {
        if (started_)
        {
            started_ = false;

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

            CloseHandle(event_);

            DeleteCriticalSection(&cs_);
        }
    }
    
    DWORD WINAPI Service::thread(LPVOID parameter)
    {
        try
        {
            Service* service = (Service*)parameter;

            service->process();

            return 0;
        }
        catch (...)
        {
            return 1;
        }
    }

    void Service::process()
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

                if (timestampItem.type_ == titLog)
                {
                    fprintf(timestampItem.logger_->file_, "%s;%I64u;%s\r\n", timestampItem.id, timestampItem.timestamp, timestampItem.memo);
                    fflush(timestampItem.logger_->file_);
                }
                else if (timestampItem.type_ == titSignal)
                {
                    SetEvent(timestampItem.logger_->event_);
                }
            }

            if (stop)
                break;
        }
    }

    Logger::Logger(Service& service) :
        service_(service),
        opened_(false)
    {
    }

    Logger::Logger(Service& service, const char* name, const char* description, const char* logDirectory) :
        service_(service),
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

    void Logger::close()
    {
        if (opened_)
        {
            opened_ = false;

            EnterCriticalSection(&service_.cs_);

            try
            {
                service_.timestampVector_.resize(service_.timestampVector_.size() + 1);
                Service::TimestampItem& timestampItem = service_.timestampVector_.back();

                timestampItem.type_ = Service::titSignal;
                timestampItem.logger_ = this;

                SetEvent(service_.event_);

                LeaveCriticalSection(&service_.cs_);
            }
            catch (...)
            {
                LeaveCriticalSection(&service_.cs_);
            }

            WaitForSingleObject(event_, INFINITE);            

            fclose(file_);

            CloseHandle(event_);
        }
    }

    void Logger::logTimestamp(const char* id, uint64_t timestamp, const char* memo)
    {
        EnterCriticalSection(&service_.cs_);

        try
        {
            if (service_.timestampVector_.size() < 10000)
            {
                service_.timestampVector_.resize(service_.timestampVector_.size() + 1);
                Service::TimestampItem& timestampItem = service_.timestampVector_.back();

                timestampItem.type_ = Service::titLog;
                timestampItem.logger_ = this;                
                strncpy(timestampItem.id, id, sizeof(timestampItem.id));
                timestampItem.timestamp = timestamp;
                strncpy(timestampItem.memo, memo, sizeof(timestampItem.memo));

                SetEvent(service_.event_);
            }

            LeaveCriticalSection(&service_.cs_);
        }
        catch (...)
        {
            LeaveCriticalSection(&service_.cs_);

            throw;
        }
    }

    uint64_t Logger::getTimestamp() const
    {
        LARGE_INTEGER counter;
        QueryPerformanceCounter(&counter);

        return (uint64_t) (counter.QuadPart * 1000000 / service_.timerFrequency_);
    }    
}