package BanksRemake.Domain.Clocks;

import java.util.Date;

/**
 * A clock that notifies accounts of the arrival of a new day or month.
 */
public interface Clock {
    /**
     * @return the current date of the clock.
     */
    Date getNowDate();

    void addSubscriber(ClockSubscriber subscriber);
    void removeSubscriber(ClockSubscriber subscriber);
}
