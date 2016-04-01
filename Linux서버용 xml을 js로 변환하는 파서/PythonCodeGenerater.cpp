#include "PythonCodeGenerater.h"


PythonCodeGenerater::PythonCodeGenerater(void)
{
}


PythonCodeGenerater::~PythonCodeGenerater(void)
{
	output.close();
}


PythonCodeGenerater::PythonCodeGenerater(BaseProgram *_baseProgram)
{
	baseProgram=_baseProgram;
	generaterStart();
}

void PythonCodeGenerater::generaterStart()
{
	output.open("prototype.py");
	output<<"import Component"<<endl;
	output<<"import Item"<<endl;
	output<<"import tkinter"<<endl;
	output<<"import Event"<<endl;


	cout<<endl<<"import Component"<<endl;
	cout<<"import Item"<<endl;
	cout<<"import tkinter"<<endl;
	cout<<"import Event"<<endl;
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

	output<<"tkinter.mainloop()"<<endl;

	cout<<"tkinter.mainloop()"<<endl;
}

void PythonCodeGenerater::windows()
{

	PythonStringArray pythonStringArray;  //vector�� �����ϱ� ���� �ӽ÷� �ʿ��� ����

	list<MyComponent> tempComponent=baseProgram->getList();  //������Ʈ iterator�� ����
	int componentIndex=0;  //iterator �ε����� ���� ���� ����

	for(list<MyComponent>::iterator iter0=tempComponent.begin();iter0!=tempComponent.end();iter0++)   //Component
	{
		//cout<<(*iter0).getPythonString(WINDOW_FORM,componentIndex)<<endl;
		pythonStringArray.setValue(componentIndex,(*iter0).getPythonString(WINDOW_FORM,componentIndex),0);  //�ӽú����� �����͸� ä������
		strList.push_back(pythonStringArray);   //vector�� ����

		list<MyItem> tempItem= (*iter0).getList();  //������ iterator�� ����
		//int itemIndex=0;
		for(list<MyItem>::iterator iter1=tempItem.begin(); iter1!=tempItem.end();iter1++)    //Item
		{
			//cout<<(*iter1).getPythonString((*iter0).getName(),itemIndex)<<endl;
			pythonStringArray.setValue(componentIndex,(*iter1).getPythonString((*iter0).getName(),componentIndex),1);  //�ӽú����� ������ ä������
			strList.push_back(pythonStringArray);  //vector�� ����

			list<MyEvent> tempEvent= (*iter1).getList(); //�̺�Ʈ iterator ����
			//int eventIndex=0;
			for(list<MyEvent>::iterator iter2=tempEvent.begin(); iter2!=tempEvent.end();iter2++)  //Event
			{
				//cout<<(*iter2).getPythonString((*iter1).getName(),itemIndex)<<endl;
				pythonStringArray.setValue(componentIndex,(*iter2).getPythonString((*iter1).getName(),componentIndex),2);  //������ ä��
				strList.push_back(pythonStringArray);  //����
				//eventIndex++;
			}
		//	itemIndex++;
		}
		componentIndex++;  //�ε��� ++
	}


	sort();  //������Ʈ, ������, �̺�Ʈ ������ ������
	for(vector<PythonStringArray>::iterator iter=strList.begin();iter!=strList.end();iter++)
	{
		cout<<(*iter).getType()<<" "<<(*iter).getNum()<<" "<<(*iter).getStr()<<endl;
		output<<(*iter).getStr()<<endl;
	}

	//�����۵��� ������Ʈ�� ��ġ��Ŵ
	for(vector<PythonStringArray>::iterator iter=strList.begin();iter!=strList.end();iter++)
	{
		if((*iter).getType()==1)
		{
			cout<<(*iter).getType()<<" "<<(*iter).getNum()<<" "<<(*iter).getStr().substr(0,(*iter).getStr().find("="))<<".placeItem()"<<endl;
			output<<(*iter).getStr().substr(0,(*iter).getStr().find("="))<<".placeItem()"<<endl;
		}
	}

}
void PythonCodeGenerater::android()
{
}
void PythonCodeGenerater::web()
{

}
void PythonCodeGenerater::tizen()
{

}

void PythonCodeGenerater::sort()
{
	PythonStringArray temp;
	for(vector<PythonStringArray>::iterator iter0=strList.begin();iter0!=strList.end();iter0++)
	{
		for(vector<PythonStringArray>::iterator iter1=iter0;iter1!=strList.end();iter1++)
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
					PythonStringArray temp= *iter0;
					*iter0 = *iter1;
					*iter1=temp;
				}
			}
		}
	}
}
