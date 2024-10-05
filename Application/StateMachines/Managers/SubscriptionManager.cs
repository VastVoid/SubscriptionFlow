namespace Application.StateMachines.Managers;

public class SubscriptionManager
{
    private SubscriptionStateMachine _stateMachine;

    public SubscriptionManager(SubscriptionStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

    }

    public SubscriptionStateMachine.State CurrentState => _stateMachine.CurrentState;

    public bool NeedRegistration => _stateMachine.CurrentState == SubscriptionStateMachine.State.NotRegistered;

    public bool CanSelectPlan => _stateMachine.CanFire(SubscriptionStateMachine.Trigger.SelectPlan);

    public bool NeedPay => _stateMachine.CanFire(SubscriptionStateMachine.Trigger.InitPayment)
        || _stateMachine.CurrentState == SubscriptionStateMachine.State.PaymentPending;

    public bool IsSubscriptionActive => _stateMachine.CurrentState == SubscriptionStateMachine.State.SubscriptionActive;

    public bool IsSubscriptionExpired => _stateMachine.CurrentState == SubscriptionStateMachine.State.SubscriptionExpired;

    public bool CanCancel => _stateMachine.CanFire(SubscriptionStateMachine.Trigger.CancelSubscription);
}
