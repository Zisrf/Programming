package banksRemake.domain.depositInterestRateSelectors;

import banksRemake.domain.exceptions.InvalidDepositInterestRateSelectorException;
import banksRemake.domain.models.MoneyGap;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * Implementation of DepositInterestRateSelector.
 */
public class DepositInterestRateSelectorImpl implements DepositInterestRateSelector {
    private final ArrayList<MoneyGap> moneyGaps;

    private DepositInterestRateSelectorImpl(ArrayList<MoneyGap> moneyGaps) {
        this.moneyGaps = moneyGaps;
    }

    public static DepositInterestRateSelectorBuilder getBuilder() {
        return new DepositInterestRateSelectorBuilder();
    }

    @Override
    public List<MoneyGap> getMoneyGaps() {
        return Collections.unmodifiableList(moneyGaps);
    }

    @Override
    public double getInterestRate(double moneyAmount) {
        for (MoneyGap gap : moneyGaps) {
            if (moneyAmount >= gap.getFrom() && moneyAmount <= gap.getTo())
                return gap.getInterestRate();
        }

        return moneyGaps.get(moneyGaps.size() - 1).getInterestRate();
    }

    public static class DepositInterestRateSelectorBuilder
    {
        private final ArrayList<MoneyGap> moneyGaps;

        public DepositInterestRateSelectorBuilder() {
            moneyGaps = new ArrayList<>();
        }

        public DepositInterestRateSelectorBuilder addMoneyGap(MoneyGap moneyGap) throws InvalidDepositInterestRateSelectorException {
            if (moneyGaps.size() > 0 && moneyGaps.get(moneyGaps.size() - 1).getTo() > moneyGap.getFrom()) {
                throw InvalidDepositInterestRateSelectorException.onGapsIntersection();
            }

            moneyGaps.add(moneyGap);

            return this;
        }

        public DepositInterestRateSelector build() throws InvalidDepositInterestRateSelectorException {
            if (moneyGaps.size() == 0) {
                throw InvalidDepositInterestRateSelectorException.onEmptyGapsList();
            }

            return new DepositInterestRateSelectorImpl(moneyGaps);
        }
    }
}
