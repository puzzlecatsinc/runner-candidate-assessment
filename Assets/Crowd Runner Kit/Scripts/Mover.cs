using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool moveTowardsPlayer = false;
    /// <summary>
    /// Move the object in the negative Z direction
    /// </summary>
    void Update()
    {
        if (PlayerController.Instance.Dead || !GameManager.Instance.GameStarted) return;
        if (transform.GetChild(0).GetComponent<Animator>() != null && transform.GetChild(0).GetComponent<Animator>().GetBool("Dead")) return;
        Vector3 playerPos = PlayerController.Instance.GetComponent<Transform>().position;
        Vector3 currentPos = this.transform.position;
        float direction = 0; 
        if (currentPos.z - playerPos.z < 15  && moveTowardsPlayer) direction = playerPos.x > currentPos.x ? 0.2f : -0.2f;
        Vector3 directionTotravel = new Vector3(direction, 0f, -1f);
        transform.Translate( directionTotravel * GameManager.Instance.Speed * Time.deltaTime);
    }
}
