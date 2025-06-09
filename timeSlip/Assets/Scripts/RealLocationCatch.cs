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
    public float delayBeforeMove = 5f;       // Hans 움직이기까지 대기 시간
    public float delayBeforeReturn = 10f;

    [Header("Door Settings")]
    public List<DoorController> doorControllers;  // ✅ 여러 개 문 열기용 리스트

    private Vector3 originalPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
            originalPosition = bossToMove.position;

            // 문 열기와 보스 이동 분리 실행
            StartCoroutine(HandleSequence());
        }
    }

    private IEnumerator HandleSequence()
    {
        // 3초 후 문 열기
        yield return new WaitForSeconds(3f);
        foreach (DoorController door in doorControllers)
        {
            if (door != null)
            {
                Debug.Log("Door opening triggered!");
                door.ToggleDoor();
            }
        }

        // 2초 추가 대기 (총 5초 후 Hans 애니메이션 실행)
        yield return new WaitForSeconds(2f);

        // Hans 애니메이션 트리거 발동
        Animator animator = bossToMove.GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log("Triggering Hans animation!");
            animator.SetTrigger("StartWalking");
        }

        // Hans 이동
        StartCoroutine(MoveBoss());
    }

    private IEnumerator MoveBoss()
    {
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
        Debug.Log("Hans arrived at position.");

        yield return new WaitForSeconds(delayBeforeReturn);
        Debug.Log("Hans returning!");

        elapsedTime = 0f;
        while (elapsedTime < smoothTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / smoothTime;
            bossToMove.position = Vector3.Lerp(endPosition, originalPosition, t);
            yield return null;
        }

        bossToMove.position = originalPosition;
        Debug.Log("Hans returned.");

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
