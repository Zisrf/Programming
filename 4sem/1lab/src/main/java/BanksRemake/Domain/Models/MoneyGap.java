package BanksRemake.Domain.Models;

import BanksRemake.Domain.Exceptions.InvalidMoneyGapException;
import lombok.Getter;

/**
 * The gap between the two values of the amount of money.
 */
public class MoneyGap {
    @Getter
    private final double from;
    @Getter
    private final double to;
    @Getter
    private final double interestRate;

    /**
     * @param from the left boundary of the gap.
     * @param to the right boundary of the interval.
     * @param interestRate interest rate corresponding to the gap.
     */
    public MoneyGap(double from, double to, double interestRate) throws InvalidMoneyGapException
    {
        if (from < 0)
            throw InvalidMoneyGapException.onInvalidMoneyAmount();

        if (to < 0)
            throw InvalidMoneyGapException.onInvalidMoneyAmount();

        if (from > to)
            throw InvalidMoneyGapException.onFromSumGreaterThanTo();

        if (interestRate < 0 || interestRate > 1)
            throw InvalidMoneyGapException.onInvalidInterestRate();

        this.from = from;
        this.to = to;
        this.interestRate = interestRate;
    }
}
