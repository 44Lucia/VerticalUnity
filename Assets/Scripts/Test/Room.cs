using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //Controla mediante el GameManager los puntos de la ulti

    //Ver si esa sala tiene algun efecto comparando con los tags
    private bool hasEffect;

    //Cuando entres spawnean los enemigos en la sala mediante un collider
    private Collider colliderToSpawn;


    [SerializeField]private List<Door> listDoors;

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
}
