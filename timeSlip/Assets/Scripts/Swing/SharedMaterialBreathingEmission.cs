using UnityEngine;
using System.Collections;

public class SharedMaterialBreathingEmission : MonoBehaviour
{
    public Material sharedMaterial;
    public Color emissionColor = Color.white;

    public float fadeInDuration = 3f;
    public float pulseMin = 0f;
    public float pulseMax = 1f;
    public float pulseSpeed = 2f;

    public bool isLet = false;  // 👈 외부에서 제어

    private bool emissionEnabled = false;

    void Start()
    {
        sharedMaterial.DisableKeyword("_EMISSION");
        sharedMaterial.SetColor("_EmissionColor", Color.black);
    }

    void Update()
    {
        if (isLet && !emissionEnabled)
        {
            emissionEnabled = true;
            StartCoroutine(FadeInAndEnableBreathing());
        }

        if (emissionEnabled)
        {
            float pulse = Mathf.Lerp(pulseMin, pulseMax, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            sharedMaterial.SetColor("_EmissionColor", emissionColor * pulse);
        }
    }

    IEnumerator FadeInAndEnableBreathing()
    {
        sharedMaterial.EnableKeyword("_EMISSION");

        float t = 0f;
        while (t < fadeInDuration)
        {
            float intensity = Mathf.Lerp(0f, pulseMin, t / fadeInDuration);
            sharedMaterial.SetColor("_EmissionColor", emissionColor * intensity);
            yield return null;
            t += Time.deltaTime;
        }

        sharedMaterial.SetColor("_EmissionColor", emissionColor * pulseMin);
    }
}
