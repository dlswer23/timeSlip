using UnityEngine;

public class NeonPulse : MonoBehaviour
{
    public Material neonMaterial;               // Inspector 연결
    public Color glowColor = Color.yellow;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;
    public float pulseSpeed = 2f;

    private float time;
    private Material instanceMaterial;

    void Start()
    {
        // 1. 개별 인스턴스 복제
        instanceMaterial = new Material(neonMaterial);

        // 2. Emission 키워드 활성화
        instanceMaterial.EnableKeyword("_EMISSION");

        // 3. 머티리얼 적용
        GetComponent<MeshRenderer>().material = instanceMaterial;

        // 4. neonMaterial도 인스턴스로 치환 (안정성↑)
        neonMaterial = instanceMaterial;
    }

    void Update()
    {
        time += Time.deltaTime * pulseSpeed;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(time) + 1f) / 2f);
        neonMaterial.SetColor("_EmissionColor", glowColor * intensity);
    }
}
