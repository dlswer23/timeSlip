using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : MonoBehaviour
{
    public string sceneToLoad = "GrandMotherHouseScene2";
    public float delayInSeconds = 35f;

    void Start()
    {
        Invoke(nameof(LoadNextScene), delayInSeconds);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
