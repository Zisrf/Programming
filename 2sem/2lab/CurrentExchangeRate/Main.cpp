#include "Daemon.h"
#include <iostream>
#include <fstream>
#include <string>
#include <string.h>

int main(int argc, char* argv[])
{
	if (argc == 1)
	{
		std::cout << "Error: no arguments" << std::endl;
		return 1;
	}

	std::cout.precision(5);
	std::cout.setf(std::ios::fixed);
	if (strcmp(argv[1], "--start") == 0)
	{
		StartDaemon();
	}
	else if (strcmp(argv[1], "--stop") == 0)
	{
		KillDaemon();

		std::ifstream fin("cer.data");
		if (!fin.is_open())
		{
			std::cout << "Error: unable to open \"cer.data\"" << std::endl;
			return 2;
		}

		std::string name;
		double current;
		double average;
		double median;

		std::cout << "Name" << "\t| ";
		std::cout << "current" << "\t| ";
		std::cout << "average" << "\t| ";
		std::cout << "median" << std::endl;

		for (int i = 0; i < 56; ++i)
			std::cout << '-';
		std::cout << std::endl;

		while (fin >> name >> current >> average >> median)
		{
			std::cout << name << "\t| ";
			std::cout << current << "\t| ";
			std::cout << average << "\t| ";
			std::cout << median << std::endl;
		}
	}
	else if (strcmp(argv[1], "--get") == 0)
	{
		if (argc < 3)
		{
			std::cout << "Error: no currency name" << std::endl;
			return 3;
		}

		std::ifstream fin("cer.data");
		if (!fin.is_open())
		{
			std::cout << "The file \"cer.data\" is damaged" << std::endl;
			return 4;
		}

		std::string name;
		double current;
		double average;
		double median;

		while (fin >> name >> current >> average >> median)
			if (name == argv[2])
				break;

		if (name == argv[2])
		{
			std::cout << "Name" << "\t| ";
			std::cout << "current" << "\t| ";
			std::cout << "average" << "\t| ";
			std::cout << "median" << std::endl;

			for (int i = 0; i < 56; ++i)
				std::cout << '-';
			std::cout << std::endl;

			std::cout << name << "\t| ";
			std::cout << current << "\t| ";
			std::cout << average << "\t| ";
			std::cout << median << std::endl;
		}
		else
		{
			std::cout << "Error: invalid currency name" << std::endl;
			return 5;
		}
	}

	return 0;
}