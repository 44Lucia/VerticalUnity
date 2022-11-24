using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Input playerInput;
    public static InputManager _INPUT_MANAGER;

    //Movement
    private Vector2 currentMovementInput;

    //Attack
    private Vector2 currentShootInput;

    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(_INPUT_MANAGER);

        }
        else
        {
            playerInput = new Input();
            playerInput.Character.Enable();

            playerInput.Character.Move.performed += LeftAxisUpdate;
            playerInput.Character.Attack.performed += ShootAxisUpdate;
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }

    }
    private void Update()
    {
        InputSystem.Update();
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }

    private void ShootAxisUpdate(InputAction.CallbackContext context) 
    {
        currentShootInput = context.ReadValue<Vector2>();
    }

    public Vector2 GetMovementButtonPressed() => this.currentMovementInput;

    public Vector2 GetShootButtonPressed() => this.currentShootInput;
}
