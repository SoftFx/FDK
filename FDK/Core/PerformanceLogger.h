#ifndef PERFORMANCE_LOGGER_H
#define PERFORMANCE_LOGGER_H

#include <vector>
#include <string>
#include <stdio.h>
#include <windows.h>

namespace Performance
{
    class CORE_API Logger;

    class CORE_API Service
    {
    public:

        Service();
        Service(int capacity);

        ~Service();

        bool started() const;

        void start(int capacity);

        void stop();

    private:

        friend class Logger;

        enum TimestampItemType
        {
            titLog,
            titSignal
        };

        struct TimestampItem
        {
            TimestampItemType type_;
            Logger* logger_;            
            char id[64];
            uint64_t timestamp;
            char memo[64];
        };

        typedef std::vector<TimestampItem> TimestampVector;

        static DWORD WINAPI thread(LPVOID parameter);

        void process();

        bool started_;

        LONGLONG timerFrequency_;

        TimestampVector timestampVector_;
        bool stop_;
        CRITICAL_SECTION cs_;
        HANDLE event_;
        HANDLE thread_;
    };

    class CORE_API Logger
    {
    public:

        Logger(Service& service);
        Logger(Service& service, const std::string& name, const std::string& description, const std::string& logDirectory);

        ~Logger();

        bool opened() const;

        void open(const std::string& name, const std::string& description, const std::string& logDirectory);

        void close();

        void logTimestamp(const char* id, uint64_t timestamp, const char* memo);

        uint64_t getTimestamp() const;

    private:

        friend class Service;
        
        Service& service_;

        std::string name_;        
        bool opened_;
        HANDLE event_;
        FILE* file_;        
    };
}

#endif
