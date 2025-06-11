using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionOnClick : MonoBehaviour
{
    public string sceneToLoad = "StrikeScene";  // 전환할 씬 이름

    public void OnClickYes()
    {
        Debug.Log("✅ [예] 버튼 클릭됨! 씬 전환 시작");
        SceneManager.LoadScene(sceneToLoad);
    }
}