using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect3 : MonoBehaviour
{
    [Header("UI 요소")]
    public GameObject popupPanel;                // UI Panel to show/hide
    public TextMeshProUGUI typingText;           // Text to type out

    [Header("타이핑 설정")]
    [TextArea]
    public string fullText = "빛나는 미싱 소품들을\n박스 안에 옮겨 넣으세요.";
    public float delay = 0.1f;                   // typing speed

    [Header("사운드")]
    public AudioSource typingSound;              // typing sound effect

    private Coroutine currentRoutine;

    /// <summary>
    /// 외부에서 타이핑 효과를 실행하는 메서드 (한 번만 실행됨)
    /// </summary>
    public void StartTypingExternally()
    {
        if (currentRoutine == null)
        {
            currentRoutine = StartCoroutine(HandlePopup());
        }
    }

    private IEnumerator HandlePopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(true);  // 팝업 표시

        if (typingSound != null)
            typingSound.Play();          // 사운드 재생

        yield return StartCoroutine(PlayTyping());

        if (typingSound != null)
            typingSound.Stop();          // 사운드 종료

        yield return new WaitForSeconds(5f);     // ✅ 자동으로 5초 뒤 숨기기

        if (popupPanel != null)
            popupPanel.SetActive(false); // 팝업 숨기기

        currentRoutine = null;
    }

    private IEnumerator PlayTyping()
    {
        typingText.text = "";

        foreach (char c in fullText)
        {
            typingText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}
