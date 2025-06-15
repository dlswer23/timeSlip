using System.Collections;
using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    public AudioSource bgmSource;      // 배경음
    public AudioSource voice1Source;   // 대사 1
    public AudioSource voice2Source;   // 대사 2

    void Start()
    {
        // 🎵 배경음 바로 재생
        if (bgmSource != null)
            bgmSource.Play();

        // 🎬 대사 재생 루틴 시작
        StartCoroutine(PlayDialogues());
    }

    IEnumerator PlayDialogues()
    {
        yield return new WaitForSeconds(3f); // ⏱️ 3초 대기 후

        if (voice1Source != null)
            voice1Source.Play(); // 🎙️ 대사 1 재생

        // 🕒 대사 1의 길이만큼 기다림
        float voice1Length = voice1Source.clip.length;
        yield return new WaitForSeconds(voice1Length + 1f); // 끝나고 +1초 대기

        if (voice2Source != null)
            voice2Source.Play(); // 🎙️ 대사 2 재생
    }
}
