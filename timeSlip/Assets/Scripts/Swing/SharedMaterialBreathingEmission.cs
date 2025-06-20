using UnityEngine;
using System.Collections;

public class SharedMaterialBreathingEmission : MonoBehaviour
{
    public Material sharedMaterial;
    public Color emissionColor = Color.white;

    public float delay = 45f;
    public float fadeInDuration = 3f;
    public float pulseMin = 0f;
    public float pulseMax = 1f;
    public float pulseSpeed = 2f;

    private bool emissionEnabled = false;
    private float timer = 0f;

    void Start()
    {
        // 초기 Emission 완전히 끄기
        sharedMaterial.DisableKeyword("_EMISSION");
        sharedMaterial.SetColor("_EmissionColor", Color.black);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!emissionEnabled && timer >= delay)
        {
            emissionEnabled = true;
            StartCoroutine(FadeInAndEnableBreathing());
        }

        // 숨쉬기 효과
        if (emissionEnabled && timer >= delay + fadeInDuration)
        {
            float pulse = Mathf.Lerp(pulseMin, pulseMax, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            sharedMaterial.SetColor("_EmissionColor", emissionColor * pulse);
        }
    }

    IEnumerator FadeInAndEnableBreathing()
    {
        // Emission 활성화 키워드 켜기
        sharedMaterial.EnableKeyword("_EMISSION");

        float t = 0f;
        while (t < fadeInDuration)
        {
            float intensity = Mathf.Lerp(0f, pulseMin, t / fadeInDuration);
            sharedMaterial.SetColor("_EmissionColor", emissionColor * intensity);
            yield return null;
            t += Time.deltaTime;
        }

        // 최종 최소값 보정
        sharedMaterial.SetColor("_EmissionColor", emissionColor * pulseMin);
    }
}
