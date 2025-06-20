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

        originalRotation = transform.rotation;
        float newY = transform.eulerAngles.y + rotationOffsetY;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, newY, transform.eulerAngles.z);

        yield return StartCoroutine(SmoothRotate(transform.rotation, targetRotation, rotationDuration));

        if (kiraAnimator != null && !string.IsNullOrEmpty(animationStateName))
            kiraAnimator.Play(animationStateName);

        if (kiraAudioSource != null && kiraClip != null)
        {
            kiraAudioSource.clip = kiraClip;
            kiraAudioSource.Play();

            // 👉 이 시점에서 isLet = true
            isLet = true;

            // ✅ TypingEffect3 트리거
            TypingEffect3 typing = FindObjectOfType<TypingEffect3>();
            if (typing != null)
                typing.StartTypingExternally();
        }

        yield return new WaitForSeconds(kiraClip.length);

        yield return StartCoroutine(SmoothRotate(transform.rotation, originalRotation, rotationDuration));

        if (kiraAnimator != null && !string.IsNullOrEmpty(idleStateName))
            kiraAnimator.Play(idleStateName);

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
