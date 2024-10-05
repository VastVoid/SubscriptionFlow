using SubscriptionAutomaton.Common.Clock;

namespace SubscriptionAutomaton.Common.Simulation;

public class CalendarSimulator
{
    private readonly IClock _clock;

    public CalendarSimulator(IClock clock)
    {
        _clock = clock;
    }

    public string GetCurrentTime()
    {
        return _clock.Now.ToString();
    }

    public void NextDay()
    {
        _clock.AdvanceDay();
    }

    public void NextWeek()
    {
        _clock.AdvanceWeek();
    }

    public void NextMonth()
    {
        _clock.AdvanceMonth();
    }

    public void Reset()
    {
        _clock.Reset();
    }
}
