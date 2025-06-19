using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect2 : MonoBehaviour
{
    [Header("UI 요소")]
    public GameObject popupPanel;
    public TextMeshProUGUI typingText;

    [Header("타이핑 설정")]
    [TextArea]
    public string fullText = "호루라기 소리가 들리면\n미싱공들의 구호에 맞춰\n시위에 참여하세요";
    public float delay = 0.1f;

    [Header("사운드")]
    public AudioSource typingSound;

    void Start()
    {
        StartCoroutine(HandlePopup());
    }

    IEnumerator HandlePopup()
    {
        yield return new WaitForSeconds(2f); // 🔸 2초 대기

        if (popupPanel != null)
            popupPanel.SetActive(true); // ✅ 팝업창 먼저 활성화

        if (typingSound != null)
            typingSound.Play(); // 🔊 사운드 먼저 재생

        yield return StartCoroutine(PlayTyping()); // ⏱ 타이핑 재생 (시간 오래 걸림)

        if (typingSound != null)
            typingSound.Stop(); // ✅ 타이핑 끝나면 사운드 종료

        yield return new WaitForSeconds(3f); // ⏳ 추가로 잠깐 보여주기

        if (popupPanel != null)
            popupPanel.SetActive(false); // 🔽 팝업 숨김
    }

    IEnumerator PlayTyping()
    {
        typingText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            typingText.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }
    }
}
