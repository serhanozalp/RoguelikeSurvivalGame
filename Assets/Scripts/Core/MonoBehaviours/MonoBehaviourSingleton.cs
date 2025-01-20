using UnityEditor;
using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (Instance == null) Instance = this as T;
        else
        {
            Debug.LogWarning($"Destroying {typeof(T)}. It already exists in the scene.");
            Destroy(this);
        }
    }
#if UNITY_EDITOR
    private void Reset()
    {
        if (FindObjectsByType<T>(FindObjectsSortMode.None).Length > 1)
        {
            Debug.LogWarning($"Destroying {typeof(T)}. It already exists in the scene.");
            EditorApplication.delayCall += () => DestroyImmediate(this);
        }
    }
#endif
}