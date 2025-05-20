using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerController controller;

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
        
        if (context.phase == InputActionPhase.Started)
        {            
            controller.SetJumpInput();
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {

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
