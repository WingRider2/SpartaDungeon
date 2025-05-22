using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveRange = 5.0f;
    public float speed = 1.0f;

    Vector3 startPos;


    void Start()
    {
        startPos = transform.position;

    }
    void Update()
    {
        float x = Mathf.PingPong(Time.time * speed, moveRange)- moveRange/2;

        transform.position = startPos + Vector3.right * x;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null, worldPositionStays: true);
        }
    }
}
