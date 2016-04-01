#pragma once
#include <iostream>
#include <string>
#include <fstream>
#include <stack>
#include <vector>
#include <list>
#include <map>
#include "BaseProgram.h"
#include "PythonCodeGenerater.h"
#include "JavaScriptCodeGenerator.h"


using namespace std;


class Parser
{
private:

	/*Filed*/
	ifstream* getFile;	

	BaseProgram baseProgram; 
	PythonCodeGenerater *pythonCodeGenerater;
	JavaScriptCodeGenerator *javaScriptCodeGenerator;


	MyComponent *preComponent;  
	MyItem *preItem;            
	MyEvent *preEvent;          
	list<MyItem> tmpItem;
	list<MyComponent> tmpComponent;
	list<MyEvent> tmpEvent;

	char *temp;

	int flag;  // 0==Python  1==Web

	/*Method*/
	char* get_token(char* _token);
	int checkTag(char* _tag);
	int checkItemCategory(char* _temp);
	int checkEventCategory(char* _temp);
	int checkComponentType(char* _temp);
public:

	Parser(void);
	Parser(ifstream* _input);
	~Parser(void);

	void start(int flag);

	void show();

};
