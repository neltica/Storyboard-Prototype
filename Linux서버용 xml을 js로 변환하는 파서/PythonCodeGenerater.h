#pragma once
#include <iostream>
#include <fstream>
#include <list>
#include <vector>
#include "BaseProgram.h"
#include "PythonStringArray.h"



using namespace std;

class PythonCodeGenerater
{
private:

	ofstream output;
	BaseProgram *baseProgram;
	vector<PythonStringArray> strList;
	void generaterStart();

	void windows();
	void android();
	void tizen();
	void web();
	void sort();
public:
	PythonCodeGenerater(void);
	PythonCodeGenerater(BaseProgram *_baseProgram);
	~PythonCodeGenerater(void);
};

