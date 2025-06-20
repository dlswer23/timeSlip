using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI typingText;         // 타이핑할 TMP 텍스트
    public string fullText = "파업 시위에 참여하시겠습니까?";
    public float delay = 0.1f;
    public AudioSource typingSound;
    public GameObject continueButton;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(8f); // ⏱️ 8초 대기

        if (typingSound != null)
            typingSound.Play(); // 🔊 타이핑 사운드 시작

        StartCoroutine(PlayTyping());
    }

    IEnumerator PlayTyping()
    {
        typingText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            typingText.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }

        if (typingSound != null)
            typingSound.Stop(); // 🔇 타이핑 사운드 중지

        if (continueButton != null)
            continueButton.SetActive(true); // 버튼 표시
    }
}
