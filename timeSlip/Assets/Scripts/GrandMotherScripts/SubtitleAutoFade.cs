using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitleAutoFade : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;    // 자막 텍스트
    public string fullText = "과거 우리 할머니 같은 분들의 희생과 노고 덕분에 지금의 내가 조금 더 나은 세상에서 살 수 있는 거 였어! 어? 옆에 액자는 언제지?";
    public float delay = 0.1f;              // 타이핑 속도
    public AudioSource voice1;              // 첫 번째 음성 오디오
    public AudioSource voice2;              // 두 번째 음성 오디오
    public AudioSource typingSound;         // 타이핑 효과음
    public GameObject background1;          // 자막 배경 1
    public GameObject background2;          // 자막 배경 2
    public float fadeOutDuration = 1.5f;    // 자막 사라지는 시간

    void Start()
    {
        subtitleText.text = "";
        subtitleText.gameObject.SetActive(false);
        background1.SetActive(false);
        background2.SetActive(false);

        StartCoroutine(ShowSubtitleRoutine());
    }

    IEnumerator ShowSubtitleRoutine()
    {
        yield return new WaitForSeconds(4f);

        // 자막 + 배경 보이게
        subtitleText.gameObject.SetActive(true);
        background1.SetActive(true);
        background2.SetActive(true);

        // 음성 재생 시작
        if (voice1 != null) voice1.Play();
        if (typingSound != null) typingSound.Play();

        // 타이핑 효과
        for (int i = 0; i < fullText.Length; i++)
        {
            subtitleText.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }

        if (typingSound != null) typingSound.Stop();

        // voice1 끝날 때까지 기다리기
        if (voice1 != null)
            while (voice1.isPlaying) yield return null;

        // voice2 재생
        if (voice2 != null) voice2.Play();

        // voice2 끝날 때까지 대기
        if (voice2 != null)
            while (voice2.isPlaying) yield return null;

        // 페이드아웃 시작
        StartCoroutine(FadeOutSubtitle());
    }

    IEnumerator FadeOutSubtitle()
    {
        float t = 0f;
        Color originalColor = subtitleText.color;

        CanvasGroup group1 = background1.GetComponent<CanvasGroup>();
        CanvasGroup group2 = background2.GetComponent<CanvasGroup>();

        // 배경에 CanvasGroup 없으면 추가
        if (group1 == null) group1 = background1.AddComponent<CanvasGroup>();
        if (group2 == null) group2 = background2.AddComponent<CanvasGroup>();

        float startAlpha = 1f;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, t / fadeOutDuration);

            subtitleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            group1.alpha = alpha;
            group2.alpha = alpha;

            yield return null;
        }

        subtitleText.gameObject.SetActive(false);
        background1.SetActive(false);
        background2.SetActive(false);
    }
}
