using UnityEngine;

public class NeonPulse : MonoBehaviour
{
    public Material neonMaterial;               // 머티리얼
    public Color glowColor = Color.yellow;      // 발광 색상
    public float minIntensity = 0.05f;           // 가장 어두운 상태
    public float maxIntensity = 1.5f;           // 가장 밝은 상태
    public float pulseSpeed = 2f;             // 숨쉬는 속도 (낮을수록 느림)

    private float time;

    void Update()
    {
        time += Time.deltaTime * pulseSpeed;

        // 숨쉬는 듯한 Intensity 값 만들기 (sin 곡선 활용)
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(time) + 1f) / 2f);

        // EmissionColor 갱신 (색 * 강도)
        neonMaterial.SetColor("_EmissionColor", glowColor * intensity);
    }
}