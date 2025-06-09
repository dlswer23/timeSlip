using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothDetector : MonoBehaviour
{
    public string clothTag = "Cloth";  // Cloth 오브젝트의 태그 이름
    public int clothCount = 0;
    public int targetCount = 5;        // 목표 개수
    public string nextSceneName = "OfficeScene";  // 전환할 씬 이름

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(clothTag))
        {
            clothCount++;
            Debug.Log($"Cloth 들어옴! 현재 개수: {clothCount}");

            if (clothCount >= targetCount)
            {
                LoadNextScene();  // 씬 전환 실행
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(clothTag))
        {
            clothCount--;
            Debug.Log($"Cloth 나감! 현재 개수: {clothCount}");
        }
    }

    private void LoadNextScene()
    {
        Debug.Log("목표 수량 달성! OfficeScene으로 전환합니다.");
        SceneManager.LoadScene(nextSceneName);
    }
}
