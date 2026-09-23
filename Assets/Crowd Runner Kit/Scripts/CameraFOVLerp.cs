using UnityEngine;
using System.Collections;

public class CameraFOVLerp : MonoBehaviour
{
    public float newFOV = 60f; // Target FOV value

    private Camera mainCamera;
    private float startingFOV;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        startingFOV = mainCamera.fieldOfView;
    }

    public void FOVZoom(float amount)
    {
        StartCoroutine(LerpFOV(amount, 0.1f));
    }

    private IEnumerator LerpFOV(float amount, float transitionTime)
    {
        float newFOV = startingFOV + amount;

        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            mainCamera.fieldOfView = Mathf.Lerp(startingFOV, newFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.fieldOfView = newFOV;

        elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            mainCamera.fieldOfView = Mathf.Lerp(newFOV, startingFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.fieldOfView = startingFOV;
    }
}
