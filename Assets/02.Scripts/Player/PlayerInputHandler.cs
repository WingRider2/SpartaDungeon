using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerController controller;


    private double _pressTime;


    public void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            controller.SetMoveInput(context.ReadValue<Vector2>());
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            controller.SetMoveInput(Vector2.zero);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                // 누른 순간 기록
                _pressTime = context.time;
                break;
            case InputActionPhase.Canceled:

                // 뗐을 때 총 누른 시간 계산
                float held = (float)(context.time - _pressTime);
                if (held >= 0.2f)
                    controller.SetJumpInput(2.0f);
                else 
                    controller.SetJumpInput(1.0f);
                break;


            default: break;
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        controller.SetMouseDelta(context.ReadValue<Vector2>());

    }
    public void OnInventory(InputAction.CallbackContext context)
    {

    }
    public void OnInteraction(InputAction.CallbackContext context)
    {

    }
    public void OnDash(InputAction.CallbackContext context)
    {

    }
    public void OnSwitchViewMode(InputAction.CallbackContext context)
    {

    }

}
