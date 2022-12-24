using Banks.DepositInterestRateSelectors;
using Banks.Exceptions;

namespace Banks.Models.BankConfigurations;

public record DepositBankAccountConfiguration
{
    public DepositBankAccountConfiguration(decimal unverifiedLimit, TimeSpan duration, IDepositInterestRateSelector selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        if (unverifiedLimit < 0)
            throw InvalidBankAccountOperationException.OnInvalidCreditLimit();

        UnverifiedLimit = unverifiedLimit;
        Duration = duration;
        Selector = selector;
    }

    public decimal UnverifiedLimit { get; }
    public TimeSpan Duration { get; }
    public IDepositInterestRateSelector Selector { get; }
}