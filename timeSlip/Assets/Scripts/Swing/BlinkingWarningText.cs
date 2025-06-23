using System.Collections;
using UnityEngine;
using TMPro;

public class BlinkingWarningUI : MonoBehaviour
{
    public TextMeshProUGUI warningText;  // 깜빡일 텍스트
    public GameObject backgroundObject;  // 배경 UI (예: Panel)

    public float blinkInterval = 1.0f;   // 깜빡임 간격 (초)
    private float showTime = 12f;        // 시작 시점
    private float hideTime = 20f;        // 종료 시점

    void Start()
    {
        // 처음엔 안 보이게
        if (warningText != null)
            warningText.gameObject.SetActive(false);
        if (backgroundObject != null)
            backgroundObject.SetActive(false);

        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        yield return new WaitForSeconds(showTime); // ⏱️ 대기

        if (warningText != null)
            warningText.gameObject.SetActive(true);
        if (backgroundObject != null)
            backgroundObject.SetActive(true);

        float timer = 0f;
        bool isVisible = true;

        while (timer < (hideTime - showTime))
        {
            isVisible = !isVisible;

            if (warningText != null)
                warningText.enabled = isVisible;
            if (backgroundObject != null)
                backgroundObject.SetActive(isVisible);

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // 종료 시 확실히 꺼줌
        if (warningText != null)
            warningText.gameObject.SetActive(false);
        if (backgroundObject != null)
            backgroundObject.SetActive(false);
    }
}
