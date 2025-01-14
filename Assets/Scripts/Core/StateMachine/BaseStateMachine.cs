using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

public abstract class BaseStateMachine : BaseState
{
    protected BaseState _currentState;
    protected readonly Dictionary<Type, BaseState> _stateDictionary;

    protected bool _isStateTransitionLocked;
    public bool IsStateTransitionLocked { get => _isStateTransitionLocked; set { _isStateTransitionLocked = value; } }

    protected BaseStateMachine(BaseStateMachine parentStateMachine) : base(parentStateMachine)
    {
        _stateDictionary = new Dictionary<Type, BaseState>();
    }

    protected void AddState<T>(T state) where T : BaseState
    {
        if (_stateDictionary.ContainsKey(typeof(T))) return;
        _stateDictionary.Add(typeof(T), state);
    }

    public void ChangeState<T>() where T : BaseState
    {
        if (!_stateDictionary.ContainsKey(typeof(T))) throw new ArgumentException("Stete Not Found!");
        if (_currentState == _stateDictionary[typeof(T)] || _isStateTransitionLocked) return;
        _currentState?.Exit();
        _currentState = _stateDictionary[typeof(T)];
        _currentState.Enter();
    }

    public async UniTask ChangeStateAsync<T>() where T : BaseState
    {
        if (!_stateDictionary.ContainsKey(typeof(T))) throw new ArgumentException("Stete Not Found!");
        if (_currentState == _stateDictionary[typeof(T)] || _isStateTransitionLocked) return;
        if (_currentState != null) await _currentState.EnterAsync();
        _currentState = _stateDictionary[typeof(T)];
        await _currentState.EnterAsync();
    }

    public override void OnUpdate()
    {
        _currentState?.OnUpdate();
    }
}
