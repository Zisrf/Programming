using Banks.Exceptions;

namespace Banks.Models.BankConfigurations;

public record DebitBankAccountConfiguration
{
    public DebitBankAccountConfiguration(decimal unverifiedLimit, decimal interestRate)
    {
        if (interestRate is < 0 or > 1)
            throw InvalidBankAccountOperationException.OnInvalidInterestRate();

        if (unverifiedLimit < 0)
            throw InvalidBankAccountOperationException.OnInvalidCreditLimit();

        UnverifiedLimit = unverifiedLimit;
        InterestRate = interestRate;
    }

    public decimal UnverifiedLimit { get; }
    public decimal InterestRate { get; }
}