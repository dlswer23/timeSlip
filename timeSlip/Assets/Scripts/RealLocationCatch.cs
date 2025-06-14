using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealLocationCatch : MonoBehaviour
{
    [Header("Door Settings")]
    public List<DoorController> doorControllers;

    [Header("Hans Script")]
    public isWalkingHans hansScript;  // 👈 인스펙터에 Hans GameObject에 붙은 스크립트 연결


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거 진입 감지됨");
    
        StartCoroutine(HandleSequence());
    }


    private IEnumerator HandleSequence()
    {
        // 🔹 1. 트리거 진입 후 3초 대기
        yield return new WaitForSeconds(3f);

        // 🔹 2. 문 열기
        foreach (DoorController door in doorControllers)
        {
            if (door != null)
            {
                Debug.Log("문 열림!");
                door.ToggleDoor();
            }
        }

        // 🔹 3. 문을 연 후 3초 추가 대기
        yield return new WaitForSeconds(3f);

        // 🔹 4. Hans 애니메이션 시퀀스 시작
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