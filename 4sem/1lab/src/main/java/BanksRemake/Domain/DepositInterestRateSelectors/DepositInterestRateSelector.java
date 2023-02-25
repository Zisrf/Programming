package BanksRemake.Domain.DepositInterestRateSelectors;

import BanksRemake.Domain.Models.MoneyGap;

import java.util.List;

/**
 * Object that determines the interest rate.
 */
public interface DepositInterestRateSelector {
    List<MoneyGap> getMoneyGaps();
    double getInterestRate(double moneyAmount);
}
