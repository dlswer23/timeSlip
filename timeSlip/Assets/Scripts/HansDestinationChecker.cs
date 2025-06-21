using UnityEngine;

public class HansDestinationChecker : MonoBehaviour
{
    public Transform hans;
    public Animator hansAnimator;
    public Vector3 targetPosition;
    public float arrivalThreshold = 0.1f;

    private bool hasTriggered = false;

    void Update()
    {
        if (!hasTriggered && Vector3.Distance(hans.position, targetPosition) < arrivalThreshold)
        {
            Debug.Log("Hans has reached the destination. Triggering wait state.");
            hansAnimator.SetTrigger("ReachedDestination");

            // 애니메이션 상태를 변경하는 트리거를 설정합니다.
            hansAnimator.SetBool("isWait", true);
            Debug.Log("wait으로 변경됨");
            hasTriggered = true;
        }
    }
}
