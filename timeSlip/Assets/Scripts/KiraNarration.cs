using System.Collections;
using UnityEngine;

public class KiraNarration : MonoBehaviour
{
    public DoorController doorController;
    public AudioSource kiraAudioSource;
    public AudioClip kiraClip;
    public float delayBeforeSpeaking = 5f;

    public Animator kiraAnimator;

    [Header("KiraNarration")]
    public string animationStateName = "Pointing";       // 애니메이션 클립 이름
    public string idleStateName = "HumanoidIdle";         // 복귀용 애니메이션 클립 이름

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

        // 👉 1. 애니메이션 강제 실행 (파라미터 없이)
        if (kiraAnimator != null && !string.IsNullOrEmpty(animationStateName))
        {
            kiraAnimator.Play(animationStateName);
            Debug.Log($"🕹 애니메이션 '{animationStateName}' 실행");
        }

        // 👉 2. 오디오 재생
        if (kiraAudioSource != null && kiraClip != null)
        {
            kiraAudioSource.clip = kiraClip;
            kiraAudioSource.Play();
            Debug.Log("🎤 Kira 오디오 재생됨");
        }

        // 👉 3. 대사 끝나고 원래 상태로 돌아가기
        yield return new WaitForSeconds(kiraClip.length);

        if (kiraAnimator != null && !string.IsNullOrEmpty(idleStateName))
        {
            kiraAnimator.Play(idleStateName);
            Debug.Log($"↩ 애니메이션 '{idleStateName}'으로 복귀");
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
