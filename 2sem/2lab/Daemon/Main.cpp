#include "ExchangeStat.h"
#include <windows.h>

int main()
{
	FreeConsole();

	ExchangeStat st;
	while (true)
	{
		st.Update();
		Sleep(10 * 1000);
	}

	return 0;
}