package BanksRemake.Domain.Clocks;

import java.time.Period;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;

/**
 * A type of clock that allows you to stop time and add certain periods of time.
 */
public class FrozenTimeClock implements Clock {
    private final ArrayList<ClockSubscriber> subscribers;
    private Date nowDate;

    public FrozenTimeClock(Date startTime) {
        this.nowDate = startTime;

        subscribers = new ArrayList<>();
    }

    /**
     * Shifts the current clock time by a specified time interval.
     * @param period time interval to add.
     */
    public void addPeriod(Period period) {
        var newDate = nowDate.toInstant().plus(period);

        for (var curDate = nowDate.toInstant(); curDate.isBefore(newDate); curDate = curDate.plus(Period.ofDays(1))){
            notifySubscribersThatDayChanged();

            if (curDate.atZone(ZoneId.systemDefault()).getDayOfMonth() == 1) {
                notifySubscribersThatMonthChanged();
            }
        }

        nowDate = Date.from(newDate);
    }

    @Override
    public Date getNowDate() {
        return nowDate;
    }

    @Override
    public void addSubscriber(ClockSubscriber subscriber) {
        subscribers.add(subscriber);
    }

    @Override
    public void removeSubscriber(ClockSubscriber subscriber) {
        subscribers.remove(subscriber);
    }

    private void notifySubscribersThatDayChanged() {
        for (var subscriber : subscribers) {
            subscriber.reactOnDayChanged();
        }
    }

    private void notifySubscribersThatMonthChanged() {
        for (var subscriber : subscribers) {
            subscriber.reactOnMonthChanged();
        }
    }
}
