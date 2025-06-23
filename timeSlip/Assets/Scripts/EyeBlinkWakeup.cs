using UnityEngine;
using System.Collections;

public class EyeBlinkWakeup : MonoBehaviour
{
    public Material eyeMat;               // EyeLidMat 연결
    public float blinkDuration = 0.6f;    // 감기/뜨기 속도
    public float holdClosedTime = 0.2f;   // 눈 감은 상태 유지 시간
    public float holdOpenTime = 0.3f;     // 눈 뜬 상태 유지 시간 (깜빡 사이)
    public int blinkCount = 3;            // 깜빡 횟수
    public float initialHoldTime = 6f;    // 🔥 처음 눈 감은 상태 유지 시간

    void Start()
    {
        // 시작 시 눈 감은 상태로 고정
        Color c = eyeMat.color;
        eyeMat.color = new Color(c.r, c.g, c.b, 1f);

        // 🔥 깜빡임 루틴 실행 전 6초 대기
        StartCoroutine(WaitThenStartBlink());
    }

    IEnumerator WaitThenStartBlink()
    {
        yield return new WaitForSeconds(initialHoldTime);
        StartCoroutine(BlinkWakeupRoutine());
    }

    IEnumerator BlinkWakeupRoutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            Debug.Log("Blink Started!");

            // 눈 뜨기
            yield return StartCoroutine(FadeToAlpha(0f));
            yield return new WaitForSeconds(holdOpenTime);

            // 눈 감기
            yield return StartCoroutine(FadeToAlpha(1f));
            yield return new WaitForSeconds(holdClosedTime);
        }

        // 마지막 눈 뜨기 (완전히 깨어남)
        yield return StartCoroutine(FadeToAlpha(0f));
    }

    IEnumerator FadeToAlpha(float targetAlpha)
    {
        float duration = blinkDuration;
        float t = 0f;
        Color currentColor = eyeMat.color;
        float startAlpha = currentColor.a;

        while (t < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            eyeMat.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            t += Time.deltaTime;
            yield return null;
        }

        eyeMat.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }
}
