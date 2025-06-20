using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class isWalkingHans : MonoBehaviour
{
    public Transform target1;
    public Transform originPosition;
    public AudioSource voiceSource;

    public float moveSpeed = 1.2f;

    private Animator animator;
    private bool isSequenceStarted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartHansSequenceExternally()
    {
        if (!isSequenceStarted)
        {
            StartCoroutine(PlayHansSequence());
        }
    }

    IEnumerator PlayHansSequence()
    {
        isSequenceStarted = true;

        animator.Play("walking");
        yield return StartCoroutine(MoveToPosition(target1.position));

        animator.Play("talking");
        voiceSource.Play();
        yield return new WaitUntil(() => !voiceSource.isPlaying);
        yield return new WaitForSeconds(2f);

        // 🔄 회전 애니메이션 실행
        animator.Play("turning");
        yield return new WaitForSeconds(1.2f);  // turning 애니메이션 실행 (회전 포함됨)

        // ✅ 목적지 바라보게 함 (걷기 애니메이션과 방향 일치)
        transform.LookAt(originPosition.position);

        animator.Play("walking");
        yield return StartCoroutine(MoveToPosition(originPosition.position));

        animator.Play("idle");
    }

    IEnumerator MoveToPosition(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveForwardDistance(float distance)
    {
        float moved = 0f;

        while (moved < distance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position += transform.forward * step;
            moved += step;
            yield return null;
        }
    }

    IEnumerator RotateBy(Vector3 angle, float duration)
    {
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.Euler(transform.eulerAngles + angle);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;
    }
}
