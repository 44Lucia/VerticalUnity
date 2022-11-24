using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Manager")]
    private InputManager input;

    [Header("Components")]
    private CharacterController player;

    [Header("PlayerParameters")]
    //Jump
    private float gravity = 20f;
    //Movement
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    [Header("PlayerBooleans")]
    private bool isWalking;

    [Header("Direction&Velocity")]
    private Vector3 finalVelocity;
    private Vector3 direction;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
    }

    private void Start()
    {
        input = InputManager._INPUT_MANAGER;

        //Default values movement
        finalVelocity = Vector3.zero;
        if (speed == 0f) { speed = 1f; }
        if (maxSpeed == 0f) { maxSpeed = 7f; }
    }

    private void Update()
    {
        //Movement
        Move();

        player.Move(finalVelocity * Time.deltaTime);

    }

    private void Move()
    {
        //GetAxis: calcular velocidad de X y Z
        direction = new Vector3(input.GetMovementButtonPressed().x, direction.y, input.GetMovementButtonPressed().y);
        direction.y = -1f;
        direction.Normalize();

        //Acceleration
        if (isMoving() && speed < maxSpeed) { speed += 6 * Time.deltaTime; }
        else if (!isMoving() && speed > 0f) { speed = 1f; }
        if (speed > maxSpeed) { speed = maxSpeed; }

        //Velocidad final XZ
        finalVelocity.x = direction.x * speed;
        finalVelocity.z = direction.z * speed;

        //Animaciónes
        if (isMoving()){ isWalking = true; }
        else { isWalking = false; }
    }

    private bool isMoving()
    {
        return finalVelocity.x != 0 || finalVelocity.z != 0;
    }
}
