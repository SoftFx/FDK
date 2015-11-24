#pragma once


class CHeap
{
public:
	CHeap();
	~CHeap();
public:
	static void* Instance();
private:
	void* m_heap;
};