#include "Parser.h"
#include <iostream>

using namespace std;


Parser::Parser(void)
{
}

Parser::Parser(ifstream* _input)
{
	getFile = _input;
}


Parser::~Parser(void)
{
}

void Parser::start(int flag)
{
	temp=new char[500];
	int x,y;
	try
	{
		checkTag("<Information>");
		checkTag("<type>");
		get_token(temp);
		int componentTypeInt=checkComponentType(temp);
		baseProgram.setType(componentTypeInt);
		get_token(temp);  //</type>
		get_token(temp);  //</Information>

		while(get_token(temp)[0]!=-1)
		{
			preComponent=new MyComponent();

			checkTag("<name>");
			get_token(temp);
			preComponent->setName(temp);
			get_token(temp);

			checkTag("<position>");
			get_token(temp);
			x=atof(strtok(temp,","));
			y=atof(strtok(NULL,","));
			preComponent->setPosition(x,y);
			get_token(temp);

			checkTag("<size>");
			get_token(temp);
			x=atof(strtok(temp,","));
			y=atof(strtok(NULL,","));
			preComponent->setSize(x,y);
			get_token(temp);

			checkTag("<background>");

			if(!checkTag("<rgb>"))
			{
				get_token(temp);
				preComponent->setRgb(temp);
			}
			else  //bitmap
			{

			}
			get_token(temp);  //</rgb> or </bitmap>
			get_token(temp);  //</background>

			checkTag("<itemList>");
			while(!strcmp(get_token(temp),"<item>"))
			{
				preItem=new MyItem();

				checkTag("<category>");
				get_token(temp);
				int itemCateInt = checkItemCategory(temp);
				preItem->setCategory(itemCateInt);
				get_token(temp);  //</category>
				////////////////////////////////////////////////////////////////////
				checkTag("<name>");
				get_token(temp);
				preItem->setName(temp);
				get_token(temp);

				checkTag("<position>");
				get_token(temp);
				x=atof(strtok(temp,","));
				y=atof(strtok(NULL,","));
				preItem->setPosition(x,y);
				get_token(temp);

				checkTag("<size>");
				get_token(temp);
				x=atof(strtok(temp,","));
				y=atof(strtok(NULL,","));
				preItem->setSize(x,y);
				get_token(temp);

				checkTag("<background>");

				if(!checkTag("<rgb>"))
				{
					get_token(temp);
					preItem->setRgb(temp);
				}
				else  //bitmap
				{

				}
				get_token(temp);  //</rgb> or </bitmap>
				get_token(temp);  //</background>

				//독립성분 파싱시작
				switch(itemCateInt)
				{
				case BUTTON:
					checkTag("<text>");
					get_token(temp);
					//preComponent->setName(temp);
					preItem->setText(temp);
					get_token(temp);

					break;
				case TEXTBOX:
					checkTag("<text>");
					get_token(temp);
					//preComponent->setName(temp);
					preItem->setText(temp);
					get_token(temp);

					break;
				case TEXTBLOCK:
					checkTag("<text>");
					get_token(temp);
					//preComponent->setName(temp);
					preItem->setText(temp);
					get_token(temp);

					break;
				case SLIDEVIEW:
					checkTag("<ViewList>");
					while(!strcmp(get_token(temp),"<vl>"))
					{
						checkTag("<binary>");
						checkTag("</binary>");


						checkTag("<size>");
						get_token(temp);
						x=atof(strtok(temp,","));
						y=atof(strtok(NULL,","));
						preItem->setvlSize(x,y);
						checkTag("</size>");

						checkTag("</vl>");
					}
					if(strcmp(temp,"</ViewList>"))
					{
						throw -1;
					}
					break;
				case LISTVIEW:
					checkTag("<Table>");
					get_token(temp);
					x=atof(strtok(temp,","));
					y=atof(strtok(NULL,","));
					preItem->setTable(x,y);
					checkTag("</Table>");

					checkTag("<TableSize>");
					get_token(temp);
					x=atof(strtok(temp,","));
					y=atof(strtok(NULL,","));
					preItem->setTableSize(x,y);
					checkTag("</TableSize>");

					checkTag("<ViewList>");
					while(!strcmp(get_token(temp),"<vl>"))
					{
						checkTag("<binary>");
						checkTag("</binary>");

						checkTag("<size>");
						get_token(temp);
						x=atof(strtok(temp,","));
						y=atof(strtok(NULL,","));
						preItem->setvlSize(x,y);
						checkTag("</size>");

						checkTag("</vl>");
					}
					if(strcmp(temp,"</ViewList>"))
					{
						throw -1;
					}
					break;
				case LISTBOX:
					checkTag("<BoxList>");
					while(!strcmp(get_token(temp),"<bl>"))
					{
						checkTag("<text>");
						get_token(temp);
						preItem->setText(temp);
						checkTag("</text>");

						checkTag("</bl>");
					}
					if(strcmp(temp,"</BoxList>"))
					{
						throw -1;
					}
					break;
				case COMBOBOX:
					checkTag("<text>");
					get_token(temp);
					//preComponent->setName(temp);
					preItem->setText(temp);
					get_token(temp);

					break;
				case CHECKBOX:
					checkTag("<text>");
					get_token(temp);
					//preComponent->setName(temp);
					preItem->setText(temp);
					get_token(temp);
					break;
				}
				//종료
				////////////////////////////////////////////////////////////////////
				checkTag("<eventList>");
				while(!strcmp(get_token(temp),"<el>"))
				{
					preEvent=new MyEvent();

					checkTag("<category>");
					get_token(temp);
					int eventCateInt = checkEventCategory(temp);
					switch(eventCateInt)
					{
					case ONCLICK:
						preEvent->setCategory(eventCateInt);
						get_token(temp);

						checkTag("<act>");
						get_token(temp);
						preEvent->setAct(temp);
						get_token(temp);

						checkTag("<passComponent>");
						if(!strcmp(preEvent->getAct(),"onclick"))
						{
							checkTag("<name>");
							get_token(temp);
							preEvent->setPassComponentName(temp);
							get_token(temp);
						}
						checkTag("</passComponent>");

						checkTag("</el>");
						tmpEvent.push_back(*preEvent);
						break;
					}
				}
				//checkTag("</eventList>");
				////////////////////////////////////////////////////////////////////

				checkTag("</item>");
				preItem->setListEvent(tmpEvent);
				tmpEvent.clear();
				tmpItem.push_back(*preItem);
			}
			checkTag("</Component>");
			preComponent->setListItem(tmpItem);
			tmpItem.clear();
			tmpComponent.push_back(*preComponent);
		}
		baseProgram.setListComponent(tmpComponent);
		tmpComponent.clear();
	}
	catch(int exception)
	{
		cout<<"syntax error"<<endl;
		return;
	}



	//baseProgram.setListComponent(tmpComponent);

	baseProgram.show();

	if(flag==0)
	{
		pythonCodeGenerater=new PythonCodeGenerater(&baseProgram);
	}
	else
	{
		javaScriptCodeGenerator=new JavaScriptCodeGenerator(&baseProgram);
	}


}

