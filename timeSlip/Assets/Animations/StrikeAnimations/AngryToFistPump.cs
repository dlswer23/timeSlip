using System.Collections;
using UnityEngine;

public class AngryToFistPump : MonoBehaviour
{
    public Animator animator; // Animator 연결 (캐릭터에 할당)

    void Start()
    {
        // 12초 뒤 애니메이션 전환 호출
        Invoke("TriggerFistPump", 14f);
    }

    void TriggerFistPump()
    {
        Debug.Log("🎬 Fist Pump 애니메이션 전환 시작");
        animator.SetBool("isFistPump2", true);
    }
}
