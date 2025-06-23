using System.Collections;
using UnityEngine;

public class DefeatedToFistPump : MonoBehaviour
{
    public Animator animator;            // Animator 컴포넌트 연결
    public float delayTime = 14f;        // 전환까지 기다릴 시간

    void Start()
    {
        Invoke("TriggerFistPump", delayTime);  // 지정 시간 뒤 전환
    }

    void TriggerFistPump()
    {
        if (animator != null)
        {
            Debug.Log("🔥 Defeated → FistPump 전환!");
            animator.SetBool("isFistPump3", true);
        }
    }
}
