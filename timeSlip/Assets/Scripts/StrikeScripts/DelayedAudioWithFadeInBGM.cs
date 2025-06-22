using UnityEngine;

public class DelayedAudioWithFadeInBGM : MonoBehaviour
{
    public AudioSource whistleAudio;     // 휘슬 사운드
    public AudioSource bgmAudio;         // BGM (페이드인)
    public AudioSource voiceAudio;       // 🎙️ 음성 오디오 추가!
    public float fadeDuration = 2f;      // 페이드인 시간

    void Start()
    {
        Invoke(nameof(PlayWhistle), 12f); // 12초 뒤 휘슬
    }

    void PlayWhistle()
    {
        if (whistleAudio != null)
        {
            whistleAudio.Play();
            float whistleLength = whistleAudio.clip.length;

            // 휘슬 끝 + 0.5초 뒤에 BGM + 음성 재생
            Invoke(nameof(StartFadeInBGMAndVoice), whistleLength + 0.5f);
        }
        else
        {
            Debug.LogWarning("Whistle AudioSource가 설정되지 않았습니다.");
        }
    }

    void StartFadeInBGMAndVoice()
    {
        // 🎧 BGM 시작
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

        // 🎙️ 음성 오디오 시작
        if (voiceAudio != null)
        {
            voiceAudio.Play();
        }
        else
        {
            Debug.LogWarning("Voice AudioSource가 설정되지 않았습니다.");
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
        bgmAudio.volume = 1f; // 최종 볼륨 고정
    }
}
