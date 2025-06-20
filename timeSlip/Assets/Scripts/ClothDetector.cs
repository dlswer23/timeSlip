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
    public AudioSource audioSource;         // ✅ 사운드 재생용 AudioSource
    public AudioClip clothEnterClip;        // ✅ Cloth 들어올 때 효과음

    private bool sceneLoadingStarted = false; // ✅ 중복 실행 방지용

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(clothTag))
        {
            clothCount++;
            Debug.Log($"🧺 Cloth 들어옴! 현재 개수: {clothCount}");

            // ✅ 사운드 재생
            if (audioSource != null && clothEnterClip != null)
            {
                audioSource.PlayOneShot(clothEnterClip);
            }

            // ✅ 목표 도달 시 씬 전환 (딜레이 포함)
            if (clothCount >= targetCount && !sceneLoadingStarted)
            {
                sceneLoadingStarted = true;
                StartCoroutine(DelayedSceneLoad(5f)); // 5초 대기 후 전환
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
