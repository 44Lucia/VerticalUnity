using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Controla mediante el GameManager los puntos de la ulti

    //Ver si esa sala tiene algun efecto comparando con los tags
    private bool hasEffect;

    //Cuando entres spawnean los enemigos en la sala mediante un collider


    [SerializeField]private List<Door> listDoors;

    [SerializeField] private int Height;
    [SerializeField] private int Width;
    [SerializeField] private int Depth;

    private void Awake()
    {
        listDoors = new List<Door>();
    }

    public void AddDoor(Door door)
    {
        listDoors.Add(door);
    }

    public void RemoveDoor(Door door)
    {
        listDoors.Remove(door);
    }

    public void OpenDoors()
    {
        foreach (Door door in listDoors)
        {
            door.OpenDoor();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos.z += Depth * 0.5f;
        pos.x += Width * 0.37f;
        pos.y += Height * 0.5f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, new Vector3(Width, Height, Depth));
    }

    public Vector3 GetRoomCenter() 
    {
        Vector3 pos = transform.position;
        pos.z += Depth * 0.5f;
        pos.x += Width * 0.37f;
        pos.y += Height * 0.5f;

        return pos;
    }
}
