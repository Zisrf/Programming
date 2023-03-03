package banksRemake.domain.clocks;

/**
 * An object that responds to clock notifications.
 */
public interface ClockSubscriber {
    void reactOnDayChanged();
    void reactOnMonthChanged();
}