char* Parser::get_token(char* _token)
{
	char ch;
	int size=0;
	while(1)
	{
		if(getFile->get(ch)==0)
		{
			_token[0] = EOF;
			return _token;
		}
		else if(ch!=' ' && ch!='\n' && ch!='\t' && ch!='\r')
		{
			_token[size++]=ch;
			break;
		}
	}
	if(_token[0]=='<')
	{
		while(ch!='>')
		{
			getFile->get(ch);
			if(ch==' ' || ch=='\n' || ch=='\t' || ch=='\r')
				continue;
			_token[size++]=ch;
		}
	}
	else
	{
		while(ch!='<')
		{
			getFile->get(ch);
			if(ch==' ' || ch=='\n' || ch=='\t' || ch=='\r')
				continue;
			_token[size++]=ch;
		}
		size--;
		getFile->seekg(-1,ios::cur);
	}
	_token[size]='\0';
	return _token;
}


int Parser::checkTag(char* _tag)
{
	get_token(temp);
	cout<<temp<<endl;
	if(strcmp(temp,_tag))
	{
        cout<<_tag<<endl;
		throw -1;
	}
	return 0;
}


int Parser::checkItemCategory(char* _temp)
{
	char* str[]={"button","textbox","textblock","image","slideview","listview","combobox","checkbox","\0"};

	for(int i=0;strcmp(str[i],"\0");i++)
	{
		if(!strcmp(_temp,str[i]))
		{
			return i;
		}
	}
	return -1;
}

int Parser::checkEventCategory(char* _temp)
{
	char* str[]={"click","\0"};

	for(int i=0;strcmp(str[i],"\0");i++)
	{
		if(!strcmp(_temp,str[i]))
		{
			return i;
		}
	}
	return -1;
}

int Parser::checkComponentType(char* _temp)
{
	char *str[]={"Android","Tizen","Windows","Web","\0"};

	for(int i=0;strcmp(str[i],"\0");i++)
	{
		if(!strcmp(_temp,str[i]))
		{
			return i;
		}
	}
	return -1;
}
