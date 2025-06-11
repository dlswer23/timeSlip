using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HansLocationCatch : MonoBehaviour
{
    public Animator hansAnimator;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Hans"))
        {
            Debug.Log("🟡 Hans 도착 → isWait = true");
            hansAnimator.SetBool("isWait", true);
            Debug.Log("감지감지완료   → isWait = true");
            hasTriggered = true;
        }
    }
}
