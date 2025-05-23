using UnityEngine;

public class NeonPulse : MonoBehaviour
{
    public Material neonMaterial;               // ��Ƽ����
    public Color glowColor = Color.yellow;      // �߱� ����
    public float minIntensity = 0.05f;           // ���� ��ο� ����
    public float maxIntensity = 1.5f;           // ���� ���� ����
    public float pulseSpeed = 2f;             // ������ �ӵ� (�������� ����)

    private float time;

    void Update()
    {
        time += Time.deltaTime * pulseSpeed;

        // ������ ���� Intensity �� ����� (sin � Ȱ��)
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(time) + 1f) / 2f);

        // EmissionColor ���� (�� * ����)
        neonMaterial.SetColor("_EmissionColor", glowColor * intensity);
    }
}