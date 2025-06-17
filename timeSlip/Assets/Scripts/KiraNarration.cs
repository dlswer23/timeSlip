using System.Collections;
using UnityEngine;

public class KiraNarration : MonoBehaviour
{
    public DoorController doorController;
    public AudioSource kiraAudioSource;
    public AudioClip kiraClip;
    public float delayBeforeSpeaking = 5f; // ⏱ 5초 후에 말하게 설정

    public Animator kiraAnimator; // 🎞 애니메이터 추가
    public string animationTriggerName = "Talk"; // 실행할 트리거 이름

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
            // 🎞 애니메이션 트리거 실행
            if (kiraAnimator != null && !string.IsNullOrEmpty(animationTriggerName))
            {
                kiraAnimator.SetTrigger(animationTriggerName);
            }

            kiraAudioSource.clip = kiraClip;
            kiraAudioSource.Play();
            Debug.Log("🎤 5초 후 Kira 오디오 재생 + 애니메이션 실행됨!");
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
