using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private Input playerInput;
    public static InputManager _INPUT_MANAGER;

    //PauseMenu
    private UnityEvent scapePressed;

    //Movement
    private Vector2 currentMovementInput;

    //Attack
    private Vector2 currentShootInput;

    //Ultimate
    private UnityEvent ultimatePressed;

    //TreasureRoom
    private UnityEvent treasureDoorPressed;

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
            playerInput.Character.PauseMenu.performed += PauseMenuPressedCallback;
            playerInput.Character.Ultimate.performed += UltimateButtonPressedCallback;
            playerInput.Character.OpenTreasureRoom.performed += OpenTreasurePressedCallback;
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }

        scapePressed = new UnityEvent();
        ultimatePressed = new UnityEvent();
        treasureDoorPressed = new UnityEvent();

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

    private void PauseMenuPressedCallback(InputAction.CallbackContext context) 
    { 
        scapePressed.Invoke(); 
    }

    private void UltimateButtonPressedCallback(InputAction.CallbackContext context) 
    {
        ultimatePressed.Invoke();
    }

    private void OpenTreasurePressedCallback(InputAction.CallbackContext context) 
    {
        treasureDoorPressed.Invoke();
    }

    public void AddListennerToUltimateButton(UnityAction action) 
    { 
        ultimatePressed.AddListener(action); 
    }

    public void RemoveListennerToUltimateButton(UnityAction action)
    {
        ultimatePressed.RemoveListener(action);
    }

    public void AddListennerToPressScape(UnityAction action) 
    {
        scapePressed.AddListener(action);
    }


    public void RemoveListennerToPressScape(UnityAction action)
    {
        scapePressed.RemoveListener(action);
    }

    public void AddListennerToPressOpenTreasureRoom(UnityAction action)
    {
        treasureDoorPressed.AddListener(action);
    }

    public void RemoveListennerToPressOpenTreasureRoom(UnityAction action)
    {
        treasureDoorPressed.RemoveListener(action);
    }

    public Vector2 GetMovementButtonPressed() => this.currentMovementInput;

    public Vector2 GetShootButtonPressed() => this.currentShootInput;
}
