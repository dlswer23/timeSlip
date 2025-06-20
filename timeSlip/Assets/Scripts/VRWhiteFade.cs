using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRWhiteFade : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeDuration = 2.5f;
    public string sceneToLoad = "SewingMachineWorkScene";

    public AudioSource bgmSourceMain;      // 처음부터 재생
    public AudioSource bgmSourceDelayed;   // 7초 후 등장 (페이드인)
    public float bgmFadeDuration = 2f;

    void Start()
    {
        // 시작 시 투명한 흰색 설정
        if (fadeMaterial != null)
            fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, 0));

        // BGM2 → 7초 후 페이드인 호출
        if (bgmSourceDelayed != null)
        {
            bgmSourceDelayed.volume = 0f; // 시작은 무음
            Invoke("FadeInDelayedBGM", 7f);
        }
    }

    void FadeInDelayedBGM()
    {
        Debug.Log("🎵 BGM2 페이드인 시작");
        bgmSourceDelayed.Play();
        StartCoroutine(FadeInBGM(bgmSourceDelayed, 1f, 2f)); // 목표 볼륨 1, 2초 동안 페이드인
    }

    private IEnumerator FadeInBGM(AudioSource source, float targetVolume, float duration)
    {
        float t = 0f;
        float startVolume = source.volume;

        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }

    public void StartWhiteFade()
    {
        Debug.Log("🔥 StartWhiteFade() 호출됨!");
        StartCoroutine(FadeAndSwitchScene());
    }

    private IEnumerator FadeAndSwitchScene()
    {
        float t = 0f;

        // 🎧 둘 다 페이드아웃
        if (bgmSourceMain != null && bgmSourceMain.isPlaying)
            StartCoroutine(FadeOutBGM(bgmSourceMain));

        if (bgmSourceDelayed != null && bgmSourceDelayed.isPlaying)
            StartCoroutine(FadeOutBGM(bgmSourceDelayed));

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

    private IEnumerator FadeOutBGM(AudioSource source)
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
