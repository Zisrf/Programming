using Banks.Exceptions;

namespace Banks.Models;

public readonly record struct MoneyGap
{
    public MoneyGap(decimal from, decimal to, decimal interestRate)
    {
        if (from < 0)
            throw InvalidMoneyGapException.OnInvalidMoneyAmount();

        if (to < 0)
            throw InvalidMoneyGapException.OnInvalidMoneyAmount();

        if (from > to)
            throw InvalidMoneyGapException.OnFromSumGreaterThanTo();

        if (interestRate is < 0 or > 1)
            throw InvalidMoneyGapException.OnInvalidInterestRate();

        From = from;
        To = to;
        InterestRate = interestRate;
    }

    public decimal From { get; }
    public decimal To { get; }
    public decimal InterestRate { get; }
}