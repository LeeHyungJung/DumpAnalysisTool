#include "stdafx.h"
#include "Utility.h"
//-------------------------------------------------------------------------------- Utility

string wstr2mbcs(const wstring& str)
{
	uint32 codePage = GetOEMCP();

	int32 newLength = WideCharToMultiByte(
		codePage, 0, str.c_str(), -1,
		NULL, 0, NULL, NULL) - 1;

	string mbcs;
	mbcs.resize(newLength);

	WideCharToMultiByte(codePage, 0, str.c_str(), -1,
		(LPSTR)mbcs.c_str(), newLength + 1, NULL, NULL);

	return mbcs;
}

string wstr2utf8(const wstring& str)
{
	int32 newLength = WideCharToMultiByte(
		CP_UTF8, 0, str.c_str(), -1,
		NULL, 0, NULL, NULL) - 1;

	string utf8;
	utf8.resize(newLength);

	WideCharToMultiByte(CP_UTF8, 0, str.c_str(), -1,
		(LPSTR)utf8.c_str(), newLength + 1, NULL, NULL);

	return utf8;
}

wstring utf82wstr(const string& str)
{
	int32 newLength = MultiByteToWideChar(
		CP_UTF8, 0, str.c_str(), -1,
		NULL, 0) - 1;

	wstring wstr;
	wstr.resize(newLength);

	MultiByteToWideChar(CP_UTF8, 0, str.c_str(), -1,
		(LPWSTR)wstr.c_str(), newLength + 1);

	return wstr;
}

wstring getFileNameFromPath(const wstring& str)
{
	size_t f0 = str.rfind(L"\\");
	size_t f1 = str.rfind(L"/");
	int p = max(
		(f0 == string::npos) ? -1 : int(f0),
		(f1 == string::npos) ? -1 : int(f1));
	return &str[p + 1];
}

bool parseOpt(const vector<wstring>& args, const wchar_t* optFormat,
	map<wchar_t, wstring>* optMap, vector<wstring>* leftArgs)
{
	// format �� �ؼ��� argMap ����

	map<wchar_t, int> argMap;
	for (const wchar_t* i = optFormat; *i != 0; i++)
	{
		wchar_t f = i[0], f_next = i[1];
		if (f_next == L':')
		{
			argMap[f] = 1;
			i++;
		}
		else
		{
			argMap[f] = 0;
		}
	}

	// argMap �� ���� args �Ľ�

	int argsCount = int(args.size());
	for (int i = 0; i < argsCount; i++)
	{
		const wstring& arg = args[i];
		if (arg[0] == L'-' && leftArgs->empty())
		{
			auto av = argMap.find(arg[1]);
			if (av == argMap.end())
			{
				printf("%S: Unknown argument\n", arg.c_str());
				return false;
			}
			if (av->second == 0)
			{
				if (arg[2] != 0)
				{
					printf("%S: Invalid argument\n", arg.c_str());
					return false;
				}
				(*optMap)[av->first] = wstring();
			}
			else
			{
				if (arg[2] != 0)
				{
					(*optMap)[av->first] = wstring(&arg[2]);
				}
				else
				{
					if (i < argsCount - 1)
					{
						(*optMap)[av->first] = args[i + 1];
						i++;
					}
					else
					{
						printf("%S: Insufficient argument\n", arg.c_str());
						return false;
					}
				}
			}
		}
		else
		{
			leftArgs->push_back(arg);
		}
	}

	return true;
}

bool isOpt(const map<wchar_t, wstring>& optMap, wchar_t opt)
{
	auto i = optMap.find(opt);
	return i != optMap.end();
}

wstring getOptValue(const map<wchar_t, wstring>& optMap, wchar_t opt, wstring defaultValue)
{
	auto i = optMap.find(opt);
	return i != optMap.end() ? i->second : defaultValue;
}

PCSTR convertString(PCWSTR pszValue)
{
	size_t i;
	wcstombs_s(&i, nullptr, 0, pszValue, _TRUNCATE);
	char *pmb = (char *)malloc(i);
	wcstombs_s(&i, pmb, i, pszValue, _TRUNCATE);
	return pmb;
}

wchar_t* CharUtility::CharToWChar(const char* pstrSrc)
{
	assert(pstrSrc);
	size_t nLen = strlen(pstrSrc) + 1;

	wchar_t* pwstr = (LPWSTR)malloc(sizeof(wchar_t)* nLen);
	size_t returnValue = 0;

	mbstowcs_s(&returnValue, pwstr, nLen, pstrSrc, nLen);

	pwstrContainer.push_back(pwstr);
	return pwstr;
}

char* CharUtility::WCharToChar(const wchar_t* pwstrSrc)
{
	assert(pwstrSrc);

#if !defined _DEBUG
	int len = 0;
	len = (wcslen(pwstrSrc) + 1) * 2;
	char* pstr = (char*)malloc(sizeof(char)* len);

	WideCharToMultiByte(949, 0, pwstrSrc, -1, pstr, len, NULL, NULL);
#else

	int nLen = wcslen(pwstrSrc);

	char* pstr = (char*)malloc(sizeof(char)* nLen + 1);
	wcstombs(pstr, pwstrSrc, nLen + 1);
#endif

	pstrContainer.push_back(pstr);
	return pstr;
}

