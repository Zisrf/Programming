using Banks.Entities;
using Banks.Services;

namespace Banks.Console.Handlers;

public class ShowBanksHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        if (request.ToLower() == "show banks")
        {
            foreach (Bank bank in centralBank.Banks)
                System.Console.WriteLine($" * Bank '{bank.Name}' {bank.Id}");

            return;
        }

        NextHandler?.Handle(request, centralBank);
    }
}