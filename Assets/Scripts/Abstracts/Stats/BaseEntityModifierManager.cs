using System.Collections.Generic;

public abstract class BaseEntityModifierManager
{
    protected readonly List<EntityModifier> _modifierList;

    public BaseEntityModifierManager()
    {
        _modifierList = new List<EntityModifier>();
    }

    public virtual void AddModifier(EntityModifier modifier)
    {
        if (!_modifierList.Contains(modifier))
        {
            _modifierList.Add(modifier);
            modifier.ModifierCountdownFinished += OnModifierCountdownFinished;
            modifier.Execute();
        }
    }

    public virtual void RemoveModifier(EntityModifier modifier)
    {
        _modifierList.Remove(modifier);
        modifier.ModifierCountdownFinished -= OnModifierCountdownFinished;
        modifier.Undo();
    }

    private void OnModifierCountdownFinished(EntityModifier modifier) => RemoveModifier(modifier);
}
