#include "ExchangeStat.h"
#define CURL_STATICLIB
#include <curl/curl.h>
#include <fstream>
#include <string>

static size_t writer(char* ptr, size_t size, size_t nmemb, std::string* data)
{
	if (data)
	{
		data->append(ptr, size * nmemb);
		return size * nmemb;
	}
	return 0;
}

void ExchangeStat::Update()
{
	using json = nlohmann::json;
	CURL* curl_handle = curl_easy_init();

	if (curl_handle)
	{
		curl_easy_setopt(curl_handle, CURLOPT_URL, "https://www.cbr-xml-daily.ru/latest.js");

		std::string data;
		curl_easy_setopt(curl_handle, CURLOPT_WRITEFUNCTION, writer);
		curl_easy_setopt(curl_handle, CURLOPT_WRITEDATA, &data);
		curl_easy_perform(curl_handle);

		json j = json::parse(data);

		add(j["rates"]);
		saveToFile();
	}

	curl_easy_cleanup(curl_handle);
}

void ExchangeStat::add(std::map<std::string, double> newStat)
{
	for (auto currency : newStat)
	{
		std::string name = currency.first;
		double value = 1 / currency.second;

		_average[name] = (_average[name] * _size[name] + value) / (_size[name] + 1);

		if (value < _median[name])
			_median[name] -= _average[name] / (_size[name] + 1);
		else
			_median[name] += _average[name] / (_size[name] + 1);

		_current[name] = value;

		++_size[name];
	}
}

void ExchangeStat::saveToFile() const
{
	std::ofstream fout("cer.data");
	for (auto i : _current)
	{
		std::string name = i.first;
		fout << name << ' ';
		fout << _current.at(name) << ' ';
		fout << _average.at(name) << ' ';
		fout << _median.at(name) << std::endl;
	}
}