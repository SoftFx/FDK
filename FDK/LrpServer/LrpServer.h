#ifdef LRPSERVER_EXPORTS
#define LRPSERVER_API __declspec(dllexport)
#else
#define LRPSERVER_API __declspec(dllimport)
#endif
