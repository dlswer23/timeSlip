// FloatingEffect.cs
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float amplitude = 0.1f;  // 위아래 움직임 크기
    public float frequency = 1f;    // 움직임 속도

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        // ✅ 처음엔 비활성화
        enabled = false;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}
