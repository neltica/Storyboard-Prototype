#include "MyEvent.h"


MyEvent::MyEvent(void)
{
}


MyEvent::~MyEvent(void)
{
}


void MyEvent::setCategory(int _category)
{
	//category=new char;
	//strcpy(category,_category);
	category=_category;
//	cout<<category<<endl;
}

int MyEvent::getCategory()
{
	return category;
}

void MyEvent::setAct(char* _act)
{
	act=new char;
	strcpy(act,_act);
	//cout<<act<<endl;
}

void MyEvent::setName(char *_name)
{
	name=new char;
	strcpy(name,_name);
}

char* MyEvent::getAct()
{
	return act;
}

void MyEvent::setPassComponentName(char* _passComponentName)
{
	passComponentName=new char;
	strcpy(passComponentName,_passComponentName);
	//cout<<passComponentName<<endl;
}

char* MyEvent::getPassComponentName()
{
	return passComponentName;
}

string MyEvent::getPythonString(string itemName,int itemIndex)
{
	switch(category)
	{
	case 0:
		str="Event.Event(\"onclick\","+itemName+","+passComponentName+")";
		break;
	}
	return str;
}

string MyEvent::getJavaScriptString(string itemName,int itemIndex)
{
	switch(category)
	{
	case 0:
		str="Event.Event(\"onclick\","+itemName+","+passComponentName+")";
		break;
	}
	return str;
}