package banksRemake.domain.models.bankConfigurations;

/**
 * A set of parameters characterizing the bank products.
 * @param creditBankAccountConfiguration a set of parameters characterizing the credit account.
 * @param debitBankAccountConfiguration a set of parameters characterizing the debit account.
 * @param depositBankAccountConfiguration a set of parameters characterizing the deposit account.
 */
public record BankConfiguration(CreditBankAccountConfiguration creditBankAccountConfiguration,
                                DebitBankAccountConfiguration debitBankAccountConfiguration,
                                DepositBankAccountConfiguration depositBankAccountConfiguration) {
}