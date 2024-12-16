using System;

public class StatModifier : ICommandWithUndo
{
    public Action OnStatModifierExecute;
    public Action OnStatModifierUndo;
    public static Action<StatModifier> StatModifierUndid;
    private CountdownTimer _modifierTimer;

    public StatModifier(Action onExecute, Action onUndo, float timerInitialValue = 0)
    {
        OnStatModifierExecute = onExecute;
        OnStatModifierUndo = onUndo;
        if(timerInitialValue > 0f) _modifierTimer = new CountdownTimer(timerInitialValue);
    }

    public virtual void Execute()
    {
        if(_modifierTimer != null)
        {
            _modifierTimer.TimerStoped += OnTimerStopped;
            _modifierTimer.Start();
        }
        OnStatModifierExecute?.Invoke();
    }

    public virtual void Undo()
    {
        OnStatModifierUndo?.Invoke();
        StatModifierUndid.Invoke(this);
    }

    private void OnTimerStopped() => Undo();
}
