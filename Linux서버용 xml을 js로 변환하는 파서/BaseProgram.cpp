#include "BaseProgram.h"


BaseProgram::BaseProgram(void)
{
}


BaseProgram::~BaseProgram(void)
{
}


void BaseProgram::setType(int _type)
{
	type=_type;
	//cout<<"type="<<type<<endl;
}

void BaseProgram::setListComponent(list<MyComponent> _cmtList)
{
	listComponent=_cmtList;
}

void BaseProgram::show()
{
	cout<<"Information"<<endl<<"baseProgram type="<<type<<endl;

	for(list<MyComponent>::iterator iter=listComponent.begin();iter!=listComponent.end();iter++)
	{
		cout<<"Component"<<endl<<"component name="<<(*iter).getName()<<endl<<"component size="<<(double)(*iter).getSize().width<<","<<(double)(*iter).getSize().hight<<endl<<"Component rgb="<<(*iter).getRgb()<<endl;
		(*iter).show();
	}
}

int BaseProgram::getType()
{
	return type;
}

string BaseProgram::getString(int _index)
{
	return str;
}

list<MyComponent> & BaseProgram::getList()
{
	return listComponent;
}