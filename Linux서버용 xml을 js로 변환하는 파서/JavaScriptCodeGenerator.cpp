#include "JavaScriptCodeGenerator.h"


JavaScriptCodeGenerator::JavaScriptCodeGenerator(void)
{
}


JavaScriptCodeGenerator::~JavaScriptCodeGenerator(void)
{
	output.close();
}


JavaScriptCodeGenerator::JavaScriptCodeGenerator(BaseProgram *_baseProgram)
{
	baseProgram=_baseProgram;
	generaterStart();
}

void JavaScriptCodeGenerator::generaterStart()
{
	output.open("MyDraw.js");
	output<<"function MyDraw(){"<<endl;
	output<<"var str='';"<<endl;

	cout<<endl<<"function Draw(){"<<endl;
	cout<<"var str='';"<<endl;
	switch(baseProgram->getType())
	{
	case ANDROID:
		android();
		break;
	case TIZEN:
		tizen();
		break;
	case WINDOW_FORM:
		windows();
		break;
	case WEB_PAGE:
		web();
		break;
	}

	output<<"document.getElementById(\"svgTag\").innerHTML = str;}"<<endl;

	cout<<"document.getElementById(\"svgTag\").innerHTML = str;}"<<endl;
}

void JavaScriptCodeGenerator::windows()
{

	JavaScriptStringArray javaScriptStringArray;  //vector에 삽입하기 위해 임시로 필요한 변수

	list<MyComponent> tempComponent=baseProgram->getList();  //컴포넌트 iterator를 얻어옴
	int componentIndex=0;  //iterator 인덱스를 세기 위한 변수

	for(list<MyComponent>::iterator iter0=tempComponent.begin();iter0!=tempComponent.end();iter0++)
	{
		//cout<<(*iter0).getPythonString(WINDOW_FORM,componentIndex)<<endl;
		javaScriptStringArray.setValue(componentIndex,(*iter0).getJavaScriptString(WINDOW_FORM,componentIndex),0);  //임시변수에 데이터를 채워넣음
		strList.push_back(javaScriptStringArray);   //vector에 삽입
				
		list<MyItem> tempItem= (*iter0).getList();  //아이템 iterator를 얻어옴
		//int itemIndex=0;
		for(list<MyItem>::iterator iter1=tempItem.begin(); iter1!=tempItem.end();iter1++)  
		{
			//cout<<(*iter1).getPythonString((*iter0).getName(),itemIndex)<<endl;
			javaScriptStringArray.setValue(componentIndex,(*iter1).getJavaScriptString((*iter0).getName(),componentIndex),1);  //임시변수에 데이터 채워넣음
			strList.push_back(javaScriptStringArray);  //vector에 삽입

			//list<MyEvent> tempEvent= (*iter1).getList(); //이벤트 iterator 얻어옴
			////int eventIndex=0;
			//for(list<MyEvent>::iterator iter2=tempEvent.begin(); iter2!=tempEvent.end();iter2++)
			//{
			//	//cout<<(*iter2).getPythonString((*iter1).getName(),itemIndex)<<endl;
			//	javaScriptStringArray.setValue(componentIndex,(*iter2).getJavaScriptString((*iter1).getName(),componentIndex),2);  //데이터 채움
			//	strList.push_back(javaScriptStringArray);  //삽입
			//	//eventIndex++;
			//}
		//	itemIndex++;
		}
		componentIndex++;  //인덱스 ++
	}


	sort();  //컴포넌트, 아이템, 이벤트 순으로 정렬함
	for(vector<JavaScriptStringArray>::iterator iter=strList.begin();iter!=strList.end();iter++)
	{
		cout<<(*iter).getType()<<" "<<(*iter).getNum()<<" "<<(*iter).getStr()<<endl;
		output<<(*iter).getStr()<<endl;
	}

	////아이템들을 컴포넌트에 위치시킴
	//for(vector<JavaScriptStringArray>::iterator iter=strList.begin();iter!=strList.end();iter++)
	//{
	//	if((*iter).getType()==1)
	//	{
	//		cout<<(*iter).getType()<<" "<<(*iter).getNum()<<" "<<(*iter).getStr().substr(0,(*iter).getStr().find("="))<<".placeItem()"<<endl;
	//		output<<(*iter).getStr().substr(0,(*iter).getStr().find("="))<<".placeItem()"<<endl;
	//	}
	//}

}
void JavaScriptCodeGenerator::android()
{
}
void JavaScriptCodeGenerator::web()
{

}
void JavaScriptCodeGenerator::tizen()
{

}

void JavaScriptCodeGenerator::sort()
{
	JavaScriptStringArray temp;
	for(vector<JavaScriptStringArray>::iterator iter0=strList.begin();iter0!=strList.end();iter0++)
	{
		for(vector<JavaScriptStringArray>::iterator iter1=iter0;iter1!=strList.end();iter1++)
		{
			if((*iter0).getType()>(*iter1).getType())
			{
				temp= *iter0;
				*iter0 = *iter1;
				*iter1=temp;
			}
			else if((*iter0).getType()==(*iter1).getType())
			{
				if((*iter0).getNum()>(*iter1).getNum())
				{
					JavaScriptStringArray temp= *iter0;
					*iter0 = *iter1;
					*iter1=temp;
				}
			}
		}
	}
}