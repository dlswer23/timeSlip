using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public Image blinkImage;              // È­¸é µ¤´Â °ËÀº ÀÌ¹ÌÁö
    public float blinkDuration = 1f;    // °¨°í ¶ß´Â ¼Óµµ (0.5ÃÊ)
    public int blinkCount = 2;            // ±ôºıÀÓ È½¼ö

    void Start()
    {
        if (blinkImage != null)
            blinkImage.color = new Color(0, 0, 0, 1f);  // ½ÃÀÛÀº ´« °¨Àº »óÅÂ

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
            // ´« ¶ß±â (1 ¡æ 0)
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

            // ´« °¨±â (0 ¡æ 1)
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

        // ¸¶Áö¸· ´« ¶ß±â
        float finalT = 0f;
        while (finalT < blinkDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, finalT / blinkDuration);
            blinkImage.color = new Color(0, 0, 0, alpha);
            finalT += Time.deltaTime;
            yield return null;
        }
        blinkImage.color = new Color(0, 0, 0, 0f);
    }
}