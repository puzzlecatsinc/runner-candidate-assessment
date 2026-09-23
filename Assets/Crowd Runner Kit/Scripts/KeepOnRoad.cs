using UnityEngine;

public class KeepOnRoad : MonoBehaviour
{

    void Update()
    {
        int layerMask = 1 << 3;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            float newY = 0;
            transform.position = hit.point + new Vector3(0, newY, 0);
        }
    }
}
