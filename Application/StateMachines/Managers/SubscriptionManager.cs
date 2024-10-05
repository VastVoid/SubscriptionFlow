namespace Application.StateMachines.Managers;

public class SubscriptionManager
{
    public SubscriptionStateMachine StateMachine;

    public SubscriptionManager(SubscriptionStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public SubscriptionStateMachine.State CurrentState => StateMachine.CurrentState;

    public bool NeedRegistration => StateMachine.CurrentState == SubscriptionStateMachine.State.NotRegistered;

    public bool CanSelectPlan => StateMachine.CanFire(SubscriptionStateMachine.Trigger.SelectPlan);

    public bool NeedPay => StateMachine.CanFire(SubscriptionStateMachine.Trigger.InitPayment)
        || StateMachine.CurrentState == SubscriptionStateMachine.State.PaymentPending;

    public bool IsSubscriptionActive => StateMachine.CurrentState == SubscriptionStateMachine.State.SubscriptionActive;

    public bool IsSubscriptionExpired => StateMachine.CurrentState == SubscriptionStateMachine.State.SubscriptionExpired;

    public bool CanCancel => StateMachine.CanFire(SubscriptionStateMachine.Trigger.CancelSubscription);
}
