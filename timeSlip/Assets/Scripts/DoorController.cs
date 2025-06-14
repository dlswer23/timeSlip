using System;
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
    public float doorOpenDuration = 10f;

    [Header("Audio")]
    public AudioSource doorCloseSound;

    // ✅ 문 닫힌 직후 외부에 알릴 수 있는 이벤트
    public Action OnDoorFullyClosed;

    private bool isOpen = false;
    private bool isMoving = false;

    public void ToggleDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(RotateDoors());

            // 문이 열릴 때만 자동 닫기 예약
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
                endRotation = startRotation * Quaternion.Euler(0, -doorData.openAngle, 0);
            else
                endRotation = startRotation * Quaternion.Euler(0, doorData.openAngle, 0);

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

        // 최종 보정
        for (int i = 0; i < doorPivots.Count; i++)
        {
            doorPivots[i].pivot.rotation = endRotations[i];
        }

        // 🔊 닫힐 때 효과음
        if (isOpen && doorCloseSound != null)
        {
            doorCloseSound.Play();
        }

        isOpen = !isOpen;
        isMoving = false;

        // ✅ 문이 '닫힌 직후'에만 콜백 실행
        if (!isOpen && OnDoorFullyClosed != null)
        {
            OnDoorFullyClosed.Invoke();
        }
    }

    private IEnumerator AutoCloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isOpen && !isMoving)
        {
            ToggleDoor(); // 문 닫기
        }
    }
}
