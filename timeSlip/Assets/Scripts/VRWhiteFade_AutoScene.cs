using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRWhiteFade_AutoScene : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeDuration = 2.5f;
    public string sceneToLoad = "GrandMotherHouseScene2";

    public AudioSource bgmSourceMain; // 하나만 사용
    public float bgmFadeDuration = 2f;
    public float autoTransitionDelay = 38f; // 38초 뒤 전환

    void Start()
    {
        // 처음엔 투명한 흰색
        if (fadeMaterial != null)
            fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, 0));

        // 38초 후 자동 전환 시작
        Invoke("StartWhiteFade", autoTransitionDelay);
    }

    public void StartWhiteFade()
    {
        Debug.Log("🔥 자동 씬 전환 시작됨!");
        StartCoroutine(FadeAndSwitchScene());
    }

    IEnumerator FadeAndSwitchScene()
    {
        float t = 0f;

        // BGM 페이드아웃
        if (bgmSourceMain != null && bgmSourceMain.isPlaying)
            StartCoroutine(FadeOutBGM(bgmSourceMain));

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, t / fadeDuration);

            if (fadeMaterial != null)
                fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, alpha));

            yield return null;
        }

        Debug.Log("🚪 씬 전환 → " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator FadeOutBGM(AudioSource source)
    {
        float t = 0f;
        float startVolume = source.volume;

        while (t < bgmFadeDuration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, t / bgmFadeDuration);
            yield return null;
        }

        source.Stop();
    }
}