#pragma once

//-------------------------------------------------------------------------------- Utility

//! �־��� �ּҸ� ���� ����Ʈ�� ���ߴ� ��ũ�� (������ �ʿ��ϸ� �ּҸ� ������Ŵ)
#define ALIGN_UP(addr, align) (((addr)+((align)-1)) & (~((align)-1)))

//! �����ڵ� ���ڿ��� MBCS ���ڿ��� ��ȯ
string wstr2mbcs(const wstring& str);

//! �����ڵ� ���ڿ��� UTF-8 �� ���ڵ��� ��ȯ
string wstr2utf8(const wstring& str);

//! �����ڵ� ���ڿ��� UTF-8 �� ���ڵ��� ��ȯ
wstring utf82wstr(const string& str);

//! ��ο��� ���� �̸��� ��ȯ
wstring getFileNameFromPath(const wstring& str);

//! �־��� args �� �Ľ��� ����� ��ȯ�մϴ�.
bool parseOpt(
	const vector<wstring>& args, const wchar_t* optFormat,
	map<wchar_t, wstring>* optMap, vector<wstring>* leftArgs);

//! OptMap ���� opt �� �ش��ϴ� ���� �ִ��� ���θ� ��ȯ�մϴ�.
bool isOpt(const map<wchar_t, wstring>& optMap, wchar_t opt);

//! OptMap ���� opt �� �ش��ϴ� ���� ã�� ��ȯ�մϴ�. ������ defaultValue �� ��ȯ�մϴ�.
wstring getOptValue(
	const map<wchar_t, wstring>& optMap, wchar_t opt,
	wstring defaultValue = wstring());

PCSTR convertString(PCWSTR pszValue);

//Char ptr ���� �޸� ������ ���� ���� ����
class CharUtility
{

public:
	CharUtility()
	{

		for (auto it = pstrContainer.begin(); it != pstrContainer.end(); it++)
		{
			char * temp = *it;

			if (temp != NULL)
				delete temp;
		}
		pstrContainer.clear();

		for (auto it = pwstrContainer.begin(); it != pwstrContainer.end(); it++)
		{
			wchar_t* temp = *it;
			if (temp != NULL)
				delete temp;
		}
		pwstrContainer.clear();

	}

private:
	~CharUtility()
	{
		for (auto it = pstrContainer.begin(); it != pstrContainer.end(); it++)
		{
			char * temp = *it;

			if (temp != NULL)
			{
				delete temp;
			}
		}
		pstrContainer.clear();

		for (auto it = pwstrContainer.begin(); it != pwstrContainer.end(); it++)
		{
			wchar_t* temp = *it;
			if (temp != NULL)
			{
				delete temp;
			}
		}
		pwstrContainer.clear();
		printf("Delete unmanagedMemory \n");
	}


private:
	vector<char*> pstrContainer;
	vector<wchar_t*> pwstrContainer;

public:
	static CharUtility & getInstance()
	{
		static CharUtility inst;
		return inst;
	}

	wchar_t* CharToWChar(const char* pstrSrc);

	char* WCharToChar(const wchar_t* pwstrSrc);

	void addCharPtrToManagedMemory(char * pstr)
	{
		pstrContainer.push_back(pstr);
	}

	void ClearMemory()
	{
		for (auto it = pstrContainer.begin(); it != pstrContainer.end(); it++)
		{
			char * temp = *it;

			if (temp != NULL)
			{
				delete temp;
			}
		}
		pstrContainer.clear();

		for (auto it = pwstrContainer.begin(); it != pwstrContainer.end(); it++)
		{
			wchar_t* temp = *it;
			if (temp != NULL)
			{
				delete temp;
			}
		}
		pwstrContainer.clear();
		printf("Delete unmanagedMemory \n");
	}



};