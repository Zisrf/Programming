using Banks.Clocks;
using Banks.Console.Handlers;
using Banks.Services;

PrintGreetings();

using var clock = new RealTimeClock();
var centralBank = new CentralBank(clock);

IHandler handler = new EmptyHandler();
handler.SetNext(new HelpHandler())
    .SetNext(new ExitHandler())
    .SetNext(new CreateBankHandler())
    .SetNext(new ShowBanksHandler())
    .SetNext(new RegisterClientHandler())
    .SetNext(new ShowClientsHandler())
    .SetNext(new CreateBankAccountHandler())
    .SetNext(new LastHandler());

while (true)
{
    string? request = Console.ReadLine();

    handler.Handle(request ?? throw new ArgumentNullException(), centralBank);
}

static void PrintGreetings()
{
    Console.WriteLine("----- Welcome to Banks -----");
    Console.WriteLine();
    Console.WriteLine("Enter a command:");
}