using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviourPersistentSingleton<ApplicationManager>
{
    protected override void Awake()
    {
        base.Awake();
        EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
        Application.targetFrameRate = 144;
        //Test
        SceneManager.LoadScene(1);
    }

    private void EditorApplication_playModeStateChanged(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.ExitingPlayMode:
                RestorePlayerLoop();
                break;
        }
    }

    private void RestorePlayerLoop()
    {
        PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
    }

    private void OnDestroy()
    {
        EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
    }
}
