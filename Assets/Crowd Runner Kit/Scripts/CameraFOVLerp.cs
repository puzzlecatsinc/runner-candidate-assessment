using UnityEngine;
using System.Collections;

/// <summary>
/// A camera FOV lerping effect script
/// </summary>
public class CameraFOVLerp : MonoBehaviour
{
    public float newFOV = 60f; // Target FOV value

    private Camera mainCamera;
    private float startingFOV;
    private bool isLerping;

    /// <summary>
    /// Initializes the variables
    /// </summary>
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        startingFOV = mainCamera.fieldOfView;
        isLerping = false;
    }

    /// <summary>
    /// Does a FOV zoom
    /// </summary>
    /// <param name="amount"></param>
    public void FOVZoom(float amount)
    {
        StartCoroutine(LerpFOV(amount, 0.1f));
    }

    /// <summary>
    /// Lerps the fov in/out
    /// </summary>
    /// <param name="amount">Amount to lerp</param>
    /// <param name="transitionTime">Time to take</param>
    private IEnumerator LerpFOV(float amount, float transitionTime)
    {
        float newFOV = startingFOV + amount;
        isLerping = true;

        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            mainCamera.fieldOfView = Mathf.Lerp(startingFOV, newFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final FOV is set exactly to the target value
        mainCamera.fieldOfView = newFOV;

        elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            float t = elapsedTime / transitionTime;
            mainCamera.fieldOfView = Mathf.Lerp(newFOV, startingFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final FOV is set exactly to the starting value
        mainCamera.fieldOfView = startingFOV;

        isLerping = false;
    }
}
