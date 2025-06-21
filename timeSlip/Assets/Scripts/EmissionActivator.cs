using System.Collections;
using UnityEngine;

public class EmissionActivator : MonoBehaviour
{
    public Material targetMaterial;
    public Color emissionColor = Color.white;
    public float delaySeconds = 30f;

    void Start()
    {
        StartCoroutine(EnableEmissionAfterDelay());
    }

    IEnumerator EnableEmissionAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);

        if (targetMaterial != null)
        {
            targetMaterial.EnableKeyword("_EMISSION");
            targetMaterial.SetColor("_EmissionColor", emissionColor);
        }
    }
}
