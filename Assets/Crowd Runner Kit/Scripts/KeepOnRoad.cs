using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnRoad : MonoBehaviour
{
    /// <summary>
    /// Keeps an object on the road each frame
    /// </summary>
    void Update()
    {
        int layerMask = 1 << 3;

        // Raycast downwards from the character
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            float newY = 0;
            transform.position = hit.point + new Vector3(0, newY, 0);
        }
    }
}
