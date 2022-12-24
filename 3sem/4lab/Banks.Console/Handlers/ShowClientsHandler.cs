using Banks.Entities;
using Banks.Services;

namespace Banks.Console.Handlers;

public class ShowClientsHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        if (request.ToLower() == "show clients")
        {
            IEnumerable<Client> clients = centralBank.Banks.SelectMany(b => b.Clients);

            foreach (Client client in clients)
                System.Console.WriteLine($" * Client '{client.Name} {client.Surname}' {client.Id}");

            return;
        }

        NextHandler?.Handle(request, centralBank);
    }
}