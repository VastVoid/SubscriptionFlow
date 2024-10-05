namespace SubscriptionAutomaton.Common.Clock;

public class SimulatedClock : IClock
{
    public DateTime Now => DateTime.Now + offset;
    private TimeSpan offset;

    public SimulatedClock()
    {
        SetNow();
    }

    public SimulatedClock(DateTime startTime)
    {
        offset = startTime - DateTime.Now;
    }

    public void AdvanceDay()
    {
        offset = offset.Add(TimeSpan.FromDays(1));
    }

    public void AdvanceWeek()
    {
        offset = offset.Add(TimeSpan.FromDays(7));
    }

    public void AdvanceMonth()
    {
        offset = offset.Add(Now.AddMonths(1) - Now);
    }

    public void Reset()
    {
        SetNow();
    }

    private void SetNow()
    {
        offset = TimeSpan.Zero;
    }
}
