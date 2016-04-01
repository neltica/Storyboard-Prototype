#pragma once

#include <list>
#include <string>
#include <iostream>
#include <cstring>
#include "MyItem.h"
#include "ComponentType.h"
#include "viewInformation.h"
typedef long long _Longlong;
using namespace std;

class MyComponent
{
private:
	// member
	int type;
	char* name;
	Point position;
	Size size;
	list<MyItem> listItem;
	// function
	char *rgb;
	string str;

public:

	// Constructor & destructor
	MyComponent(void);
	~MyComponent(void);

	// member

	// setter
	void setType(int _type);
	void setName(char* _name);
	void setPosition(double _x, double _y);
	void setSize(double _width, double _hight);
	void setListItem(list<MyItem> _listItem);
	void setRgb(char* _rgb);

	// getter
	int getType();
	char* getName();
	Point getPosition();
	Size getSize();
	char *getRgb();

	string getPythonString(int _type,int _index);
	string getJavaScriptString(int _type,int _index);

	list<MyItem> & getList();


	void show();

	//string componentToPython(int index);
};

