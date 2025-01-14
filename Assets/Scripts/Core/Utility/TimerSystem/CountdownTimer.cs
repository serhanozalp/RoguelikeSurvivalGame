using UnityEngine;

public class CountdownTimer : Timer
{
    public CountdownTimer(float initialTime) : base(initialTime)
    {
    }

    public override void Tick()
    {
        if (!_isTicking) return;
        if(_currentTime > 0f) _currentTime = Mathf.Max(_currentTime - Time.deltaTime, 0f);
        else if (_currentTime == 0) Stop();
    }
}