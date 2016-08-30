#include "stdafx.h"
#include "ClientImpl.h"
#include "Client.h"

namespace
{
    FxRef<CClient> ClientFromHandle(void* handle)
    {
        if (nullptr == handle)
        {
            throw CArgumentNullException("handle can not be null");
        }
        FxRef<CClient> result = TypeFromHandle<CClient>(handle);
        if (!result)
        {
            throw CInvalidHandleException("invalid handle");
        }
        return result;
    }
}


bool CClientImpl::Start(void* handle)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    const bool result = client->Start();
    return result;
}

HRESULT CClientImpl::Shutdown(void* handle)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    const HRESULT result = client->Shutdown();
    return result;
}
HRESULT CClientImpl::Stop(void* handle)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    const HRESULT result = client->Stop();
    return result;
}
std::string CClientImpl::NextId(void* handle)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    return client->NextId(cExternalSynchCall);
}
string CClientImpl::GetProtocolVersion(void* handle)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    return client->GetProtocolVersion();
}

HRESULT CClientImpl::GetNextMessage(void* /*handle*/, int& /*type*/, void*& /*data*/)
{
    return E_NOTIMPL;
}
bool CClientImpl::GetNextMessage(void* handle, CFxMessage& message)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    return client->GetNextMessage(message);
}
void CClientImpl::DispatchMessage(void* handle, const CFxMessage& message)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    client->DispatchMessage(message);
}
void CClientImpl::GetNetworkActivity(void* handle, uint64& dataBytesSent, uint64& sslBytesSent, uint64& dataBytesReceived, uint64& sslBytesReceived)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    client->GetNetworkActivity(&dataBytesSent, &sslBytesSent, &dataBytesReceived, &sslBytesReceived);
}
void CClientImpl::SendTwoFactorResponse(void* handle, const FxTwoFactorReason reason, const std::string& otp)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    return client->SendTwoFactorResponse(reason, otp);
}
CFxSessionInfo CClientImpl::GetSessionInfo(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    return client->GetSessionInfo(timeoutInMilliseconds);
}
CFxFileChunk CClientImpl::GetFileChunk(void* handle, const string& fileId, uint32 chunkId, const size_t timeoutInMilliseconds)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    return client->GetFileChunk(fileId, chunkId, timeoutInMilliseconds);
}

bool CClientImpl::WaitForLogon(void* handle, size_t timeoutInMilliseconds)
{
    FxRef<CClient> client = ClientFromHandle(handle);
    return client->WaitForLogon(timeoutInMilliseconds);
}
