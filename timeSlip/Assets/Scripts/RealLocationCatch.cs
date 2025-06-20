using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealLocationCatch : MonoBehaviour
{
    [Header("Door Settings")]
    public List<DoorController> doorControllers;

    [Header("Hans Script")]
    public isWalkingHans hansScript;

    private bool hasTriggered = false; // 🔒 중복 방지용 플래그

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (hasTriggered) return; // 🚫 이미 실행됐다면 무시

        hasTriggered = true; // ✅ 실행 상태 기억
        Debug.Log("트리거 진입 감지됨");

        StartCoroutine(HandleSequence());
    }

    private IEnumerator HandleSequence()
    {
        yield return new WaitForSeconds(3f);

        foreach (DoorController door in doorControllers)
        {
            if (door != null)
            {
                Debug.Log("문 열림!");
                door.ToggleDoor();
            }
        }

        yield return new WaitForSeconds(3f);

        if (hansScript != null)
        {
            Debug.Log("Hans 애니메이션 실행");
            hansScript.StartHansSequenceExternally();
        }
        else
        {
            Debug.LogWarning("HansScript가 연결되지 않았습니다!");
        }
    }
}
