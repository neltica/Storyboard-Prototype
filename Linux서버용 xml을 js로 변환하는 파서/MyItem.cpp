#include "MyItem.h"


MyItem::MyItem(void)
{
	rgb="0";
	image="0";
	position.x=0;
	position.y=0;
	size.hight=0;
	size.width=0;
}


MyItem::~MyItem(void)
{
}


// setter

void MyItem::setRgb(char* _rgb)
{
	rgb=new char;
	strcpy(rgb,_rgb);
}
void MyItem::setCategory(int _category)
{
	category=_category;
	//cout<<category<<endl;
}
void MyItem::setName(string _name)
{
	name = _name;
	//cout<<name<<endl;
}
void MyItem::setPosition(double _x, double _y)
{
	position.x = _x;
	position.y = _y;
	//cout<<position.x<<","<<position.y<<endl;
}
void MyItem::setSize(double _width, double _hight)
{
	size.width = _width;
	size.hight = _hight;
	//cout<<size.hight<<","<<size.width<<endl;
}

// getter
int MyItem::getCategory()
{
	return category;
}
string MyItem::getName()
{
	return name;
}
Point MyItem::getPosition()
{
	return position;
}
Size MyItem::getSize()
{
	return size;
}

void MyItem::setListEvent(list<MyEvent> _ListEvent)
{
	listEvent=_ListEvent;
}

void MyItem::setvlImage(char *_vlImageTemp)
{
	vlImageTemp=new char();
	strcpy(vlImageTemp,_vlImageTemp);
	vlImage.push_back(vlImageTemp);
}

void MyItem::setvlSize(double x,double y)
{
	vlSizeTemp=new Size();
	vlSizeTemp->width=x;
	vlSizeTemp->hight=y;
	vlSize.push_back(*vlSizeTemp);
}

void MyItem::setText(string _text)
{
	text.push_back(_text);
}

void MyItem::setTable(double x,double y)
{
	table.width=x;
	table.hight=y;
}

void MyItem::setTableSize(double x,double y)
{
	tableSize.width=x;
	tableSize.hight=y;
}

void MyItem::show()
{
	for(list<MyEvent>::iterator iter=listEvent.begin();iter!=listEvent.end();iter++)
	{
		cout<<"Event"<<endl<<"Event act="<<(*iter).getAct()<<endl<<"Event category="<<(*iter).getCategory()<<endl<<"Event passComponentName="<<(*iter).getPassComponentName()<<endl;
	}
}

string MyItem::getPythonString(string componentName,int _index)
{
	string typeName;
	switch(category)
	{
	case 0:
		typeName="MyButton";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+",\""+text.at(0)+"\","+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+","+"\""+rgb+"\","+"image)";
		break;
	case 1:
		typeName="MyTextbox";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+","+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+",\""+rgb+"\","+")";
		break;
	case 2:
		typeName="MyTextblock";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+","+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+",\""+rgb+"\","+image+","+text.at(0)+")";
		break;
	case 3:
		break;
	case 4:
		typeName="MySlideview";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+","+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+",image)";
		break;
	case 5:
		typeName="MyListview";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+","+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+","+to_string((_Longlong)table.width)+","+to_string((_Longlong)table.hight)+","+to_string((_Longlong)tableSize.width)+","+to_string((_Longlong)tableSize.hight)+"\""+rgb+"\""+"image)";
		break;
	case 6:
		typeName="MyListbox";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+","+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+",\""+rgb+"\","+"texts)";
		break;
	case 7:
		typeName="MyCombobox";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+","+intToString(position.x)+","+intToString(position.y)+","+intToString(size.width)+",texts)";
		break;
	case 8:
		typeName="MyCheckbox";
		str=name+"=";
		str+="Item."+typeName+"("+componentName+","+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+",\""+rgb+"\","+text.at(0)+")";
		break;
	}
	return str;
}

string MyItem::getPythonItemPack(int _index)
{
	str=name+".placeItem()";
	return str;
}

list<MyEvent> & MyItem::getList()
{
	return listEvent;
}

char* MyItem::getRgb()
{
	return rgb;
}



string MyItem::getJavaScriptString(string componentName,int _index)
{
	string typeName;
	switch(category)
	{
	case 0:
		str="str+=MyButton(str,"+to_string((_Longlong)position.x)+","+to_string((_Longlong)position.y)+","+to_string((_Longlong)size.width)+","+to_string((_Longlong)size.hight)+",\""+name+"\",0);";  //마지막 0은 이미지 주소들어갈곳 ->0이면 이미지가 없음(Default)
		break;
	case 1:
		break;
	case 2:
		break;
	case 3:
		break;
	case 4:
		break;
	case 5:
		break;
	case 6:
		break;
	case 7:
		break;
	case 8:
		break;
	case 9:
		break;
	case 10:
		break;
	case 11:
		break;
	case 12:
		break;
	}
	return str;
}

string MyItem::getJavaScriptItemPack(int _index)
{
	str=name+to_string((_Longlong)_index)+".placeItem()";
	return str;
}

vector<char*> MyItem::getvlImage()
{
	return vlImage;
}

vector<Size> MyItem::getvlSize()
{
	return vlSize;
}

vector<string> MyItem::getText()
{
	return text;
}

Size MyItem::getTable()
{
	return table;
}

Size MyItem::getTableSize()
{
	return tableSize;
}

string MyItem::intToString(int number)
{
	return to_string((_Longlong)number);
}