namespace Application.StateMachines;

public class SubscriptionStateMachine
{
    public enum State
    {
        NotRegistered,
        Registered,
        PlanSelected,
        PaymentPending,
        PaymentSuccessed,
        PaymentFailed,
        SubscriptionActive,
        SubscriptionExpired,
        SubscriptionCancelled
    }

    public enum Trigger
    {
        Register,
        SelectPlan,
        InitPayment,
        SuccessPayment,
        FailPayment,
        ActivateSubscription,
        ExpireSubscription,
        CancelSubscription,
    }

    public State CurrentState => _currentState;

    private State _currentState;

    // Словарь переходов, где ключ - текущее состояние, 
    // а значение - возможные переходы
    private readonly Dictionary<(State, Trigger), State> _transitions;

    public SubscriptionStateMachine(State initState = State.NotRegistered)
    {
        // Начальное состояние
        _currentState = initState;

        // Инициализация возможных переходов
        _transitions = new Dictionary<(State, Trigger), State>
        {
            { 
                (State.NotRegistered, Trigger.Register), 
                State.Registered 
            },
            { 
                (State.Registered, Trigger.SelectPlan), 
                State.PlanSelected 
            },
            { 
                (State.PlanSelected, Trigger.InitPayment), 
                State.PaymentPending 
            },
            { 
                (State.PaymentPending, Trigger.SuccessPayment), 
                State.PaymentSuccessed 
            },
            { 
                (State.PaymentPending, Trigger.FailPayment), 
                State.PaymentFailed 
            },
            { 
                (State.PaymentFailed, Trigger.InitPayment), 
                State.PaymentPending 
            },
            { 
                (State.PaymentSuccessed, Trigger.ActivateSubscription), 
                State.SubscriptionActive 
            },
            { 
                (State.SubscriptionActive, Trigger.ExpireSubscription), 
                State.SubscriptionExpired 
            },
            { 
                (State.SubscriptionExpired, Trigger.InitPayment), 
                State.PaymentPending 
            },
            { 
                (State.SubscriptionExpired, Trigger.CancelSubscription), 
                State.SubscriptionCancelled 
            },
            { 
                (State.PaymentFailed, Trigger.CancelSubscription), 
                State.SubscriptionCancelled 
            },
            { 
                (State.SubscriptionCancelled, Trigger.SelectPlan), 
                State.PlanSelected 
            },
        };
    }

    // Метод для выполнения перехода
    public void Fire(Trigger trigger)
    {
        if (CanFire(trigger))
        {
            _currentState = _transitions[(_currentState, trigger)];
            Console.WriteLine($"Переход выполнен: {_currentState}");
        }
        else
        {
            Console.WriteLine("Некорректный переход:" 
                              + $"{_currentState} -> {trigger}");
        }
    }

    // Метод для проверки, возможен ли переход
    public bool CanFire(Trigger trigger) => 
        _transitions.ContainsKey((_currentState, trigger));
}
