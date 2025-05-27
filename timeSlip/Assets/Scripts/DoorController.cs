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
    public List<DoorPivotData> doorPivots;   // pivot별로 다른 openAngle

    public float openSpeed = 1f;

    private bool isOpen = false;
    private bool isMoving = false;

    public void ToggleDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(RotateDoors());
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

        for (int i = 0; i < doorPivots.Count; i++)
        {
            doorPivots[i].pivot.rotation = endRotations[i];
        }

        isOpen = !isOpen;
        isMoving = false;
    }
}
