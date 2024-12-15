using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviourPersistentSingleton<ApplicationManager>
{
    protected override void Awake()
    {
        base.Awake();
        SceneManager.LoadScene(1);
    }
}
