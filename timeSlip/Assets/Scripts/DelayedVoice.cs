using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedVoice : MonoBehaviour
{
    public AudioSource voiceSource;
    public float delayInSeconds = 3f;

    void Start()
    {
        StartCoroutine(PlayVoiceAfterDelay());
    }

    private IEnumerator PlayVoiceAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        voiceSource.Play();
    }
}
