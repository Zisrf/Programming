using Banks.Models;

namespace Banks.DepositInterestRateSelectors;

public interface IDepositInterestRateSelector
{
    public IReadOnlyList<MoneyGap> Gaps { get; }

    public decimal GetInterestRate(decimal moneyAmount);
}