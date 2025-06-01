using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRWhiteFade : MonoBehaviour
{
    public Material fadeMaterial;                // Fade용 머티리얼 (URP/Unlit + Transparent)
    public float fadeDuration = 2.5f;            // 페이드 연출 지속 시간
    public string sceneToLoad = "SewingMachineWorkScene"; // 전환할 씬 이름
    public AudioSource bgmSource;                // 🎵 배경음 AudioSource
    public float bgmFadeDuration = 2f;           // 배경음 페이드아웃 시간

    void Start()
    {
        // 실행 시작 시 알파값을 0으로 초기화 (투명하게 시작)
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

        // 🎧 BGM 페이드아웃 시작
        if (bgmSource != null && bgmSource.isPlaying)
        {
            StartCoroutine(FadeOutBGM());
        }

        // 🎬 화면 화이트 페이드
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, t / fadeDuration);

            if (fadeMaterial != null)
            {
                fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, alpha));
            }

            yield return null;
        }

        Debug.Log("🚪 씬 전환 → " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator FadeOutBGM()
    {
        float startVolume = bgmSource.volume;
        float t = 0f;

        while (t < bgmFadeDuration)
        {
            t += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / bgmFadeDuration);
            yield return null;
        }

        bgmSource.Stop();
    }
}
