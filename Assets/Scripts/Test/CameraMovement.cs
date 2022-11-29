using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : Singleton<CameraMovement>
{
    //Poner limites en la camara que siga al jugador 

    //Que cambie de posicion y limites cuando cambias de habitación

    [SerializeField] private float moveSpeedWhenRoomChange = 100;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition() 
    {
        if (RoomManager.Instance.GetCurrentRoom == null){
            Debug.Log("no room");
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();
        targetPos.y = transform.position.y;
        Vector3 dir = targetPos - transform.position;

        if (dir.sqrMagnitude < 0.1f){
            return;
        }
        dir.Normalize();

        transform.position += moveSpeedWhenRoomChange * dir * Time.deltaTime;

        //transform.position = targetPos;
    }

    Vector3 GetCameraTargetPosition() 
    {
        if (RoomManager.Instance.GetCurrentRoom == null)
        {
            return Vector3.zero;
        }
        Vector3 targetPos = RoomManager.Instance.GetCurrentRoom.GetRoomCenter();

        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
