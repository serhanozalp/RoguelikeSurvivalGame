using UnityEngine;

public class CountdownTimer : Timer
{
    public CountdownTimer(float initialTime) : base(initialTime)
    {
    }

    public override void Tick()
    {
        if (!_isTicking) return;
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0) Stop();
    }
}