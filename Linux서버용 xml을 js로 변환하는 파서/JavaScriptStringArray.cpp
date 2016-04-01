#include "JavaScriptStringArray.h"


JavaScriptStringArray::JavaScriptStringArray(void)
{
}


JavaScriptStringArray::~JavaScriptStringArray(void)
{
}

void JavaScriptStringArray::setValue(int _num,string _str,int _type)
{
	type=_type;
	str=_str;
	num=_num;
}

int JavaScriptStringArray::getNum()
{
	return num;
}


string JavaScriptStringArray::getStr()
{
	return str;
}

int JavaScriptStringArray::getType()
{
	return type;
}