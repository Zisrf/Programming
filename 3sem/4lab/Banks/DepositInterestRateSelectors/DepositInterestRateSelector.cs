using Banks.Exceptions;
using Banks.Models;

namespace Banks.DepositInterestRateSelectors;

public class DepositInterestRateSelector : IDepositInterestRateSelector
{
    private DepositInterestRateSelector(IReadOnlyList<MoneyGap> gaps)
    {
        ArgumentNullException.ThrowIfNull(gaps);

        Gaps = gaps;
    }

    public IReadOnlyList<MoneyGap> Gaps { get; }

    public static DepositInterestRateSelectorBuilder GetBuilder()
    {
        return new DepositInterestRateSelectorBuilder();
    }

    public decimal GetInterestRate(decimal moneyAmount)
    {
        foreach (MoneyGap gap in Gaps)
        {
            if (moneyAmount >= gap.From && moneyAmount <= gap.To)
                return gap.InterestRate;
        }

        return Gaps[^1].InterestRate;
    }

    public class DepositInterestRateSelectorBuilder
    {
        private readonly List<MoneyGap> _gaps;

        public DepositInterestRateSelectorBuilder()
        {
            _gaps = new List<MoneyGap>();
        }

        public DepositInterestRateSelectorBuilder AddMoneyGap(MoneyGap moneyGap)
        {
            if (_gaps.Count > 0 && _gaps[^1].To > moneyGap.From)
                throw InvalidDepositInterestRateSelectorException.OnGapsIntersection();

            _gaps.Add(moneyGap);

            return this;
        }

        public DepositInterestRateSelector Build()
        {
            if (_gaps.Count == 0)
                throw InvalidDepositInterestRateSelectorException.OnEmptyGapsList();

            return new DepositInterestRateSelector(_gaps);
        }
    }
}