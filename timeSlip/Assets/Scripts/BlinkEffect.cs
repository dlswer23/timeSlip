using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public Image blinkImage;              // ȭ�� ���� ���� �̹���
    public float blinkDuration = 0.2f;    // ���Ҵ� �ߴ� �ӵ�
    public int blinkCount = 3;            // �� ���� Ƚ��

    void Start()
    {
        // �� ���� ����(ȭ�� ����)�� ����
        if (blinkImage != null)
            blinkImage.color = new Color(0, 0, 0, 1f);  // ���� 1 = ������

        // �������ڸ��� �ڵ����� 3�� ������ ����
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
            // �� �߱� (1 �� 0)
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

            // �� ���� (0 �� 1)
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

        // ������ �� �߱� (1 �� 0)
        float finalT = 0f;
        while (finalT < blinkDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, finalT / blinkDuration);
            blinkImage.color = new Color(0, 0, 0, alpha);
            finalT += Time.deltaTime;
            yield return null;
        }
        blinkImage.color = new Color(0, 0, 0, 0f); // �� ������ �� ���� ����
    }
}
