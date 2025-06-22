using UnityEngine;
using UnityEngine.SceneManagement; // 👈 씬 전환에 필요
using System.Collections;

public class BossAudioSource : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;
    public float delayBetweenLines = 0.3f;
    public float startDelay = 4f; // 🎯 씬 시작 후 4초 기다림
    public float endDelayBeforeSceneChange = 1f; // 🎬 마지막 대사 후 씬 전환까지의 여유시간

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(PlayDialogueSequence());
    }

    IEnumerator PlayDialogueSequence()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            audioSource.clip = clips[i];
            audioSource.Play();
            yield return new WaitForSeconds(clips[i].length + delayBetweenLines);
        }

        Debug.Log("🎤 대사 끝났고 이제 SadScene으로 이동할게!");

        // 마지막 대사 끝나고 잠깐 여유
        yield return new WaitForSeconds(endDelayBeforeSceneChange);

        // 🎬 SadScene으로 자동 전환
        SceneManager.LoadScene("SadScene");
    }
}
