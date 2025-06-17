using UnityEngine;

public class SkipAndPlayAudio : MonoBehaviour
{
    public AudioSource audioSource; // 재생할 오디오 소스
    public float skipTime = 0.3f;   // 건너뛸 시간 (초)

    void Start()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            // 0.3초 지점부터 재생
            audioSource.time = skipTime;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource 또는 오디오 클립이 비어 있습니다.");
        }
    }
}
