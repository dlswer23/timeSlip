using System.Collections;
using UnityEngine;

public class BGMAudioFader : MonoBehaviour
{
    public AudioSource bgmSource;        // 배경음 AudioSource
    public float fadeDuration = 2f;      // 페이드아웃 지속 시간

    public void StartFadeOut()
    {
        if (bgmSource != null && bgmSource.isPlaying)
            StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = bgmSource.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        bgmSource.Stop();
    }
}