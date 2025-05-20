using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float padPower;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")){
            Debug.Log("발사");
            collision.rigidbody.AddForce(Vector2.up * padPower, ForceMode.Impulse);
        }

    }
}
