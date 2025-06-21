using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    public Animator handAnimator;

    [Header("Input Action")]
    public InputActionProperty gripAction; // ← 트리거 입력 연결용

    private void Update()
    {
        float gripValue = gripAction.action.ReadValue<float>();
        Debug.Log("Right Grip: " + gripAction.action.ReadValue<float>());
        handAnimator.SetFloat("GripValue", gripValue); // 애니메이터 파라미터에 값 넣기
    }
}
