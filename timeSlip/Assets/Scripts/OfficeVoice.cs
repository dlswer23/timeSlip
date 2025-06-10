using UnityEngine;

public class DelayedVoiceLine : MonoBehaviour
{
    public AudioSource voiceSource;   // 음성 재생용
    public float delaySeconds = 4f;   // 몇 초 후 재생

    void Start()
    {
        Invoke("PlayVoice", delaySeconds);
    }

    void PlayVoice()
    {
        voiceSource.Play();
        Debug.Log("🗣️ 대사 음성 재생 시작!");
    }
}