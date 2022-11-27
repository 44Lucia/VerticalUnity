using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] private Room currentRoom;

    private List<Enemy> currenEnemies;

    protected override void Awake()
    {
        base.Awake();
        currenEnemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        currenEnemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        currenEnemies.Remove(enemy);
    }

    public void ActiveRoom(Room room, bool value) 
    {
        room.gameObject.SetActive(value);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("HitAllEnemies"))
        {
            for (int i = 0; i < currenEnemies.Count; i++)
            {
                currenEnemies[i].OnHit();
            }
        }
    }

    public int GetNumberOfEnemies => currenEnemies.Count;

    public Room GetCurrentRoom => currentRoom;

    public Room CurrentRoom { get { return currentRoom; } set { currentRoom = value; }  }
}
