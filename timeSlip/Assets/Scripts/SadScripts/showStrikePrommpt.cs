using UnityEngine;

public class ShowStrikePrompt : MonoBehaviour
{
    public GameObject strikePromptUI;  // 질문 UI

    void Start()
    {
        // 8초 후에 ShowUI 함수 호출
        Invoke("ShowUI", 8f);
    }

    void ShowUI()
    {
        if (strikePromptUI != null)
        {
            strikePromptUI.SetActive(true);
        }
    }
}
