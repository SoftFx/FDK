#pragma once

class CDailySnapshotsIteratorImpl
{
public:
    int TotalItems(void* handle);
    bool EndOfStream(void* handle);
    void Next(void* handle, size_t timeoutInMilliseconds);
    CFxDailyAccountSnapshotReport GetDailyAccountSnapshotReport(void* handle);
};
