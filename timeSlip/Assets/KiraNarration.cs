using System.Collections;
using UnityEngine;

public class KiraNarration : MonoBehaviour
{
    public DoorController doorController;
    public AudioSource kiraAudioSource;
    public AudioClip kiraClip;
    public float delayBeforeSpeaking = 5f; // ⏱ 5초 후에 말하게 설정

    void Start()
    {
        doorController.OnDoorFullyClosed += OnDoorClosed;
    }

    void OnDoorClosed()
    {
        StartCoroutine(PlayKiraAfterDelay());
    }

    private IEnumerator PlayKiraAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSpeaking);

        if (kiraAudioSource != null && kiraClip != null)
        {
            kiraAudioSource.clip = kiraClip;
            kiraAudioSource.Play();
            Debug.Log("🎤 5초 후 Kira 오디오 재생됨!");
        }
    }

    void OnDestroy()
    {
        if (doorController != null)
        {
            doorController.OnDoorFullyClosed -= OnDoorClosed;
        }
    }
}
