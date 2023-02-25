package BanksRemake.Domain.Models.BankConfigurations;

/**
 * A set of parameters characterizing the credit bank account.
 * @param unverifiedLimit maximum transaction amount for an unverified user.
 * @param creditLimit the amount you can withdraw minus.
 * @param commission daily commission for being in the minus.
 */
public record CreditBankAccountConfiguration(double unverifiedLimit,
                                             double creditLimit,
                                             double commission) {
}
