using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGround : MonoBehaviour
{
    Rigidbody body;

    public float dorpSpeed;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        body.velocity = Vector3.down * dorpSpeed;
    }
}
