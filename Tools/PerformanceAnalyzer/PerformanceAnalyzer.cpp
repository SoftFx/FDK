#include "stdafx.h"

#include "PerformanceProcessor.h"

#include <iostream>
#include <string>
#include <map>

using namespace std;

struct TimestampItem
{
    uint64_t timestamp;
};

typedef map<string, TimestampItem> TimestampItemMap;

const char* getValueString(const char* psz, string& value)
{
    const char* pszEnd = psz;

    while (*pszEnd != ';' && *pszEnd != '\n' && *pszEnd != '\0')
        ++ pszEnd;

    value.assign(psz, pszEnd - psz);

    return pszEnd;
}

const char* getValueUInt64(const char* psz, uint64_t& value)
{
    char* pszEnd;
    value = strtoull(psz, &pszEnd, 10);

    if (*pszEnd != ';' && *pszEnd != '\n' && *pszEnd != '\0')
        throw exception("Invalid integer value");

    return pszEnd;
}

void readLogFile(const char* filename, string& description, TimestampItemMap& timestampItemMap)
{
    FILE* file = _fsopen(filename, "rt", _SH_DENYRW);

    if (file == NULL)
        throw exception((string("Could not open file: ") + filename).c_str());

    try
    {
        char buffer[1024];

        if (! fgets(buffer, sizeof(buffer), file))
        {
            if (feof(file))
                throw exception((string("Invalid file: ") + filename).c_str());

            throw exception((string("Could not read file: ") + filename).c_str());
        }

        getValueString(buffer, description);

        string id;
        TimestampItem timestampItem;

        while (fgets(buffer, sizeof(buffer), file))
        {
            const char* pc = getValueString(buffer, id);

            if (*pc != ';')
                throw exception((string("Invalid file: ") + filename).c_str());

            ++pc;

            pc = getValueUInt64(pc, timestampItem.timestamp);

            if (*pc != ';')
                throw exception((string("Invalid file: ") + filename).c_str());
            
            if (!timestampItemMap.insert(TimestampItemMap::value_type(id, timestampItem)).second)
            {
                cout << "WARN: Duplicate timestamp : " << id << ";" << timestampItem.timestamp << endl;
            }
        }

        if (! feof(file))
            throw exception((string("Could not read file: ") + filename).c_str());

        fclose(file);
    }
    catch (...)
    {
        fclose(file);

        throw;
    }
}

void process(const string& inDescription, const TimestampItemMap& inTimestampItemMap, const string& outDescription, const TimestampItemMap& outTimestampItemMap)
{
    string name = "Latency \"" + inDescription + " - " + outDescription + "\"";
    PerformanceProcessor  performanceProcessor(name, cout);

    performanceProcessor.start(0, 0, 200, 10, 1000000);

    for (TimestampItemMap::const_iterator itOut = outTimestampItemMap.begin(); itOut != outTimestampItemMap.end(); ++ itOut)
    {
        TimestampItemMap::const_iterator itIn = inTimestampItemMap.find(itOut->first);

        if (itIn != inTimestampItemMap.end())
        {
            if (itOut->second.timestamp >= itIn->second.timestamp)
            {
                performanceProcessor.process(itIn->second.timestamp, itOut->second.timestamp);
            }
            else
            {
                cout << "WARN: Invalid timestamps : " << itOut->first << ";" << itIn->second.timestamp << ";" << itOut->second.timestamp << endl;
            }
        }
    }

    performanceProcessor.stop();
}

void analyze(const char* inLogFilename, const char* outLogFilename)
{
    string inDescription;
    TimestampItemMap inTimestampItemMap;
    readLogFile(inLogFilename, inDescription, inTimestampItemMap);

    string outDescription;
    TimestampItemMap outTimestampItemMap;
    readLogFile(outLogFilename, outDescription, outTimestampItemMap);

    process(inDescription, inTimestampItemMap, outDescription, outTimestampItemMap);
}

int main(int argc, char* argv[])
{
    try
    {
        if (argc != 3)
        {
            if (argc == 1)
            {
                cout << "Usage: <in_log_file> <out_log_file>" << endl;
                return 0;
            }

            throw exception("Invalid command line");
        }

        const char* inLogFilename = argv[1];
        const char* outLogFilename = argv[2];

        analyze(inLogFilename, outLogFilename);

        return 0;
    }
    catch (const std::exception& exception)
    {
        cerr << "Error: " << exception.what() << endl;

        return 1;
    }
    catch (...)
    {
        cerr << "Error" << endl;

        return 1;
    }
}

