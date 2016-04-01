#pragma once
#include <iostream>
#include <list>
#include <string>
#include "MyComponent.h"

using namespace std;

class BaseProgram
{
private:
	int typecnt;
	string str;
	int type;
	list<MyComponent> listComponent;
public:
	BaseProgram(void);
	~BaseProgram(void);

		
	// setter
	void setType(int type);
	void setTypeCount();
	void setListComponent(list<MyComponent> listComponent);

	// getter
	int getType();
	int getTypeCount();
	list<MyComponent> & getList();

	string getString(int _index);


	void show();
};

