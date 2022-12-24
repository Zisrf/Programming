using Banks.DepositInterestRateSelectors;
using Banks.Entities;
using Banks.Models;
using Banks.Models.BankConfigurations;
using Banks.Services;

namespace Banks.Console.Handlers;

public class CreateBankHandler : Handler
{
    public override void Handle(string request, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(centralBank);

        if (request.ToLower() == "create bank")
        {
            string name = ReadBankName();
            BankConfiguration configuration = ReadBankConfiguration();

            Bank bank = centralBank.CreateBank(name, configuration);
            System.Console.WriteLine($"Bank {bank.Id} '{bank.Name}' created!");

            return;
        }

        NextHandler?.Handle(request, centralBank);
    }

    private static string ReadBankName()
    {
        System.Console.Write("Name: ");
        return System.Console.ReadLine() ?? throw new ArgumentNullException();
    }

    private static BankConfiguration ReadBankConfiguration()
    {
        return new BankConfiguration(
            ReadCreditConfiguration(),
            ReadDebitConfiguration(),
            ReadDepositConfiguration());
    }

    private static DebitBankAccountConfiguration ReadDebitConfiguration()
    {
        System.Console.Write("Debit unverified limit: ");
        decimal unverifiedLimit = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

        System.Console.Write("Debit interest rate: ");
        decimal interestRate = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

        return new DebitBankAccountConfiguration(unverifiedLimit, interestRate);
    }

    private static CreditBankAccountConfiguration ReadCreditConfiguration()
    {
        System.Console.Write("Credit unverified limit: ");
        decimal unverifiedLimit = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

        System.Console.Write("Credit limit: ");
        decimal creditLimit = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

        System.Console.Write("Credit commission: ");
        decimal commission = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

        return new CreditBankAccountConfiguration(unverifiedLimit, creditLimit, commission);
    }

    private static DepositBankAccountConfiguration ReadDepositConfiguration()
    {
        System.Console.Write("Deposit unverified limit: ");
        decimal unverifiedLimit = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

        System.Console.Write("Deposit duration: ");
        var duration = TimeSpan.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

        System.Console.Write("Deposit gaps count: ");
        int gapsCount = int.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());
        DepositInterestRateSelector.DepositInterestRateSelectorBuilder builder = DepositInterestRateSelector.GetBuilder();
        for (int i = 0; i < gapsCount; ++i)
        {
            System.Console.Write($"{i + 1} gap's from: ");
            decimal from = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

            System.Console.Write($"{i + 1} gap's to: ");
            decimal to = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

            System.Console.Write($"{i + 1} gap's interest rate: ");
            decimal interestRate = decimal.Parse(System.Console.ReadLine() ?? throw new ArgumentNullException());

            builder.AddMoneyGap(new MoneyGap(from, to, interestRate));
        }

        return new DepositBankAccountConfiguration(unverifiedLimit, duration, builder.Build());
    }
}