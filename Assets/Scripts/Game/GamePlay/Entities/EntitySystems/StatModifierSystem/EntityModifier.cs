using System;

public class EntityModifier : ICommandWithUndo
{
    public Action OnEntityModifierExecute;
    public Action OnEntityModifierUndo;
    public Action<EntityModifier> ModifierCountdownFinished;

    private CountdownTimer _modifierTimer;

    public EntityModifier(Action onExecute, Action onUndo, float timerInitialValue = 0)
    {
        OnEntityModifierExecute = onExecute;
        OnEntityModifierUndo = onUndo;
        if(timerInitialValue > 0f) _modifierTimer = new CountdownTimer(timerInitialValue);
    }

    public virtual void Execute()
    {
        if(_modifierTimer != null)
        {
            _modifierTimer.TimerStoped += OnTimerStopped;
            _modifierTimer.Start();
        }
        OnEntityModifierExecute?.Invoke();
    }

    public virtual void Undo() => OnEntityModifierUndo?.Invoke();

    private void OnTimerStopped()
    {
        _modifierTimer.TimerStoped -= OnTimerStopped;
        ModifierCountdownFinished?.Invoke(this);
    }
}
