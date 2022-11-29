using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private Input playerInput;
    public static InputManager _INPUT_MANAGER;

    private UnityEvent scapePressed;

    //Movement
    private Vector2 currentMovementInput;

    //Attack
    private Vector2 currentShootInput;

    //Puse
    private float timeSincePausePressed = 0f;

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
            playerInput.Character.PauseMenu.performed += PauseMenuPressed;
            playerInput.Character.PauseMenu.performed += PauseMenuPressedCallback;
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }

        scapePressed = new UnityEvent();

    }
    private void Update()
    {
        timeSincePausePressed += Time.deltaTime;

        InputSystem.Update();
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }

    private void ShootAxisUpdate(InputAction.CallbackContext context) 
    {
        currentShootInput = context.ReadValue<Vector2>();

        Debug.Log("Magnitude" + currentShootInput.magnitude);
        Debug.Log("Normalized" + currentShootInput.normalized);
    }

    private void PauseMenuPressed(InputAction.CallbackContext context) 
    {
        timeSincePausePressed = 0f;
    }

    public void AddListennerToPressScape(UnityAction action) 
    {
        scapePressed.AddListener(action);
    }

    private void PauseMenuPressedCallback(InputAction.CallbackContext context) { scapePressed.Invoke(); }

    public void RemoveListennerToPressScape(UnityAction action)
    {
        scapePressed.RemoveListener(action);
    }

    public Vector2 GetMovementButtonPressed() => this.currentMovementInput;

    public Vector2 GetShootButtonPressed() => this.currentShootInput;

    public bool GetPauseButtonPressed() => this.timeSincePausePressed == 0f;
}
