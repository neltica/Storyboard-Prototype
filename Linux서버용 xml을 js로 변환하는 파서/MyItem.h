#pragma once
#include <iostream>
#include <string>
#include <list>
#include <vector>

#include "ItemType.h"
#include "viewInformation.h"
#include "MyEvent.h"

typedef long long _Longlong;
using namespace std;
class MyItem
{
private:
	int category;  //����
	string name;  //��ư�ؽ�Ʈand��ư���̵�, �����۾��̵�
	Point position;  //��ġ
	Size size;   //�����ۻ�����
	char* rgb;   //��� ����
	char* image;  //���  �̹���

	char *vlImageTemp;  //vl�̹��� �ӽÿ뺯��
	Size *vlSizeTemp;  //vl������ �ӽÿ뺯��

	vector<char*> vlImage;  //vlImage ����Ʈ
	vector<Size> vlSize;  //vlSize ����Ʈ
	vector<string> text;  //listbox,textbox,button,textblock,combobox,checkbox���� ���̴� �ؽ�Ʈ
	Size table;
	Size tableSize;

	string str;   //��¿� str

	list<MyEvent> listEvent;
public:
	MyItem(void);
	~MyItem(void);

	// setter
	void setCategory(int _category);
	void setName(string _name);
	void setPosition(double _x, double _y);
	void setSize(double _width, double _hight);
	void setListEvent(list<MyEvent> _ListEvent);
	void setRgb(char* _rgb);

	void setvlImage(char *_vlImageTemp);
	void setvlSize(double x,double y);
	void setText(string _text);
	void setTable(double x,double y);
	void setTableSize(double x,double y);


	// getter
	int getCategory();
	string getName();
	Point getPosition();
	Size getSize();
	char* getRgb();
	list<MyEvent> & getList();

	vector<char*> getvlImage();
	vector<Size> getvlSize();
	vector<string> getText();
	Size getTable();
	Size getTableSize();



	//etc method

	string getPythonString(string componentName,int _index);
	string getPythonItemPack(int _index);

	string getJavaScriptString(string componentName,int _index);
	string getJavaScriptItemPack(int _index);
	void show();
	string intToString(int number);
};

