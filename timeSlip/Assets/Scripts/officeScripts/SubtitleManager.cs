using UnityEngine;
using TMPro;

public class MultiAudioSubtitle : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        public string subtitleText;     // 자막 텍스트
        public float delayBeforeNext = 1f; // 다음 대사까지 대기 시간
    }

    public AudioSource[] audioSources;          // 오디오 소스 배열 (3개)
    public SubtitleLine[] subtitles;            // 자막 배열
    public TextMeshProUGUI subtitleTextUI;      // TMP 자막 UI

    void Start()
    {
        StartCoroutine(PlaySubtitlesWithAudio());
    }

    private System.Collections.IEnumerator PlaySubtitlesWithAudio()
    {
        for (int i = 0; i < audioSources.Length && i < subtitles.Length; i++)
        {
            subtitleTextUI.text = subtitles[i].subtitleText;

            if (audioSources[i] != null && audioSources[i].clip != null)
            {
                audioSources[i].Play();
                yield return new WaitForSeconds(audioSources[i].clip.length);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }

            subtitleTextUI.text = ""; // 자막 꺼주기
            yield return new WaitForSeconds(subtitles[i].delayBeforeNext);
        }
    }
}
