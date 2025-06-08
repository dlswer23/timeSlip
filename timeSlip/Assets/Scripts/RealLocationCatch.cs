using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealLocationCatch : MonoBehaviour
{
    [Header("Boss Move Settings")]
    public Transform bossToMove;
    public Vector3 targetPosition;
    public float moveSpeed = 2f;
    public float smoothTime = 0.6f;
    public float delayBeforeMove = 5f;
    public float delayBeforeReturn = 10f;

    [Header("Door Settings")]
    public List<DoorController> doorControllers;  // ✅ 리스트로 변경

    private Vector3 originalPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            // 애니메이터에 트리거 발동!
            Animator animator = bossToMove.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("StartWalking");
            }

            // 기존 로직
            if (bossToMove != null)
            {
                originalPosition = bossToMove.position;
                StartCoroutine(MoveBoss());
            }

            foreach (DoorController door in doorControllers)
            {
                if (door != null) door.ToggleDoor();
            }
        }
    }


    private IEnumerator MoveBoss()
    {
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

        // 문 닫기
        foreach (DoorController door in doorControllers)
        {
            if (door != null)
            {
                Debug.Log("Closing the door!");
                door.ToggleDoor();
            }
        }
    }
}
