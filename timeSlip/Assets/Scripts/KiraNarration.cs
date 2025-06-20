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
    public float rotationOffsetY = -30f;
    public float rotationDuration = 0.8f;

    [Header("Optional Particle Effect")]
    public ParticleSystem particleEffect;

    // ✅ 전역에서 마지막으로 실행된 인스턴스를 기록
    private static KiraNarration lastTriggered = null;

    // ✅ 전역 팝업 실행 여부 (모든 인스턴스 공통)
    private static bool hasPopupShownGlobal = false;

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
        // ✅ 전역 기준 마지막 실행 객체 기록
        lastTriggered = this;

        yield return new WaitForSeconds(delayBeforeSpeaking);

        // 👉 원래 회전 저장
        originalRotation = transform.rotation;
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

            // 👉 대사 시작 시 isLet = true
            isLet = true;

            // ✅ 단 하나의 인스턴스만 팝업 실행
            if (!hasPopupShownGlobal && lastTriggered == this)
            {
                TypingEffect3 typing = FindObjectOfType<TypingEffect3>();
                if (typing != null)
                    typing.StartTypingExternally();

                hasPopupShownGlobal = true;
            }
        }

        yield return new WaitForSeconds(kiraClip.length);

        // ↩ 원래 방향으로 회전 복귀
        yield return StartCoroutine(SmoothRotate(transform.rotation, originalRotation, rotationDuration));

        // 😌 애니메이션 복귀
        if (kiraAnimator != null && !string.IsNullOrEmpty(idleStateName))
            kiraAnimator.Play(idleStateName);

        // ✨ 외부 연계 효과 실행
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

            if (particleEffect != null)
                particleEffect.Play();
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
