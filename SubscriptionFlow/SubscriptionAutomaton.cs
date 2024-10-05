namespace SubscriptionFlow.Subscriptions;

public enum SubscriptionState
{
    NotRegistered,
    Registered,
    PlanSelected,
    PaymentCompleted,
    SubscriptionActive,
    SubscriptionExpired
}

public enum SubscriptionEvent
{
    RegistrationCompleted,
    PlanSelected,
    PaymentCompleted,
    SubscriptionExpired
}

public class SubscriptionAutomaton
{
    public SubscriptionState CurrentState { get; private set; } = SubscriptionState.NotRegistered;

    public void HandleEvent(SubscriptionEvent subscriptionEvent)
    {
        switch (CurrentState)
        {
            case SubscriptionState.NotRegistered:
                if (subscriptionEvent == SubscriptionEvent.RegistrationCompleted)
                    CurrentState = SubscriptionState.Registered;
                break;

            case SubscriptionState.Registered:
                if (subscriptionEvent == SubscriptionEvent.PlanSelected)
                    CurrentState = SubscriptionState.PlanSelected;
                break;

            case SubscriptionState.PlanSelected:
                if (subscriptionEvent == SubscriptionEvent.PaymentCompleted)
                    CurrentState = SubscriptionState.SubscriptionActive;
                break;

            case SubscriptionState.SubscriptionActive:
                if (subscriptionEvent == SubscriptionEvent.SubscriptionExpired)
                    CurrentState = SubscriptionState.SubscriptionExpired;
                break;

                // Здесь можно добавлять дополнительные переходы, если нужно
        }
    }
}