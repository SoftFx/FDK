#pragma once

class CFinCalcImpl
{
public:
	static void* Constructor(const string& text);
	static void Destructor(void* handle);
	static void Calculate(void* handle);
	static void Clear(void* handle);
	static string Format(void* handle);
};