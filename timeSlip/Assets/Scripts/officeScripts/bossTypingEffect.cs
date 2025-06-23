using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitleController : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;     // 자막 텍스트
    public string fullText = "이번 달 적자나서 돈 못 줘\n돈 받고 싶으면 나가서 더 일이나 해 나가!";
    public float delay = 0.25f;              // 타이핑 속도
    public AudioSource voiceAudio;           // 음성 오디오
    public GameObject background1;           // 자막 배경 1
    public GameObject background2;           // 자막 배경 2

    void Start()
    {
        subtitleText.text = "";

        // 🎬 시작 시 전부 숨김
        subtitleText.gameObject.SetActive(false);
        if (background1 != null) background1.SetActive(false);
        if (background2 != null) background2.SetActive(false);

        StartCoroutine(ShowSubtitleWithVoice());
    }

    IEnumerator ShowSubtitleWithVoice()
    {
        yield return new WaitForSeconds(4f); // ⏱️ 4초 대기

        // 🎥 자막, 배경 모두 나타나게
        subtitleText.gameObject.SetActive(true);
        if (background1 != null) background1.SetActive(true);
        if (background2 != null) background2.SetActive(true);

        if (voiceAudio != null)
            voiceAudio.Play(); // 🎤 음성 재생

        // 타이핑 효과
        for (int i = 0; i < fullText.Length; i++)
        {
            subtitleText.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }

        // 🎧 음성 끝날 때까지 기다리긴 하지만 자막은 그대로 유지
        if (voiceAudio != null)
        {
            while (voiceAudio.isPlaying)
                yield return null;
        }

        // ❌ 자막과 배경은 더 이상 사라지지 않음 (이 부분 삭제됨)
    }
}
