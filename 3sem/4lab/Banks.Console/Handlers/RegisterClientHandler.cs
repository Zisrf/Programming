using Banks.Entities;
using Banks.Services;

namespace Banks.Console.Handlers;

public class RegisterClientHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        if (request.ToLower() == "register client")
        {
            System.Console.Write("Bank id: ");
            var bankId = Guid.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

            System.Console.Write("Client name: ");
            var clientName = System.Console.ReadLine() ?? throw new ArgumentNullException();

            System.Console.Write("Client surname: ");
            var clientSurname = System.Console.ReadLine() ?? throw new ArgumentNullException();

            Bank bank = centralBank.GetBankById(bankId);
            var client = new Client(clientName, clientSurname);
            bank.AddClient(client);

            System.Console.WriteLine($"Client '{clientName} {clientSurname}' {client.Id} was created");
            return;
        }

        NextHandler?.Handle(request, centralBank);
    }
}