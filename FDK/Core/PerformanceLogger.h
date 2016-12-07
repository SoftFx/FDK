#ifndef PERFORMANCE_LOGGER_H
#define PERFORMANCE_LOGGER_H

#include <vector>
#include <stdio.h>
#include <windows.h>

namespace Performance
{
    class CORE_API Logger
    {
    public:

        Logger();
        Logger(const char* name, const char* description, const char* logDirectory);

        ~Logger();

        bool opened() const;

        void open(const char* name, const char* description, const char* logDirectory);

        void close();

        void logTimestamp(const char* id, uint64_t timestamp, const char* memo);

        uint64_t getTimestamp() const;

    private:

        struct TimestampItem
        {
            char id[64];
            uint64_t timestamp;
            char memo[64];
        };

        typedef std::vector<TimestampItem> TimestampVector;

        static DWORD WINAPI thread(LPVOID parameter);

        void process();
        
        std::string name_;

        bool opened_;

        LONGLONG timerFrequency_;

        TimestampVector timestampVector_;
        bool stop_;
        CRITICAL_SECTION cs_;
        HANDLE event_;

        FILE* file_;

        HANDLE thread_;
    };
}

#endif
