using System.Collections;
using UnityEngine;

public class FinalSceneExit : MonoBehaviour
{
    public AudioSource bgmSource;           // 배경음
    public Material fadeMaterial;           // 흰색 머티리얼
    public float fadeDuration = 2.5f;       // 화면 페이드 시간
    public float bgmFadeDuration = 2.5f;    // 오디오 페이드 시간

    void Start()
    {
        // 시작할 때 머티리얼 알파 0으로 초기화
        if (fadeMaterial != null)
            fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, 0));

        // 23초 후 연출 시작
        Invoke("StartFinalFade", 23f);
    }

    void StartFinalFade()
    {
        Debug.Log("🎬 최종 페이드 시작!");
        if (bgmSource != null) StartCoroutine(FadeOutBGM(bgmSource));
        StartCoroutine(FadeToWhiteAndQuit());
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

    IEnumerator FadeToWhiteAndQuit()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, t / fadeDuration);
            if (fadeMaterial != null)
                fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, alpha));
            yield return null;
        }

        Debug.Log("💤 종료!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
