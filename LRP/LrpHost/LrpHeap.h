#pragma once


class CLrpHeap
{
public:
	CLrpHeap();
	~CLrpHeap();
public:
	void* Heap();
private:
	void* m_heap;
};