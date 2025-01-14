using System;

public class SequenceScope : IDisposable
{
    private Action _cleanUpAction;

    public SequenceScope(Action setupAction, Action cleanUpAction)
    {
        _cleanUpAction = cleanUpAction;
        setupAction?.Invoke();
    }

    public void Dispose()
    {
        _cleanUpAction?.Invoke();
        GC.SuppressFinalize(this);
    }
}
