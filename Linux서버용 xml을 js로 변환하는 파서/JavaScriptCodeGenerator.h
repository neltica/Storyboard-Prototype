#pragma once
#include <iostream>
#include <fstream>
#include <list>
#include <vector>
#include "BaseProgram.h"
#include "JavaScriptStringArray.h"



using namespace std;

class JavaScriptCodeGenerator
{
private:

	ofstream output;
	BaseProgram *baseProgram;
	vector<JavaScriptStringArray> strList;
	void generaterStart();

	void windows();
	void android();
	void tizen();
	void web();
	void sort();
public:
	JavaScriptCodeGenerator(void);
	JavaScriptCodeGenerator(BaseProgram *_baseProgram);
	~JavaScriptCodeGenerator(void);
};

