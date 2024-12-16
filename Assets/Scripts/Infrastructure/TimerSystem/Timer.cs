using System;
using UnityEngine;

public class Timer
{
    protected bool _isTicking;

    protected float _initialTime;
    protected float _currentTime;
    public float CurrentTime { get { return _currentTime; } }

    public Action TimerStarted;
    public Action TimerStoped;

    public Timer(float initialTime)
    {
        _isTicking = false;
        _initialTime = initialTime;
    }

    public virtual void Tick()
    {
        if (!_isTicking) return;
        _currentTime += Time.deltaTime;
    }

    public void Start()
    {
        _currentTime = _initialTime;
        _isTicking = true;
        TimerSystem.RegisterTimer(this);
        TimerStarted?.Invoke();
    }

    public void Stop()
    {
        _isTicking = false;
        TimerSystem.UnRegisterTimer(this);
        TimerStoped?.Invoke();
    }

    public void Pause() => _isTicking = false;

    public void Resume() => _isTicking = true;
}