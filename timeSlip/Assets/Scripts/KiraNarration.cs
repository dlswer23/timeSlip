using System.Collections;
using UnityEngine;

public class KiraNarration : MonoBehaviour
{
    public DoorController doorController;
    public AudioSource kiraAudioSource;
    public AudioClip kiraClip;
    public float delayBeforeSpeaking = 5f;

    [Header("External Control")]
    public bool isLet = false;

    public Animator kiraAnimator;

    [Header("KiraNarration")]
    public string animationStateName = "Pointing";
    public string idleStateName = "HumanoidIdle";

    [Header("Floating Effect Target")]
    public FloatingEffect floatingTarget;

    [Header("Optional Emission Effects")]
    public SharedMaterialBreathingEmission[] breathingEmissionTargets;

    [Header("Rotation Settings")]
    public float rotationOffsetY = -30f; // 말할 때 회전 각도
    public float rotationDuration = 0.8f;

    private Quaternion originalRotation;

    void Start()
    {
        if (doorController != null)
            doorController.OnDoorFullyClosed += OnDoorClosed;
    }

    void OnDestroy()
    {
        if (doorController != null)
            doorController.OnDoorFullyClosed -= OnDoorClosed;
    }

    void OnDoorClosed()
    {
        StartCoroutine(PlayKiraAfterDelay());
    }

    private IEnumerator PlayKiraAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSpeaking);

        // 👉 원래 회전 저장
        originalRotation = transform.rotation;

        // 👉 타겟 회전 계산
        float newY = transform.eulerAngles.y + rotationOffsetY;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, newY, transform.eulerAngles.z);

        // 👉 부드러운 회전
        yield return StartCoroutine(SmoothRotate(transform.rotation, targetRotation, rotationDuration));

        // 🎞 애니메이션 실행
        if (kiraAnimator != null && !string.IsNullOrEmpty(animationStateName))
            kiraAnimator.Play(animationStateName);

        // 🎤 오디오 재생
        if (kiraAudioSource != null && kiraClip != null)
        {
            kiraAudioSource.clip = kiraClip;
            kiraAudioSource.Play();
        }

        yield return new WaitForSeconds(kiraClip.length);

        // ↩ 원래 방향으로 회전 복귀
        yield return StartCoroutine(SmoothRotate(transform.rotation, originalRotation, rotationDuration));

        // 😌 애니메이션 복귀
        if (kiraAnimator != null && !string.IsNullOrEmpty(idleStateName))
            kiraAnimator.Play(idleStateName);

        // ✨ 외부 연계 효과 실행 (isLet 체크)
        if (isLet)
        {
            if (floatingTarget != null)
                floatingTarget.enabled = true;

            if (breathingEmissionTargets != null)
            {
                foreach (var effect in breathingEmissionTargets)
                {
                    if (effect != null)
                        effect.isLet = true;
                }
            }
        }
    }

    private IEnumerator SmoothRotate(Quaternion from, Quaternion to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }
}