#include <iostream>
#include <fstream>
#include "Parser.h"
using namespace std;
char token[10000];


int main(int argc, char* argv[])
{
	ifstream OpenFile("cpp-home.txt",ios::binary);
	Parser parser(&OpenFile);
	parser.start(0);
	OpenFile.close();
	return 0;
}
