package BanksRemake.Domain.Clocks;

/**
 * An object that responds to clock notifications.
 */
public interface ClockSubscriber {
    void reactOnDayChanged();
    void reactOnMonthChanged();
}
