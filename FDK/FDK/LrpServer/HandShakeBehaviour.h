#pragma once

#include "BaseBehaviour.h"


class COutgoing;
class CHandShakeBehaviour;
typedef HRESULT (CHandShakeBehaviour::*CHandShakeMethod)();


class CHandShakeBehaviour : public CBaseBehaviour
{
public:
	CHandShakeBehaviour(CChannel& channel);
	virtual HRESULT VProcess(const uint64 now);
public:
	void Logon(const HRESULT status, const string& message);
private:
	void StartLogicalAccept();
	HRESULT DoLogicalAccept();
private:
	void StartClientVersion();
	HRESULT DoClientVersion();
private:
	void StartVersionResponse();
	HRESULT DoVersionResponse();
private:
	void StartUsernamePassword();
	HRESULT DoUsernamePassword();
private:
	HRESULT DoLogon();
	HRESULT DoLogout();
private:
	void StartClientSignature();
	HRESULT DoClientSignature();
private:
	void StartServerSignature();
	HRESULT DoServerSignature();
private:
	HRESULT SelectReceive();
	HRESULT Receive();
	HRESULT ReceiveSize();
private:
	CHandShakeMethod m_method;
	COutgoing& m_outgoing;
};
