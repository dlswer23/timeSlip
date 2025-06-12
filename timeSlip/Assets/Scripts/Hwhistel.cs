using UnityEngine;

public class DelayedAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스를 인스펙터에서 할당

    void Start()
    {
        // 12초 뒤에 PlayAudio 함수 실행
        Invoke("PlayAudio", 12f);
    }

    void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource가 할당되지 않았습니다.");
        }
    }
}
