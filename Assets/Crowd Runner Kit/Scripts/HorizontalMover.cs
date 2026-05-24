using UnityEngine;

/// <summary>
/// An obstacle that moves left and right
/// </summary>
public class HorizontalMover : MonoBehaviour
{
    public float minX = -2; 
    public float maxX = 2; 
    public float speed = 2; 
    private bool movingRight = true; 

    /// <summary>
    /// Bounce the object between left and right horizontally
    /// </summary>
    void Update()
    {
        if (PlayerController.Instance.Dead || !GameManager.Instance.GameStarted) return;

        // Calculate the new position based on the current movement direction and speed
        float movement = movingRight ? speed * Time.deltaTime : -speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(movement, 0f, 0f);

        // Check if the new position is within the specified boundaries
        if (newPosition.x < minX)
        {
            newPosition.x = minX; 
            movingRight = true; 
        }
        else if (newPosition.x > maxX)
        {
            newPosition.x = maxX; 
            movingRight = false; 
        }

        transform.position = newPosition;
    }
}
