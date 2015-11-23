#pragma once


typedef bool (*DllMainFunc)(HANDLE module);
bool DisableFreeLibrary(HANDLE module);
bool LoadFixFields(HANDLE module);
bool ProcessConfig(HANDLE module);












