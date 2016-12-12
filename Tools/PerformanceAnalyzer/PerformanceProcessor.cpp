#include "stdafx.h"
#include "PerformanceProcessor.h"

#include <algorithm>
#include <assert.h>

using namespace std;

PerformanceProcessor::PerformanceProcessor(const string& name, ostream& ostream):
    name_(name),
    ostream_(ostream),
    started_(false)
{
}

PerformanceProcessor::~PerformanceProcessor()
{
    stop();
}

void PerformanceProcessor::start
(
    uint32_t warmupMessageCount, 
    uint32_t minMessageTime,
    uint32_t maxMessageTime,
    uint32_t messageTimeStep,
    uint64_t frequencySec
)
{
    if (started_)
        throw exception("Performance processor is already started");

    warmupMessageCount_ = warmupMessageCount;
    minMessageTime_ = minMessageTime;
    maxMessageTime_ = maxMessageTime;
    messageTimeStep_ = messageTimeStep;

    frequencySec_ = frequencySec;
    frequencyUs_ = frequencySec / 1000000.0;

    messageCountVector_.resize((maxMessageTime - minMessageTime) / messageTimeStep + 2);
    memset(&messageCountVector_[0], 0, messageCountVector_.size() * sizeof(messageCountVector_[0]));

    messageLatencyVector_.clear();
    messageLatencyVector_.reserve(10000);
        
    startMessageTime_ = 0;
    endMessageTime_ = 0;
    sumMessageTime_ = 0;
    messageCount_ = 0;

    skippedMessageCount_ = 0;

    started_ = true;
}

void PerformanceProcessor::stop()
{
    if (started_)
    {
        started_ = false;

        assert(endMessageTime_ >= startMessageTime_);

        uint64_t messageTime = endMessageTime_ - startMessageTime_;        

        sort(messageLatencyVector_.begin(), messageLatencyVector_.end());

        ostream_ << name_ << " : " << endl;

        if (messageTime > frequencySec_)
        {
            ostream_ << "   Total time : " << messageTime / frequencySec_ << " s" << endl;
        }
        else
            ostream_ << "   Total time : " << 1000 * messageTime / frequencySec_ << " ms" << endl;

        ostream_ << "   Total events : " << messageCount_ << endl;

        if (messageTime)
        {
            ostream_ << "   Average rate : " << messageCount_ * frequencySec_ / messageTime << " event/s" << endl;
        }
        else
            ostream_ << "   Average rate : NA" << endl;

        if (messageCount_)
        {
            ostream_ << "   Average latency : " << sumMessageTime_ / messageCount_ / frequencyUs_ << " us" << endl;
            ostream_ << "   Minimal latency : " << messageLatencyVector_.front() / frequencyUs_ << " us" << endl;
        }
        else
        {
            ostream_ << "   Average latency : NA" << endl;
            ostream_ << "   Minimal latency : NA" << endl;
        }

        if (messageCount_ > 1)
        {
            ostream_ << "   10% latency : " << getMessageLatencyPercentile(10.0) / frequencyUs_ << " us" << endl;
            ostream_ << "   50% latency : " << getMessageLatencyPercentile(50.0) / frequencyUs_ << " us" << endl;
            ostream_ << "   70% latency : " << getMessageLatencyPercentile(70.0) / frequencyUs_ << " us" << endl;
            ostream_ << "   90% latency : " << getMessageLatencyPercentile(90.0) / frequencyUs_ << " us" << endl;
            ostream_ << "   99% latency : " << getMessageLatencyPercentile(99.0) / frequencyUs_ << " us" << endl;
        }
        else
        {
            ostream_ << "   10% latency : NA" << endl;
            ostream_ << "   50% latency : NA" << endl;
            ostream_ << "   70% latency : NA" << endl;
            ostream_ << "   90% latency : NA" << endl;
            ostream_ << "   99% latency : NA" << endl;
        }

        if (messageCount_)
        {
            ostream_ << "   Maximal latency : " << messageLatencyVector_.back() / frequencyUs_ << " us" << endl;

            uint32_t totalCount = messageCountVector_.front();
            ostream_ << "   < " << minMessageTime_ << " us  :  " << 100 * messageCountVector_.front() / messageCount_ << " | " << 100 * totalCount / messageCount_ << endl;

            for (size_t index = 1; index < messageCountVector_.size() - 1; ++ index)
            {
                totalCount += messageCountVector_[index];
                ostream_ << "   < " << minMessageTime_ + index * messageTimeStep_ << " us  :  " << 100 * messageCountVector_[index] / messageCount_ << " | " << 100 * totalCount / messageCount_ << endl;
            }

            totalCount += messageCountVector_.back();
            ostream_ << "   < inf  :  " << 100 * messageCountVector_.back() / messageCount_ << " | " << 100 * totalCount / messageCount_;
        }
        else
            ostream_ << "   Maximal latency : NA";

        ostream_ << endl;
    }
}

void PerformanceProcessor::process(uint64_t time0, uint64_t time1)
{
    assert(time1 >= time0);

    if (skippedMessageCount_ < warmupMessageCount_)
    {
        ++ skippedMessageCount_;

        return;
    }

    if (! startMessageTime_ || time0 < startMessageTime_)
        startMessageTime_ = time0;

    uint64_t messageTime = time1 - time0;
    sumMessageTime_ += messageTime;    
    ++ messageCount_;

    uint32_t messageTimeUs = (uint32_t) (messageTime / frequencyUs_);

    if (messageTimeUs < minMessageTime_)
    {
        ++ messageCountVector_.front();
    }
    else if (messageTimeUs >= maxMessageTime_)
    {
        ++ messageCountVector_.back();
    }
    else
    {
        size_t index = (messageTimeUs - minMessageTime_) / messageTimeStep_ + 1;
        ++ messageCountVector_[index];
    }

    messageLatencyVector_.push_back(messageTime);

    if (! endMessageTime_ || time1 > endMessageTime_)
        endMessageTime_ = time1;
}

uint64_t PerformanceProcessor::getMessageLatencyPercentile(double percentile)
{
    assert(percentile >= 0 && percentile < 100);

    if (! percentile)
        return messageLatencyVector_.front();

    size_t size = messageLatencyVector_.size();

    double d = percentile * (size - 1) / 100;
    int i = (int) d;
    uint64_t v = messageLatencyVector_[i];

    return v + (uint64_t) ((d - i) * (messageLatencyVector_[i + 1] - v));
}

