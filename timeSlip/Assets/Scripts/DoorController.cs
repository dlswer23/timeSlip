using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [System.Serializable]
    public class DoorPivotData
    {
        public Transform pivot;
        public float openAngle;
    }

    [Header("Door Settings")]
    public List<DoorPivotData> doorPivots;
    public float openSpeed = 1f;
    public float doorOpenDuration = 10f; // ⏱ 문이 열린 상태 유지 시간

    [Header("Audio")]
    public AudioSource doorCloseSound;   // 🎵 문 닫힐 때 효과음

    private bool isOpen = false;
    private bool isMoving = false;

    public void ToggleDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(RotateDoors());

            // 🔹 문이 열릴 때만 닫기 예약
            if (!isOpen)
            {
                StartCoroutine(AutoCloseAfterDelay(doorOpenDuration));
            }
        }
    }

    private IEnumerator RotateDoors()
    {
        isMoving = true;

        List<Quaternion> startRotations = new List<Quaternion>();
        List<Quaternion> endRotations = new List<Quaternion>();

        foreach (var doorData in doorPivots)
        {
            Quaternion startRotation = doorData.pivot.rotation;
            Quaternion endRotation;

            if (isOpen)
            {
                endRotation = startRotation * Quaternion.Euler(0, -doorData.openAngle, 0);
            }
            else
            {
                endRotation = startRotation * Quaternion.Euler(0, doorData.openAngle, 0);
            }

            startRotations.Add(startRotation);
            endRotations.Add(endRotation);
        }

        float time = 0;

        while (time < openSpeed)
        {
            time += Time.deltaTime;
            float t = time / openSpeed;

            for (int i = 0; i < doorPivots.Count; i++)
            {
                doorPivots[i].pivot.rotation = Quaternion.Slerp(startRotations[i], endRotations[i], t);
            }

            yield return null;
        }

        // 최종 각도 보정
        for (int i = 0; i < doorPivots.Count; i++)
        {
            doorPivots[i].pivot.rotation = endRotations[i];
        }

        // 🔊 문 닫혔을 때 효과음 재생
        if (isOpen && doorCloseSound != null)
        {
            doorCloseSound.Play();
        }

        isOpen = !isOpen;
        isMoving = false;
    }

    private IEnumerator AutoCloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isOpen && !isMoving)
        {
            ToggleDoor();  // 닫기 호출
        }
    }
}
