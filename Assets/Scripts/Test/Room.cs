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

    [SerializeField] private int X;
    [SerializeField] private int Y;
    [SerializeField] private int Z;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, Depth));
    }

    public Vector3 GetRoomCenter() 
    {
        return new Vector3(X * Width, Y * Height, Z * Depth);
    }
}
