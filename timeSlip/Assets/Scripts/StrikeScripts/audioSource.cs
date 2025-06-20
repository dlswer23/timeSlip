using UnityEngine;

public class DelayedAudioFadeOut : MonoBehaviour
{
    public AudioSource audioSource;      // 페이드아웃할 AudioSource
    public float fadeDuration = 3f;      // 페이드아웃 지속 시간 (초)

    void Start()
    {
        // 12초 후에 페이드아웃 시작
        Invoke("StartFadeOut", 12f);
    }

    void StartFadeOut()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            StartCoroutine(FadeOutCoroutine());
        }
        else
        {
            Debug.LogWarning("오디오가 재생 중이 아니거나 AudioSource가 없습니다.");
        }
    }

    System.Collections.IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // 나중을 위해 볼륨 복원 가능
    }
}
