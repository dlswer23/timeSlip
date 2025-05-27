using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneController : MonoBehaviour
{
    [Header("Boss Move Settings")]
    public Transform bossToMove;
    public Vector3 targetPosition;
    public float moveSpeed = 2f;
    public float smoothTime = 0.6f;
    public float delayBeforeMove = 5f;
    public float delayBeforeReturn = 10f;  // ✅ 10초 뒤에 다시 돌아갈 딜레이

    [Header("Door Settings")]
    public DoorController doorController;

    private Vector3 originalPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            // 보스 이동
            if (bossToMove != null)
            {
                Debug.Log("BossMoveTest starts moving!");
                originalPosition = bossToMove.position;   // ✅ 처음 위치 저장
                StartCoroutine(MoveBoss());
            }

            // 문 열기
            if (doorController != null)
            {
                Debug.Log("Door opening triggered!");
                doorController.ToggleDoor();
            }
            else
            {
                Debug.LogWarning("DoorController is NOT connected!");
            }
        }
    }

    private IEnumerator MoveBoss()
    {
        // 보스 이동 (목표 위치)
        yield return new WaitForSeconds(delayBeforeMove);

        Vector3 startPosition = bossToMove.position;
        Vector3 endPosition = targetPosition;
        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / smoothTime;
            bossToMove.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        bossToMove.position = endPosition;
        Debug.Log("BossMoveTest arrived!");

        // ✅ 10초 후에 다시 원래 자리로 이동!
        yield return new WaitForSeconds(delayBeforeReturn);
        Debug.Log("BossMoveTest returning!");

        elapsedTime = 0f;
        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / smoothTime;
            bossToMove.position = Vector3.Lerp(endPosition, originalPosition, t);
            yield return null;
        }

        bossToMove.position = originalPosition;
        Debug.Log("BossMoveTest returned!");

        // ✅ 돌아오면 문도 다시 닫기!
        if (doorController != null)
        {
            Debug.Log("Closing the door!");
            doorController.ToggleDoor();
        }
    }
}
