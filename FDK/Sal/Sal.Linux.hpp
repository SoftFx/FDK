#include <dlfcn.h>
#include <iostream>
#include <sstream>
#include <fstream>
string FxGenerateGuid()
{
	uuid_t guid;
	char buffer[256];
	uuid_generate(guid);
	uuid_unparse(guid, buffer);
	string result = buffer;
	return result;
}
SAL_API std::string FxStartupDirectory()
{
	//init executable url
	char buffer[512] = "";
	sprintf(buffer, "/proc/%d/exe", getpid());
	string st = buffer;
	readlink(st.c_str(), buffer, sizeof(buffer));
	const string result = buffer;
	return result;
}

uint64 FxGetTickCount()
{
	timeval interval;
	gettimeofday(&interval, NULL);
	uint64 result = interval.tv_sec;
	result *= 1000;
	result += interval.tv_usec / 1000;
	return result;
}
std::string FxStringFromResource(void* module, const std::string& id, const std::string& type)
{
	Dl_info info;
	dladdr(module, &info);
	cout<<info.dli_fname<<endl;
	string path = info.dli_fname;
	size_t index = path.find_last_of('/');
	path = path.substr(0, 1 + index);
	path = path + id + type;

	cout<<path<<endl;

	std::stringstream stream;
	std::ifstream file(path.c_str());

	for (;!file.eof();)
	{
		char ch = 0;
		file.get(ch);
		if(0 != ch)
		{
			stream<<ch;
		}
	}
	std::string result = stream.str();
	return result;
}
string FxCombinePath(const string& directory, const string& fileName)
{
	if (directory.empty())
	{
		return fileName;
	}
	if ('/' == directory.back())
	{
		return directory + fileName;
	}
	else
	{
		return directory + '/' + fileName;
	}
}