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
	int category;  //종류
	string name;  //버튼텍스트and버튼아이디, 아이템아이디
	Point position;  //위치
	Size size;   //아이템사이즈
	char* rgb;   //배경 색상
	char* image;  //배경  이미지

	char *vlImageTemp;  //vl이미지 임시용변수
	Size *vlSizeTemp;  //vl사이즈 임시용변수

	vector<char*> vlImage;  //vlImage 리스트
	vector<Size> vlSize;  //vlSize 리스트
	vector<string> text;  //listbox,textbox,button,textblock,combobox,checkbox에서 쓰이는 텍스트
	Size table;
	Size tableSize;

	string str;   //출력용 str

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

