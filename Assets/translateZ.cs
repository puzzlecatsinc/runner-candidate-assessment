using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translateZ : MonoBehaviour
{
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        //float OffsetZ = Time.time * speed;

        //Vector3 myVector = this.GetComponent<Transform>().position;

        //myVector += new Vector3(0, 0, speed);

        this.GetComponent<Transform>().position += new Vector3(0, 0, speed);
    }
}