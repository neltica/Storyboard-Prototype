#pragma once
class ImageBinary
{
private:
	char* binary;
public:
	void setBinary(const char* p);
	char* getBinary();

	ImageBinary(void);
	~ImageBinary(void);
};

