using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FadeTriggerOnGrab : MonoBehaviour
{
    public VRWhiteFade fadeScript; // VRWhiteFade 연결해줘야 함!

    private void OnEnable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("🖐 액자 잡힘! 2초 뒤 페이드 시작");
        Invoke("TriggerFade", 2f);
    }

    private void TriggerFade()
    {
        if (fadeScript != null)
        {
            fadeScript.StartWhiteFade();
        }
    }
}