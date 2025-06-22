using UnityEngine;
using System.Collections;

public class SequentialAudioPlayer : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

    void Start()
    {
        StartCoroutine(PlayAudiosSequentially());
    }

    IEnumerator PlayAudiosSequentially()
    {
        // 🎧 오디오 1 재생
        if (audio1 != null)
        {
            audio1.Play();
            yield return new WaitForSeconds(audio1.clip.length);
        }

        // 🎧 오디오 2 재생
        if (audio2 != null)
        {
            audio2.Play();
            yield return new WaitForSeconds(audio2.clip.length);
        }

        // ⏱️ 1.5초 대기
        yield return new WaitForSeconds(1.5f);

        // 🎧 오디오 3 재생
        if (audio3 != null)
        {
            audio3.Play();
            yield return new WaitForSeconds(audio3.clip.length);
        }

        // 🎉 모두 끝난 후 동작은 여기에 추가 가능
    }
}
