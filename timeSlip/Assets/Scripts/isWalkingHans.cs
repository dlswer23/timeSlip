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
        // ❌ StartCoroutine(PlayHansSequence()); // 자동 실행 제거
    }

    // ✅ 외부에서 호출할 때만 실행됨
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

        yield return StartCoroutine(RotateBy(Vector3.up * 180f, 1.2f));

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
