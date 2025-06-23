using System.Collections;
using UnityEngine;
using TMPro;

public class TimedAudioAndText : MonoBehaviour
{
    public AudioSource audioSource;           // 🎧 오디오 소스
    public GameObject textUI;                // 📝 텍스트 오브젝트
    public TextMeshProUGUI textComponent;    // 텍스트 컴포넌트 (선택적)
    public string message = "파업 시위 성공! 밀린 급여를 받았습니다";

    void Start()
    {
        // 시작 시 텍스트 비활성화
        if (textUI != null)
            textUI.SetActive(false);

        StartCoroutine(TriggerAudioAndText());
    }

    IEnumerator TriggerAudioAndText()
    {
        yield return new WaitForSeconds(32f);

        // 오디오 재생
        if (audioSource != null)
            audioSource.Play();

        // 텍스트 보이기
        if (textUI != null)
        {
            textUI.SetActive(true);

            // 텍스트 내용 세팅
            if (textComponent != null)
                textComponent.text = message;
        }
    }
}
