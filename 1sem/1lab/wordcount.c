#include <stdio.h>
#include <string.h>

int isSeparating(char c)
{
	return c == ' ' || c == '\n' || c == '\t' || c == '.' || c == ',' || c == '!' || c == '?';
}

int main(int argc, char *argv[])
{
	if (argc != 3)
	{
		printf("Error: invalid number of arguments");
		return 1;
	}

	const char *option = argv[1];
	const char *fileName = argv[2];
	FILE *file = fopen(fileName, "r");
	if (!file)
	{
		printf("Error: unable to open file");
		return 1;
	}

	if (strcmp(option, "-l") == 0 || strcmp(option, "--lines") == 0)
	{
		int lineCount = 0;
		char curC, lastC = '\n';
		while ((curC = fgetc(file)) != EOF)
		{
			if (curC == '\n')
				++lineCount;
			lastC = curC;
		}
		if (lastC != '\n')
			++lineCount;
		printf("%d", lineCount);
	}
	else if (strcmp(option, "-c") == 0 || strcmp(option, "--bytes") == 0)
	{
		int byteCount = 0;
		char curC;
		while ((curC = fgetc(file)) != EOF)
			if (curC == '\n')
				byteCount += 2;
			else
				byteCount += 1;
		printf("%d", byteCount);
	}
	else if (strcmp(option, "-w") == 0 || strcmp(option, "--words") == 0)
	{
		int wordCount = 0;
		char curC, prevC = -1;
		while ((curC = fgetc(file)) != EOF)
		{
			if (isSeparating(curC) && !isSeparating(prevC))
				++wordCount;
			prevC = curC;
		}
		if (prevC != -1 && !isSeparating(prevC))
			++wordCount;
		printf("%d", wordCount);
	}
	else
	{
		printf("Error: invalid option");
		return 1;
	}

	fclose(file);
	return 0;
}