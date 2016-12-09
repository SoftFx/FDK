#ifndef PERFORMANCE_PROCESSOR_H
#define PERFORMANCE_PROCESSOR_H

#include <vector>
#include <iostream>
#include <string>

class PerformanceProcessor
{
public:

    // Constructor / destructor

    PerformanceProcessor(const std::string& name, std::ostream& ostream);

    ~PerformanceProcessor();

    // Starting / stopping

    void start
    (        
        uint32_t warmupMessageCount, 
        uint32_t minMessageTime,
        uint32_t maxMessageTime,
        uint32_t messageTimeStep,
        uint64_t frequencySec
    );

    void stop();

    // Processing

    void process(uint64_t time0, uint64_t time1);

private:

    typedef std::vector<uint32_t> MessageCountVector;
    typedef std::vector<uint64_t> MessageLatencyVector;

    uint64_t getMessageLatencyPercentile(double persentile);

    std::string name_;
    std::ostream& ostream_;
    uint32_t warmupMessageCount_;
    uint32_t minMessageTime_;
    uint32_t maxMessageTime_;
    uint32_t messageTimeStep_;

    uint64_t frequencySec_;
    double frequencyUs_;

    uint64_t startMessageTime_;
    uint64_t endMessageTime_;
    uint64_t sumMessageTime_;
    uint64_t messageCount_;

    MessageCountVector messageCountVector_;
    MessageLatencyVector messageLatencyVector_;

    uint32_t skippedMessageCount_;

    bool started_;
};

#endif
