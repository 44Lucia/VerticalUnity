using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    [SerializeField] private float lastFire;

    [SerializeField] private FamiliarData familiar;
    private InputManager input;
    [SerializeField] private float lastOffsetX;
    [SerializeField] private float lastOffsetY;

    private void Start()
    {
        input = InputManager._INPUT_MANAGER;
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting the same of PlayerController
        float shootHor = input.GetShootButtonPressed().x;
        float shootVert = input.GetShootButtonPressed().y;

        float horizontal = input.GetMovementButtonPressed().x;
        float vertical = input.GetMovementButtonPressed().y;

        if (horizontal != 0 || vertical != 0)
        {
            float offsetX = (horizontal < 0) ? Mathf.Floor(horizontal) : Mathf.Ceil(horizontal);
            float offsetY = (vertical < 0) ? Mathf.Floor(vertical) : Mathf.Ceil(vertical);
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, familiar.speed * Time.deltaTime);
            lastOffsetX = offsetX;
            lastOffsetY = offsetY;
        }
        else 
        {
            if (!(transform.position.x < lastOffsetX + 0.5f) || !(transform.position.y < lastOffsetY + 0.5f))
            {
                //transform.position = Vector3.MoveTowards(transform.position, new Vector2(PlayerController.Instance.transform.position.x - lastOffsetX, PlayerController.Instance.transform.position.y - lastOffsetY), familiar.speed * Time.deltaTime);
            }
        }
    }
}
