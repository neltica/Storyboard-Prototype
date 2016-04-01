#include "MyComponent.h"

MyComponent::MyComponent(void)
{
}


MyComponent::~MyComponent(void)
{
}

// setter

void MyComponent::setName(char* _name)
{
	name=new char;
	strcpy(name,_name);
	//cout<<name<<endl;
}

void MyComponent::setPosition(double _x, double _y)
{
	position.x = _x;
	position.y = _y;
	//cout<<position.x<<position.y<<endl;
}

void MyComponent::setSize(double _width, double _hight)
{
	size.hight = _hight;
	size.width = _width;
	//cout<<size.hight<<","<<size.width<<endl;
}

void MyComponent::setListItem(list<MyItem> _listItem)
{
	listItem=_listItem;
}

void MyComponent::setRgb(char *_rgb)
{
	rgb=new char;
	strcpy(rgb,_rgb);
}

// getter


char* MyComponent::getName()
{
	return name;
}

Point MyComponent::getPosition()
{
	return position;
}

Size MyComponent::getSize()
{
	return size;
}


char * MyComponent::getRgb()
{
	return rgb;
}

void MyComponent::show()
{
	for(list<MyItem>::iterator iter=listItem.begin();iter!=listItem.end();iter++)
	{
		cout<<"Item"<<endl<<"Item name="<<(*iter).getName()<<endl<<"Item category="<<(*iter).getCategory()<<endl;
		(*iter).show();
	}
}
//
//string MyComponent::componentToPython(int index)
//{
//	return string("app"+index);
//}

string MyComponent::getPythonString(int _type,int _index)
{
	switch(_type)
	{
	case ANDROID:
		break;
	case TIZEN:
		break;
	case WINDOW_FORM:
		str=name;
		if(_index==0)
		{
			str+="=Component.Component(\"parent\","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+")";
		}
		else
		{
			str+="=Component.Component(\"child\","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+")";
		}
		break;
	case WEB_PAGE:
		break;
	}
	return str;
}

string MyComponent::getJavaScriptString(int _type,int _index)
{
	switch(_type)
	{
	case ANDROID:
		break;
	case TIZEN:
		break;
	case WINDOW_FORM:
		str+="str+=MyComponent(str,"+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+",0);";
		break;
	case WEB_PAGE:
		break;
	}
	return str;
}



list<MyItem> & MyComponent::getList()
{
	return listItem;
}
