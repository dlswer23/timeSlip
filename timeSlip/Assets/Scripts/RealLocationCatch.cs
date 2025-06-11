using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealLocationCatch : MonoBehaviour
{
    [Header("Door Settings")]
    public List<DoorController> doorControllers;

    [Header("Animation Settings")]
    public float delayBeforeWalkAnim = 5f;

    [Header("Target")]
    public Animator targetAnimator;  // 🎯 Hans의 Animator를 인스펙터에 직접 연결

    private void OnTriggerEnter(Collider other)
    {
        // 태그 체크 제거 → 누구든 트리거되면 작동
        Debug.Log("트리거 진입 감지됨");
        StartCoroutine(HandleSequence());
    }

    private IEnumerator HandleSequence()
    {
        // 3초 후 문 열기
        yield return new WaitForSeconds(3f);
        foreach (DoorController door in doorControllers)
        {
            if (door != null)
            {
                Debug.Log("문 열림!");
                door.ToggleDoor();
            }
        }

        // 2초 추가 대기
        yield return new WaitForSeconds(2f);

        // Animator 실행
        if (targetAnimator != null)
        {
            Debug.Log("isWalk = true 설정");
            targetAnimator.SetBool("isWalk", true);
            yield return new WaitForSeconds(10f);
        }
        else
        {
            Debug.LogWarning("타겟 Animator가 연결되지 않았습니다!");
        }
    }
}
