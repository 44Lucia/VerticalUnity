using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Manager")]
    private InputManager input;

    [Header("Components")]
    private CharacterController player;

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
    private float bulletSpeed;
    private float lastFire;
    private float fireDelay;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<CharacterController>();
    }

    private void Start()
    {
        input = InputManager._INPUT_MANAGER;

        fireDelay = 0.5f;
        bulletSpeed = 7.5f;

        //Default values movement
        finalVelocity = Vector3.zero;
        if (speed == 0f) { speed = 8f; }

    }

    private void Update()
    {
        //Movement
        Move();
        Shooting();
        
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

    private bool isMoving()
    {
        return finalVelocity.x != 0 || finalVelocity.z != 0;
    }

    private void Shooting() 
    {
        float shootHor = input.GetShootButtonPressed().x;
        float shootVert = input.GetShootButtonPressed().y;

        if (input.GetShootButtonPressed().x == -1)
        {
            //Izquierda
        }
        else if (input.GetShootButtonPressed().x == 1)
        {
            //Derecha
        }
        else if (input.GetShootButtonPressed().y == -1) 
        { 
            //Abajo
        }
        else if (input.GetShootButtonPressed().y == 1)
        {
            //Arriba
        }

        if ((shootHor !=0 || shootVert !=0) && Time.time > lastFire + fireDelay){
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(
                (shootHor < 0) ? Mathf.Floor(shootHor) * bulletSpeed : Mathf.Ceil(shootHor) * bulletSpeed,
                (shootVert < 0) ? Mathf.Floor(shootVert) * bulletSpeed : Mathf.Ceil(shootVert) * bulletSpeed,
                0
                );
            lastFire = Time.time;
        }
    }

    public void SetPosition(Vector3 newPos) 
    {
        player.enabled = false;
        player.transform.position = newPos;
        player.enabled = true;
    }

    public float Speed { get { return speed; } set { speed = value; } }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {


    }
}
