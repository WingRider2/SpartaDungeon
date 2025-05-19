using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : Singleton<PlayerController>
{
    private CharacterController characterController;
    private PlayerInputHandler playerInputHandler;

    Vector2 moveInput; // 이동입력
    Vector3 velocity; //플레이어의 이동 정보
    public float walkSpeed;

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
        HandleMove();
    }
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    void HandleMove()
    {
        // 이동 입력
        Vector3 input = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 worldMove = transform.TransformDirection(input) * walkSpeed;

        // 중력 적용
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -1f;
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // 실제 이동
        characterController.Move((worldMove + velocity) * Time.deltaTime);

    }
}
