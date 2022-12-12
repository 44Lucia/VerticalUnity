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

    public void SetRoomTo(Room room) 
    {
        PlayerController.Instance.ResetStats();
        currentRoom = room;
        room.gameObject.SetActive(true);
        StartCoroutine(InitRoom(room));
        GameManager._GAME_MANAGER.GetInCombat = true;
    }

    public IEnumerator InitRoom(Room room)
    {
        yield return new WaitForSeconds(0.1f);
        if (currenEnemies.Count != 0)
        {
            room.initRoom();
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(520, 400, 100,20), "HitEnemy", "button"))
        {
            for (int i = 0; i < currenEnemies.Count; i++){
                currenEnemies[i].OnHit();
            }
        }
        if (GUI.Button(new Rect(630, 400, 130, 20), "AddUltimatePoint", "button"))
        {
            GameManager._GAME_MANAGER.SetChargesUltimate += 1;
        }
        if (GUI.Button(new Rect(380, 400, 130, 20), "AddLives", "button"))
        {
            GameManager._GAME_MANAGER.Health += 1;
        }
    }

    public int GetNumberOfEnemies => currenEnemies.Count;

    public Room GetCurrentRoom => currentRoom;

    public List<Enemy> GetCurrentEnemies => currenEnemies;

    public Room CurrentRoom { get { return currentRoom; } set { currentRoom = value; }  }
}
