using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiraNarration : MonoBehaviour
{
    public DoorController doorController;
    public AudioSource kiraAudioSource;
    public AudioClip kiraClip;
    public float delayBeforeSpeaking = 5f;

    public Animator kiraAnimator;

    [Header("KiraNarration")]
    public string animationStateName = "Pointing";
    public string idleStateName = "HumanoidIdle";

    [Header("Floating Effect Target")]
    public FloatingEffect floatingTarget;

    [Header("Breathing Emission Targets")] // ✅ 여러 개 등록 가능
    public List<SharedMaterialBreathingEmission> emissionTargets;

    public bool isLet = false;

    void Start()
    {
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

        // 👉 애니메이션 실행
        if (kiraAnimator != null && !string.IsNullOrEmpty(animationStateName))
            kiraAnimator.Play(animationStateName);

        // 👉 오디오 재생
        if (kiraAudioSource != null && kiraClip != null)
        {
            kiraAudioSource.clip = kiraClip;
            kiraAudioSource.Play();
        }

        yield return new WaitForSeconds(kiraClip.length);

        // 👉 애니메이션 복귀
        if (kiraAnimator != null && !string.IsNullOrEmpty(idleStateName))
            kiraAnimator.Play(idleStateName);

        // ✅ Emission 효과들 전체 실행
        if (isLet && emissionTargets != null)
        {
            foreach (var target in emissionTargets)
            {
                if (target != null)
                {
                    target.isLet = true;
                    Debug.Log($"✨ Emission 활성화: {target.name}");
                }
            }
        }

        // ✅ FloatingEffect도 같이 실행
        if (isLet && floatingTarget != null)
        {
            floatingTarget.enabled = true;
        }
    }
}
