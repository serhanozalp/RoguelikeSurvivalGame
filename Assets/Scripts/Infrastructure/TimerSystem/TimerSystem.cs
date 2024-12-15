using UnityEngine.PlayerLoop;
using UnityEngine.LowLevel;
using UnityEngine;

public static class TimerSystem
{
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

    private static void UpdateTimers()
    {
    }
}
