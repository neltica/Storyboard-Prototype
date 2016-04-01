#pragma once
#include <string>
using namespace std;
class JavaScriptStringArray
{

private:
	int type;
	string str;
	int num;
public:
	JavaScriptStringArray(void);
	~JavaScriptStringArray(void);
	int getType();
	string getStr();
	int getNum();

	void setValue(int _num,string _str,int type);
};

