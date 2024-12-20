using UnityEngine;

public abstract class MonoBehaviourPersistentSingleton<T> : MonoBehaviourSingleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
}

