using System.Collections;
using UnityEngine;

public class TriggerFistPump : MonoBehaviour
{
    public Animator characterAnimator; // 캐릭터에 붙은 Animator

    void Start()
    {
        // 12초 뒤에 전환 실행
        Invoke("TriggerAnimation", 15f);
    }

    void TriggerAnimation()
    {
        characterAnimator.SetBool("isFistPump", true);
    }
}
