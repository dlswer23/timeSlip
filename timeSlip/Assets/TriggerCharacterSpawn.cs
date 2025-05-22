using UnityEngine;

public class TriggerCharacterSpawn : MonoBehaviour
{
    public GameObject newCharacter;      // ������ �ι�
    public Transform startPosition;      // �ι��� ��Ÿ�� ��ġ
    public float delay = 3f;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            Debug.Log("[TriggerCharacterSpawn] �÷��̾ Ʈ���� ���� ���Խ��ϴ�. " +
                      $"3�� �� '{newCharacter.name}'�� �����մϴ�.");

            Invoke("SpawnCharacter", delay);
        }
    }

    void SpawnCharacter()
    {
        newCharacter.SetActive(true);
        newCharacter.transform.position = startPosition.position;

        Debug.Log($"[TriggerCharacterSpawn] '{newCharacter.name}'�� {startPosition.position} ��ġ�� �����߽��ϴ�.");
    }
}
