#include "stdafx.h"
#include "FinCalcImpl.h"
#include "FinancialCalculator.h"

using namespace FDK;

void* CFinCalcImpl::Constructor(const string& text)
{
	CFinancialCalculator* result = new CFinancialCalculator(text.c_str());
	return result;
}

void CFinCalcImpl::Destructor(void* handle)
{
	CFinancialCalculator* pCalc = reinterpret_cast<CFinancialCalculator*>(handle);
	delete pCalc;
}

void CFinCalcImpl::Calculate(void* handle)
{
	CFinancialCalculator* pCalc = reinterpret_cast<CFinancialCalculator*>(handle);
	pCalc->Calculate();
}

void CFinCalcImpl::Clear(void* handle)
{
	CFinancialCalculator* pCalc = reinterpret_cast<CFinancialCalculator*>(handle);
	pCalc->Clear();
}

string CFinCalcImpl::Format(void* handle)
{
	CFinancialCalculator* pCalc = reinterpret_cast<CFinancialCalculator*>(handle);
	return pCalc->ToString();
}
