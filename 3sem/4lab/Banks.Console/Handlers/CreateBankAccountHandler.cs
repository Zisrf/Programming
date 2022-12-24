using Banks.BankAccounts;
using Banks.BankAccounts.Factories;
using Banks.Entities;
using Banks.Services;

namespace Banks.Console.Handlers;

internal class CreateBankAccountHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        if (request.ToLower() == "create bank account")
        {
            System.Console.Write("Bank id: ");
            var bankId = Guid.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());
            Bank bank = centralBank.GetBankById(bankId);

            System.Console.Write("Client id: ");
            var clientId = Guid.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());
            Client client = bank.Clients.First(c => c.Id.Equals(clientId));

            System.Console.WriteLine("Account type (debit, credit or deposit): ");
            string type = System.Console.ReadLine() ?? throw new ArgumentNullException();

            IBankAccountFactory factory;
            if (type == "debit")
            {
                factory = new DebitBankAccountFactory();
            }
            else if (type == "credit")
            {
                factory = new CreditBankAccountFactory();
            }
            else if (type == "deposit")
            {
                System.Console.WriteLine("Deposit sum: ");
                decimal sum = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

                factory = new DepositBankAccountFactory(sum);
            }
            else
            {
                System.Console.WriteLine("Invalid account type");
                return;
            }

            IBankAccount account = bank.CreateAccount(factory, client, centralBank.Clock);

            System.Console.WriteLine($"Bank account {account.Id} created");
            return;
        }

        NextHandler?.Handle(request, centralBank);
    }
}