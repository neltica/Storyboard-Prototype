#include "PythonStringArray.h"


PythonStringArray::PythonStringArray(void)
{
}


PythonStringArray::~PythonStringArray(void)
{
}

void PythonStringArray::setValue(int _num,string _str,int _type)
{
	type=_type;
	str=_str;
	num=_num;
}

int PythonStringArray::getNum()
{
	return num;
}


string PythonStringArray::getStr()
{
	return str;
}

int PythonStringArray::getType()
{
	return type;
}