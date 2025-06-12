using System.Collections;
using UnityEngine;

public class HansMover : MonoBehaviour
{
    [Header("Hans Settings")]
    public Transform hans;
    public Animator hansAnimator;
    public float walkSpeed = 5f;

    [Header("Destination")]
    public Vector2 target2DPosition = new Vector2(-0.217f,-0.04900002f);
    public float arrivalThreshold = 0.1f;

    private bool hasArrived = false;
    private bool shouldMoveToZero = false;

    void Update()
    {
        // (1) 걷기: 목표 위치까지
        if (hansAnimator.GetBool("isWalk") && !hasArrived)
        {
            Vector3 currentPosition = hans.position;
            Vector3 targetPosition = new Vector3(
                target2DPosition.x,
                currentPosition.y,
                target2DPosition.y
            );

            float step = walkSpeed * Time.deltaTime;
            hans.position = Vector3.MoveTowards(currentPosition, targetPosition, step);

            Vector2 current2D = new Vector2(hans.position.x, hans.position.z);
            if (Vector2.Distance(current2D, target2DPosition) < arrivalThreshold)
            {
                Debug.Log("✅ Hans가 목표 지점에 도착했습니다.");
                hasArrived = true;

                hansAnimator.SetBool("isWalk", false);
                StartCoroutine(HandleArrivalSequence());
            }
        }

        // (2) isTurn이 true인 동안 → (0, 0)으로 이동
        if (shouldMoveToZero)
        {
            Vector3 current = hans.position;
            Vector3 target = new Vector3(0f, current.y, 0f);

            float step = walkSpeed * Time.deltaTime;
            hans.position = Vector3.MoveTowards(current, target, step);
        }
    }

    private IEnumerator HandleArrivalSequence()
    {
        hansAnimator.SetBool("isWait", true);
        yield return new WaitForSeconds(0.5f);
        hansAnimator.SetBool("isWait", false);

        yield return new WaitForSeconds(10f);
        hansAnimator.SetBool("isTurn", true);
        Debug.Log("🔁 isTurn = true");

        // 이동 플래그 켜기
        shouldMoveToZero = true;
    }
}
