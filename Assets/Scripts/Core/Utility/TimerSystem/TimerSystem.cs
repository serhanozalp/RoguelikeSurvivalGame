using UnityEngine.PlayerLoop;
using UnityEngine.LowLevel;
using UnityEngine;
using System.Collections.Generic;

public static class TimerSystem
{
    private static readonly List<Timer> _timerList = new List<Timer>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
        PlayerLoopSystem timerManagerSystem = new PlayerLoopSystem { subSystemList = null, type = typeof(TimerSystem), updateDelegate = TimerSystem.UpdateTimers };
        if (!timerManagerSystem.IsSystemPresent(currentPlayerLoop))
        {
            if (timerManagerSystem.TryInsertSystem<Update>(ref currentPlayerLoop, 0)) PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            else Debug.LogWarning("Couldn't Insert TimerManager to Current Player Loop!");
        }
        else
        {
            Debug.LogWarning("TimerManager Already Exists in Current Player Loop!");
        }
    }

    public static void RegisterTimer(Timer timer)
    {
        if (!_timerList.Contains(timer)) _timerList.Add(timer);
    }

    public static void UnRegisterTimer(Timer timer) => _timerList.Remove(timer);
    private static void UpdateTimers()
    {
        for (int i = 0; i < _timerList.Count; i++)
        {
            _timerList[i]?.Tick();
        }
    }
}