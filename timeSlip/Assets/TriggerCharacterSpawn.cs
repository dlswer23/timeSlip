using UnityEngine;

public class TriggerCharacterSpawn : MonoBehaviour
{
    public GameObject newCharacter;      // 등장할 인물
    public Transform startPosition;      // 인물이 나타날 위치
    public float delay = 3f;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            Debug.Log("[TriggerCharacterSpawn] 플레이어가 트리거 존에 들어왔습니다. " +
                      $"3초 후 '{newCharacter.name}'이 등장합니다.");

            Invoke("SpawnCharacter", delay);
        }
    }

    void SpawnCharacter()
    {
        newCharacter.SetActive(true);
        newCharacter.transform.position = startPosition.position;

        Debug.Log($"[TriggerCharacterSpawn] '{newCharacter.name}'이 {startPosition.position} 위치에 등장했습니다.");
    }
}
