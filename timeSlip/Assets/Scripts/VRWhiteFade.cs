using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRWhiteFade : MonoBehaviour
{
    public Material fadeMaterial;             // 머티리얼 (URP/Unlit + Transparent)
    public float fadeDuration = 2.5f;         // 서서히 덮이는 시간
    public string sceneToLoad = "SewingMachineWorkScene"; // 전환할 씬 이름

    void Start()
    {
        // ✅ 실행 시작 시 알파값을 0으로 초기화 (투명)
        if (fadeMaterial != null)
        {
            fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, 0));
        }
    }

    public void StartWhiteFade()
    {
        Debug.Log("🔥 StartWhiteFade() 호출됨!");
        StartCoroutine(FadeAndSwitchScene());
    }

    private IEnumerator FadeAndSwitchScene()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, t / fadeDuration);

            if (fadeMaterial != null)
            {
                // 흰색 + 알파로 머티리얼 갱신
                fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, alpha));
            }

            yield return null;
        }

        Debug.Log("🚪 씬 전환 → " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
