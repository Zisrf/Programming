package BanksRemake.Domain.Models.BankConfigurations;

import BanksRemake.Domain.DepositInterestRateSelectors.DepositInterestRateSelector;

import java.time.Period;

/**
 * A set of parameters characterizing the deposit account.
 * @param unverifiedLimit maximum transaction amount for an unverified user.
 * @param duration time to withdraw money.
 * @param selector object that determines the interest rate.
 */
public record DepositBankAccountConfiguration(double unverifiedLimit,
                                              Period duration,
                                              DepositInterestRateSelector selector) {
}
