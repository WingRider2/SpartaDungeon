using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    //private CharacterController characterController;
    private PlayerInputHandler _playerInputHandler;
    private Rigidbody _rigidbody;

    [Header("Movement")]
    Vector2 moveInput; // 이동입력
    float verticalVelocity = 0f;
    public float walkSpeed;
    public float jumpPower;
    public LayerMask groundLayerMask;
    public float gravityScale;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;//회전 민감도

    private Vector2 mouseDelta;

    public Action inventory;
    private void Awake()
    {
        
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        CameraLook();
    }
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    public void SetJumpInput(float held)
    {        
        if(IsGrounded()) _rigidbody.AddForce(Vector2.up * jumpPower * held, ForceMode.Impulse);
    }
    public void SetMouseDelta(Vector2 mouseDelta)
    {
        this.mouseDelta = mouseDelta;
    }
    void HandleMove()
    {
        Vector3 dir = transform.forward * moveInput.y + transform.right * moveInput.x;
        dir *= walkSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down*3.0f),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down*3.0f),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down*3.0f),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down*3.0f)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i],groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

}
