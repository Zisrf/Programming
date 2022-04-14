#pragma once
#include <json/json.hpp>
#include <string>
#include <map>

struct ExchangeStat
{
private:
	std::map<std::string, double> _size;
	std::map<std::string, double> _current;
	std::map<std::string, double> _average;
	std::map<std::string, double> _median;

public:
	ExchangeStat() = default;
	void Update();

private:
	void add(std::map<std::string, double> newStat);
	void saveToFile() const;
};