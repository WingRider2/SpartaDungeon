using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInputHandler playerInputHandler;

    [Header("Movement")]
    Vector2 moveInput; // 이동입력
    Vector3 velocity; //플레이어의 이동 정보
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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveAndJump();
    }
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    public void SetJumpInput()
    {
        if (characterController.isGrounded) verticalVelocity = Mathf.Sqrt(jumpPower * -2f * Physics.gravity.y);
    }
    void HandleMoveAndJump()
    {
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; //음수를 넣어 착지 확인
        }

        // 이동 입력
        Vector3 input = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 worldMove = transform.TransformDirection(input) * walkSpeed;

        // 중력 적용
        float g = Physics.gravity.y * (verticalVelocity < 0 ? gravityScale : 1f );
        verticalVelocity += g * Time.deltaTime;

        worldMove.y = verticalVelocity;
        

        // 실제 이동
        characterController.Move((worldMove + velocity) * Time.deltaTime);

    }

}
