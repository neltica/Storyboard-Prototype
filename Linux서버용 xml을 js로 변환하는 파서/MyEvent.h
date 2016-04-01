#pragma once
#include <iostream>
#include <string>
#include <cstring>
#include "EventType.h"
using namespace std;
class MyEvent
{
	int category;
	char* act;
	char* passComponentName;
	char* name;
	string str;

public:
	MyEvent(void);
	~MyEvent(void);

	void setCategory(int _category);
	void setAct(char* _act);
	void setPassComponentName(char* _passComponentName);
	void setName(char* _name);

	int getCategory();
	char* getAct();
	char* getPassComponentName();
	string getPythonString(string itemName,int itemIndex);
	string getJavaScriptString(string itemName,int itemIndex);
};

