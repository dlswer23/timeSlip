using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothDetector : MonoBehaviour
{
    [Header("Cloth 조건")]
    public string clothTag = "Cloth";
    public int clothCount = 0;
    public int targetCount = 5;
    public string nextSceneName = "OfficeScene";

    [Header("사운드")]
    public AudioSource audioSource;             // ✅ 공용 AudioSource
    public AudioClip clothEnterClip;            // Cloth 들어올 때 재생
    public AudioClip clothCompleteClip;         // Cloth 5개 도달 시 재생

    private bool sceneLoadingStarted = false;
    private bool hasPlayedCompleteSound = false; // ✅ 중복 방지

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(clothTag))
        {
            clothCount++;
            Debug.Log($"🧺 Cloth 들어옴! 현재 개수: {clothCount}");

            // ▶️ 개별 Cloth 들어올 때 소리
            if (audioSource != null && clothEnterClip != null)
            {
                audioSource.PlayOneShot(clothEnterClip);
            }

            // ✅ Cloth 5개 도달 시 사운드 재생 (1회만)
            if (clothCount >= targetCount && !hasPlayedCompleteSound)
            {
                hasPlayedCompleteSound = true;
                if (audioSource != null && clothCompleteClip != null)
                {
                    audioSource.PlayOneShot(clothCompleteClip);
                    Debug.Log("🎵 Cloth 5개 도달 사운드 재생!");
                }
            }

            // ✅ 씬 전환 예약 (한 번만)
            if (clothCount >= targetCount && !sceneLoadingStarted)
            {
                sceneLoadingStarted = true;
                StartCoroutine(DelayedSceneLoad(2f));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(clothTag))
        {
            clothCount--;
            Debug.Log($"👕 Cloth 나감! 현재 개수: {clothCount}");
        }
    }

    private IEnumerator DelayedSceneLoad(float delay)
    {
        Debug.Log($"🎯 Cloth 모두 감지됨! {delay}초 후 {nextSceneName}로 이동합니다...");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}
