using UnityEngine;
using System.Collections;

public class DisableEmissionAfterDelay : MonoBehaviour
{
    public Renderer targetRenderer;             // MeshRenderer 연결
    public Color baseEmissionColor = Color.yellow; // 빛나는 색상
    public float intensity = 1f;                // 처음 밝기
    public float delaySeconds = 16f;             // 몇 초 뒤 꺼지기 시작
    public float fadeDuration = 2f;             // 천천히 꺼지는 시간

    private Material instanceMat;

    void Start()
    {
        // 머티리얼 인스턴스화 (공유X)
        instanceMat = targetRenderer.material;
        instanceMat.EnableKeyword("_EMISSION");
        instanceMat.SetColor("_EmissionColor", baseEmissionColor * intensity);

        // 지정된 시간 뒤에 서서히 꺼지기 시작
        Invoke(nameof(BeginFadeOut), delaySeconds);
    }

    void BeginFadeOut()
    {
        StartCoroutine(FadeEmission());
    }

    IEnumerator FadeEmission()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            float lerpIntensity = Mathf.Lerp(intensity, 0f, t / fadeDuration);
            instanceMat.SetColor("_EmissionColor", baseEmissionColor * lerpIntensity);
            t += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 완전히 꺼지도록 확정
        instanceMat.SetColor("_EmissionColor", Color.black);
    }
}
