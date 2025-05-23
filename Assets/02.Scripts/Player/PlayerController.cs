using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    //private CharacterController characterController;
    private PlayerInputHandler _playerInputHandler;
    private Rigidbody _rigidbody;
    private PlayerCondition _condition;

    public Transform handHoldPoint;
    [Header("Movement")]
    Vector2 moveInput; // 이동입력
    public PlayerMovementData playerMovementData;
    public PlayerMovementData MovementBuffDate;
    public LayerMask groundLayerMask;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;//회전 민감도

    private Vector2 mouseDelta;

    public bool isGripWall;
    [HideInInspector]
    public bool canLook = true;
    public Action inventory;

    public Transform reSponPos;
    private void Awake()
    {

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rigidbody = GetComponent<Rigidbody>();
        _condition = GetComponent<PlayerCondition>();

    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {        
        CameraLook();
    }
    void FixedUpdate()
    {
        HandleMove();
        if(this.transform.position.y < -10.0f)
        {
            this.transform.position = reSponPos.position;
        }
    }
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    public void JumpInput(float held)
    {
        Debug.Log("점프");
        if (IsGrounded()) _rigidbody.AddForce(Vector2.up * (playerMovementData.jumpPower + MovementBuffDate.jumpPower) * held, ForceMode.Impulse);
    }
    public void Dash()
    {
        if (_condition.UseStamina(1))
        {
            Vector3 direction = (
                (_rigidbody.velocity.magnitude < 1)
                ? transform.forward.normalized
                : new Vector3(_rigidbody.velocity.normalized.x, 0, _rigidbody.velocity.normalized.z));

            _rigidbody.AddForce(direction * (playerMovementData.dashPower + MovementBuffDate.dashPower), ForceMode.Impulse);

        }

    }
    public void UseBuff(BuffData data)
    {
        if (data.Duration < 0)
        {
            switch (data.type)
            {
                case BuffTupe.Speed:
                    playerMovementData.walkSpeed += data.Velue;
                    break;
                case BuffTupe.JumpPower:
                    playerMovementData.jumpPower += data.Velue;
                    break;
                case BuffTupe.DashPowe:
                    playerMovementData.dashPower += data.Velue;
                    break;
                default:
                    Debug.Log("잘못된 버프임");
                    break;
            }
        }
        else
        {
            switch (data.type)
            {
                case BuffTupe.Speed:
                    StartCoroutine(MovementBuffDate.addWalkSpeed(data));
                    break;
                case BuffTupe.JumpPower:
                    StartCoroutine(MovementBuffDate.addJumpPower(data));
                    break;
                case BuffTupe.DashPowe:
                    StartCoroutine(MovementBuffDate.addDashPower(data));
                    break;
                default:
                    Debug.Log("잘못된 버프임");
                    break;
            }
        }

    }
    public void SetMouseDelta(Vector2 mouseDelta)
    {
        this.mouseDelta = mouseDelta;
    }
    public void OpenInventory()
    {
        inventory?.Invoke();
        ToggleCursor();
    }
    void HandleMove()
    {        
        Vector3 dir = (transform.forward * moveInput.y + transform.right * moveInput.x)
                             * (playerMovementData.walkSpeed + MovementBuffDate.walkSpeed);

        float vertical;
        if (isGripWall)
        {
            vertical = moveInput.y * playerMovementData.walkSpeed;
        }
        else
        {
            vertical = _rigidbody.velocity.y;
        }
        _rigidbody.velocity = new Vector3(dir.x, vertical, dir.z);
        _rigidbody.useGravity = !isGripWall;
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };
        /*
        foreach (var ray in rays)
        {
            Debug.Log("야");
            Debug.DrawRay(ray.origin, Vector3.down, Color.red , 1.0f , depthTest: false);
        }
        */

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1.2f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
    void OnCollisionEnter(Collision collision) // 낙하 데미지
    {
        float impactSpeed = collision.relativeVelocity.magnitude;
        if (impactSpeed > 10) _condition.hit((int)(impactSpeed / 10));

        if (collision.transform.CompareTag("GripWall"))
        {
            isGripWall = true;
            transform.GetComponent<Rigidbody>().useGravity = !isGripWall; 
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("GripWall"))
        {
            isGripWall = false;
            transform.GetComponent<Rigidbody>().useGravity = !isGripWall;
        }
    }
    public void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
