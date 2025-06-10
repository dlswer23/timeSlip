using System.Collections;
using UnityEngine;

public class VRWhiteFadeIn : MonoBehaviour
{
    public Material fadeMaterial;         // 흰색 투명 머티리얼 (URP/Unlit + Transparent)
    public float fadeInDuration = 2.5f;   // 점점 사라지는 시간

    void Start()
    {
        if (fadeMaterial != null)
        {
            // 시작할 때 흰색 알파 = 1 (완전 덮임)
            fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, 1));
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(1f, 0f, t / fadeInDuration);
            fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, alpha));
            yield return null;
        }

        // 마지막에 완전히 투명하게
        fadeMaterial.SetColor("_BaseColor", new Color(1, 1, 1, 0));
    }
}
