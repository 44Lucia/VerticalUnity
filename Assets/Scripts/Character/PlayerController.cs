using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Manager")]
    [SerializeField]private InputManager input;

    [Header("Components")]
    private CharacterController player;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject shield;
    private AnimatorController aController;

    [Header("PlayerParameters")]
    //Movement
    [SerializeField] private float speed;
    private float gravity = 20f;

    [Header("Direction&Velocity")]
    private Vector3 finalVelocity;
    private Vector3 direction;

    private float turnSmoothTime;
    private float turnSmoothSpeed;

    [Header("ShootParameters")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float countdownBetweenFire = 0f;
    [SerializeField] private float fireRate = 2f;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<CharacterController>();
        aController = GetComponent<AnimatorController>();
    }

    private void Start()
    {
        input = InputManager._INPUT_MANAGER;

        //Default values movement
        finalVelocity = Vector3.zero;
        if (speed == 0f) { speed = 8f; }

        input.AddListennerToUltimateButton(Ultimate);
    }

    private void Update()
    {
        if (!isMoving()){
            aController.playAnimation(ANIMATIONS.Idle, 0.0f);
        }
        else{
            aController.playAnimation(ANIMATIONS.Move, 0.0f);
        }

        //Movement
        Move();
        Shooting();

        Debug.Log(GameManager._GAME_MANAGER.GetChargesUltimate);


        //if (!player.isGrounded){ finalVelocity.y += direction.y * gravity * Time.deltaTime; }

        player.Move(finalVelocity * Time.deltaTime);

    }

    private void Move()
    {
        //GetAxis: calcular velocidad de X y Z
        direction = new Vector3(input.GetMovementButtonPressed().x, direction.y, input.GetMovementButtonPressed().y);
        direction.y = -1f;
        direction.Normalize();

        //Velocidad final XZ
        finalVelocity.x = direction.x * speed;
        finalVelocity.z = direction.z * speed;

        float targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothSpeed, turnSmoothTime);
        if (isMoving()) { transform.rotation = Quaternion.Euler(0f, angle, 0f); }
    }

    private void Ultimate() 
    {
        if (GameManager._GAME_MANAGER.GetChargesUltimate == 4)
        {
            Debug.Log("Ultimate");
            shield.SetActive(!shield.activeSelf);
            InputManager._INPUT_MANAGER.RemoveListennerToUltimateButton(Ultimate);
            GameManager._GAME_MANAGER.SetChargesUltimate = 0;
        }
    }

    private bool isMoving()
    {
        return finalVelocity.x != 0 || finalVelocity.z != 0;
    }

    private void InstBullet() 
    {
        if (countdownBetweenFire <= 0f)
        {
            aController.playAnimation(ANIMATIONS.Attack, 0.0f);
            Instantiate(bulletPrefab, spawnPos.transform.position, transform.rotation);
            countdownBetweenFire = 1f / fireRate;
        }
        countdownBetweenFire -= Time.deltaTime;
    }

    private void Shooting() 
    {
        //Shoot left
        if (input.GetShootButtonPressed().x == -1)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            InstBullet();
        }

        //Shoot right
        else if (input.GetShootButtonPressed().x == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            InstBullet();
        }

        //Shoot down
        else if (input.GetShootButtonPressed().y == -1) 
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            InstBullet();
        }

        //Shoot up
        else if (input.GetShootButtonPressed().y == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            InstBullet();
        }
    }

    public void SetPosition(Vector3 newPos) 
    {
        player.enabled = false;
        player.transform.position = newPos;
        player.enabled = true;
    }

    public float Speed { get { return speed; } set { speed = value; } }
}