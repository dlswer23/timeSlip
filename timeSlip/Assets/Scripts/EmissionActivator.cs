using UnityEngine;

public class AnimationStopper : MonoBehaviour
{
    public Animator animator;
    public float stopTime = 14f;

    void Start()
    {
        Invoke("StopAnimation", stopTime);
    }

    void StopAnimation()
    {
        animator.enabled = false;
    }
}
