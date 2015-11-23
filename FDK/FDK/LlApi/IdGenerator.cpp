#include "stdafx.h"
#include "IdGenerator.h"

CIdGenerator::CIdGenerator()
    : m_id()
    , m_suffix(FxGenerateGuid())
{
}

uint64 CIdGenerator::Next()
{
	m_synchronizer.Acquire();
	const uint64 result = ++m_id;
	m_synchronizer.Release();
	return result;
}

string CIdGenerator::Next(const string& prefix)
{
	stringstream stream;
	const uint64 id = Next();
	stream<<prefix<<id<<m_suffix;
	return stream.str();
}

void CIdGenerator::Reset()
{
	string suffix = FxGenerateGuid();
	m_synchronizer.Acquire();
	m_id = 0;
	swap(m_suffix, suffix);
	m_synchronizer.Release();
}
