namespace Banks.Clocks;

public class FrozenTimeClock : IClock
{
    public FrozenTimeClock()
        : this(DateTime.Now) { }

    public FrozenTimeClock(DateTime startTime)
    {
        Now = startTime;
    }

    public event Action? DayChanged;
    public event Action? MonthChanged;

    public DateTime Now { get; private set; }

    public void AddTimeSpan(TimeSpan timeSpan)
    {
        DateTime newDate = Now + timeSpan;

        for (DateTime curDate = Now + TimeSpan.FromDays(1); curDate <= newDate; curDate += TimeSpan.FromDays(1))
        {
            DayChanged?.Invoke();

            if (IsFirstDayOfMonth(curDate))
                MonthChanged?.Invoke();
        }

        Now = newDate;
    }

    private static bool IsFirstDayOfMonth(DateTime date)
    {
        return date.Day == 1;
    }
}