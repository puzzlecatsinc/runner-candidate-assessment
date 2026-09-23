using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float minX = -2;
    public float maxX = 2;
    public float speed = 2;
    private bool movingRight = true;

    void Update()
    {
        if (PlayerController.Instance.Dead || !GameManager.Instance.GameStarted) return;
        float movement = movingRight ? speed * Time.deltaTime : -speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(movement, 0f, 0f);
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
