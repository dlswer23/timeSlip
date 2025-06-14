using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationSequence : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> narrationClips;
    public float delayBetweenClips = 1f;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.loop = false; // ✅ 오디오 루프 해제
        }

        StartCoroutine(PlayNarrationSequenceOnce());
    }

    private IEnumerator PlayNarrationSequenceOnce()
    {
        if (narrationClips == null || narrationClips.Count == 0 || audioSource == null)
        {
            Debug.LogWarning("오디오 소스나 클립이 설정되지 않았습니다.");
            yield break;
        }

        foreach (AudioClip clip in narrationClips)
        {
            audioSource.clip = clip;
            audioSource.Play();
            Debug.Log($"▶️ 재생 중: {clip.name}");

            yield return new WaitUntil(() => !audioSource.isPlaying);
            yield return new WaitForSeconds(delayBetweenClips);
        }

        Debug.Log("🎬 모든 나레이션 완료!");
    }
}
