namespace SubscriptionAutomaton.Common.Clock;

public interface IClock
{
    DateTime Now { get; }
    void AdvanceDay();
    void AdvanceWeek();
    void AdvanceMonth();
    void Reset();
}
