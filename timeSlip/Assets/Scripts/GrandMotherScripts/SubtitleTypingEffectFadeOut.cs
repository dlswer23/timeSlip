using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitleWithTypingAndFade : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;          // 자막 텍스트
    public string fullText = "과거 우리 할머니 같은 분들의 희생과 노고 덕분에 지금의 내가 조금 더 나은 세상에서 살 수 있는 거 였어! 어? 옆에 액자는 언제지?";
    public float delay = 0.15f;                   // 타이핑 속도

    public AudioSource typingSound;               // 타이핑 효과음
    public AudioSource voiceAudio;                // 캐릭터 음성

    public GameObject background1;                // 자막 배경 1
    public GameObject background2;                // 자막 배경 2
    public float fadeOutDuration = 2f;            // 자막 사라지는 시간

    void Start()
    {
        subtitleText.text = "";
        subtitleText.gameObject.SetActive(false);
        if (background1 != null) background1.SetActive(false);
        if (background2 != null) background2.SetActive(false);

        StartCoroutine(ShowSubtitle());
    }

    IEnumerator ShowSubtitle()
    {
        yield return new WaitForSeconds(4f);  // 4초 뒤에 자막 등장

        subtitleText.gameObject.SetActive(true);
        if (background1 != null) background1.SetActive(true);
        if (background2 != null) background2.SetActive(true);

        if (typingSound != null) typingSound.Play();  // 🔊 타이핑 소리
        if (voiceAudio != null) voiceAudio.Play();    // 🎤 음성 재생

        subtitleText.text = "";
        for (int i = 0; i < fullText.Length; i++)
        {
            subtitleText.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }

        if (typingSound != null) typingSound.Stop(); // 🔇 타이핑 소리 중지

        // 음성이 끝날 때까지 대기
        if (voiceAudio != null)
        {
            while (voiceAudio.isPlaying)
                yield return null;
        }

        // 자막 fade out
        StartCoroutine(FadeOutSubtitles());
    }

    IEnumerator FadeOutSubtitles()
    {
        float elapsed = 0f;
        Color originalColor = subtitleText.color;

        CanvasGroup bg1 = background1?.GetComponent<CanvasGroup>();
        CanvasGroup bg2 = background2?.GetComponent<CanvasGroup>();

        // CanvasGroup 없으면 추가
        if (bg1 == null && background1 != null) bg1 = background1.AddComponent<CanvasGroup>();
        if (bg2 == null && background2 != null) bg2 = background2.AddComponent<CanvasGroup>();

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeOutDuration);

            subtitleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            if (bg1 != null) bg1.alpha = alpha;
            if (bg2 != null) bg2.alpha = alpha;

            yield return null;
        }

        subtitleText.gameObject.SetActive(false);
        if (background1 != null) background1.SetActive(false);
        if (background2 != null) background2.SetActive(false);
    }
}
