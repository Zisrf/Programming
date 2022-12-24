namespace Banks.Clocks;

public class RealTimeClock : IClock, IDisposable
{
    private bool _disposed;

    public RealTimeClock()
    {
        _disposed = false;

        StartTicks();
    }

    public event Action? DayChanged;
    public event Action? MonthChanged;

    public DateTime Now => DateTime.Now;

    public void Dispose()
    {
        _disposed = true;
    }

    private async void StartTicks()
    {
        const int dayInMilliseconds = 1000 * 60 * 60 * 24;

        while (!_disposed)
        {
            await Task.Delay(dayInMilliseconds);

            DayChanged?.Invoke();

            if (IsFirstDayOfMonth())
                MonthChanged?.Invoke();
        }
    }

    private bool IsFirstDayOfMonth()
    {
        return Now.Day == 1;
    }
}