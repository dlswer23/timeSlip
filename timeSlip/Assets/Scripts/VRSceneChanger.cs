using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSceneChanger : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene(SelectEnterEventArgs args)
    {
        Debug.Log("Grab Detected! Changing Scene to " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}