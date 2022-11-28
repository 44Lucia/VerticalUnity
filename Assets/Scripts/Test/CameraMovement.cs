using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : Singleton<CameraMovement>
{
    //Poner limites en la camara que siga al jugador 

    //Que cambie de posicion y limites cuando cambias de habitación

    private float moveSpeedWhenRoomChange = 100;

    protected override void Awake()
    {
        base.Awake();
    }

    public void UpdatePosition() 
    {
        if (RoomManager.Instance.GetCurrentRoom == null){
            Debug.Log("no room");
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
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
