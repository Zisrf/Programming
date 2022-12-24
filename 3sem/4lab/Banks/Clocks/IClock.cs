namespace Banks.Clocks;

public interface IClock
{
    event Action DayChanged;
    event Action MonthChanged;

    DateTime Now { get; }
}