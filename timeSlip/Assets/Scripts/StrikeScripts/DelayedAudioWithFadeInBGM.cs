using UnityEngine;

public class DelayedAudioWithFadeInBGM : MonoBehaviour
{
    public AudioSource whistleAudio;     // 휘슬 소리
    public AudioSource bgmAudio;         // 배경음 (페이드인)
    public float fadeDuration = 2f;      // 페이드인에 걸리는 시간 (초)

    void Start()
    {
        Invoke(nameof(PlayWhistle), 12f);  // 12초 뒤 휘슬 실행
    }

    void PlayWhistle()
    {
        if (whistleAudio != null)
        {
            whistleAudio.Play();
            float whistleLength = whistleAudio.clip.length;

            // 휘슬 끝난 뒤 0.5초 대기 후 BGM 재생 시작
            Invoke(nameof(StartFadeInBGM), whistleLength + 0.5f);
        }
        else
        {
            Debug.LogWarning("Whistle AudioSource가 설정되지 않았습니다.");
        }
    }

    void StartFadeInBGM()
    {
        if (bgmAudio != null)
        {
            bgmAudio.volume = 0f;
            bgmAudio.Play();
            StartCoroutine(FadeInBGM());
        }
        else
        {
            Debug.LogWarning("BGM AudioSource가 설정되지 않았습니다.");
        }
    }

    System.Collections.IEnumerator FadeInBGM()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            bgmAudio.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }
        bgmAudio.volume = 1f; // 최종적으로 1로 고정
    }
}
