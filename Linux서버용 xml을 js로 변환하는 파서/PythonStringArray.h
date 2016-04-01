#pragma once
#include <string>
using namespace std;
class PythonStringArray
{

private:
	int type;
	string str;
	int num;
public:
	PythonStringArray(void);
	~PythonStringArray(void);
	int getType();
	string getStr();
	int getNum();

	void setValue(int _num,string _str,int type);
};

