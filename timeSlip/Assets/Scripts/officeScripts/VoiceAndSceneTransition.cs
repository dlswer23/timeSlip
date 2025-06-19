using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VoiceAndSceneTransition : MonoBehaviour
{
    public AudioSource voiceSource;        // 캐릭터 음성 소스
    public string sceneToLoad = "SadScene"; // 전환할 씬 이름
    public float voiceStartDelay = 4f;     // 시작 지연 (4초)

    void Start()
    {
        StartCoroutine(DelayedVoiceAndScene());
    }

    IEnumerator DelayedVoiceAndScene()
    {
        // 1. 4초 대기
        yield return new WaitForSeconds(voiceStartDelay);

        // 2. 음성 재생
        if (voiceSource != null && voiceSource.clip != null)
        {
            voiceSource.Play();
            Debug.Log("🗣️ 캐릭터 대사 시작됨!");

            // 3. 음성 길이만큼 기다렸다가 씬 전환
            yield return new WaitForSeconds(voiceSource.clip.length);
            Debug.Log("🎬 음성 끝! 씬 전환 → " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("❌ AudioSource 또는 Clip이 연결되지 않았습니다!");
        }
    }
}
