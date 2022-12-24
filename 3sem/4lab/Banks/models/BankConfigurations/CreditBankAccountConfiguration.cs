using Banks.Exceptions;

namespace Banks.Models.BankConfigurations;

public record CreditBankAccountConfiguration
{
    public CreditBankAccountConfiguration(decimal unverifiedLimit, decimal creditLimit, decimal commission)
    {
        if (creditLimit < 0)
            throw InvalidBankAccountOperationException.OnInvalidCreditLimit();

        if (creditLimit < 0)
            throw InvalidBankAccountOperationException.OnInvalidCreditLimit();

        if (commission < 0)
            throw InvalidBankAccountOperationException.OnInvalidCommission();

        UnverifiedLimit = unverifiedLimit;
        CreditLimit = creditLimit;
        Commission = commission;
    }

    public decimal UnverifiedLimit { get; }
    public decimal CreditLimit { get; }
    public decimal Commission { get; }
}