using System.Collections;
using UnityEngine;

public class DelayedVoice : MonoBehaviour
{
    public AudioSource voiceSource;
    public float delayInSeconds = 3f;

    [Header("애니메이션")]
    public Animator animator;
    public string animationStateName = "Pointing";
    public string idleStateName = "HumanoidIdle"; // ✅ 복귀할 상태 이름

    void Start()
    {
        StartCoroutine(PlayVoiceAfterDelay());
    }

    private IEnumerator PlayVoiceAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (animator != null && !string.IsNullOrEmpty(animationStateName))
        {
            animator.Play(animationStateName);
            Debug.Log($"🕹 애니메이션 '{animationStateName}' 실행됨");
        }

        if (voiceSource != null)
        {
            voiceSource.Play();
            Debug.Log("🎤 오디오 재생 시작");

            // 👉 애니메이션 클립 재생 후 기다리기
            yield return new WaitForSeconds(voiceSource.clip.length);

            if (animator != null && !string.IsNullOrEmpty(idleStateName))
            {
                animator.Play(idleStateName);
                Debug.Log($"↩ 애니메이션 '{idleStateName}'로 복귀");
            }
        }
    }
}
