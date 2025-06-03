using UnityEngine;

public class ClothBoxManager : MonoBehaviour
{
    public Transform clothParentPoint;   // 옷을 넣을 위치 (Transform으로 따로 지정 가능)

    public void AddCloth(GameObject clothPrefab)
    {
        if (clothParentPoint != null && clothPrefab != null)
        {
            // Cloth 프리팹을 인스턴스화해서 박스 자식으로 넣기
            GameObject newCloth = Instantiate(clothPrefab, clothParentPoint.position, clothParentPoint.rotation, clothParentPoint);

            // 로그 출력
            Debug.Log("옷이 성공적으로 박스에 들어갔습니다!");
        }
        else
        {
            Debug.LogWarning("ClothPrefab이나 ClothParentPoint가 비어 있습니다!");
        }
    }
}
