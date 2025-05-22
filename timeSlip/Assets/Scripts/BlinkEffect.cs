using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public Image blinkImage;              // 화면 덮는 검은 이미지
    public float blinkDuration = 0.2f;    // 감았다 뜨는 속도
    public int blinkCount = 3;            // 눈 깜빡 횟수

    void Start()
    {
        // 눈 감은 상태(화면 검정)로 시작
        if (blinkImage != null)
            blinkImage.color = new Color(0, 0, 0, 1f);  // 알파 1 = 불투명

        // 시작하자마자 자동으로 3번 깜빡임 실행
        BlinkMultiple();
    }

    public void BlinkMultiple()
    {
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // 눈 뜨기 (1 → 0)
            float t = 0f;
            while (t < blinkDuration)
            {
                float alpha = Mathf.Lerp(1f, 0f, t / blinkDuration);
                blinkImage.color = new Color(0, 0, 0, alpha);
                t += Time.deltaTime;
                yield return null;
            }
            blinkImage.color = new Color(0, 0, 0, 0f);
            yield return new WaitForSeconds(0.1f);

            // 눈 감기 (0 → 1)
            t = 0f;
            while (t < blinkDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, t / blinkDuration);
                blinkImage.color = new Color(0, 0, 0, alpha);
                t += Time.deltaTime;
                yield return null;
            }
            blinkImage.color = new Color(0, 0, 0, 1f);
            yield return new WaitForSeconds(0.1f);
        }

        // 마지막 눈 뜨기 (1 → 0)
        float finalT = 0f;
        while (finalT < blinkDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, finalT / blinkDuration);
            blinkImage.color = new Color(0, 0, 0, alpha);
            finalT += Time.deltaTime;
            yield return null;
        }
        blinkImage.color = new Color(0, 0, 0, 0f); // 눈 완전히 뜬 상태 유지
    }
}
