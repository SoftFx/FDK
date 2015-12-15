#include "stdafx.h"

namespace
{
	class Initializer
	{
	public:
		Initializer();
		~Initializer();
	};

	Initializer::Initializer()
	{
		#ifdef _MSC_VER
		const WORD version = MAKEWORD(2, 2);
		WSADATA data;
		if (WSAStartup(version, &data))
		{
			throw runtime_error("couldn't initialize windows sockets.");
		}
		_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
		#endif
		if (!SSL_library_init())
		{
			throw runtime_error("couldn't initialize SSL.");
		}
		SSL_load_error_strings();
	}
	Initializer::~Initializer()
	{
		#ifdef _MSC_VER
		WSACleanup();
		#endif
	}
}

namespace
{
	Initializer gInitializer;
}
