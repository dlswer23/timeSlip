using UnityEngine;
using UnityEngine.SceneManagement;

public class StrikeConfirm : MonoBehaviour
{
    public string sceneToLoad = "StrikeScene";

    public void OnClickYes()
    {
        Debug.Log("🪧 파업 시위 씬으로 전환!");
        SceneManager.LoadScene(sceneToLoad);
    }
}
