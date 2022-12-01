using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    protected float health = 1;
    protected float maxHealth = 1;

    protected virtual void Start()
    {
        RoomManager.Instance.AddEnemy(this);
    }

    protected virtual void OnDeath() 
    {
        RoomManager roomManager = RoomManager.Instance;

        roomManager.RemoveEnemy(this);
        if (roomManager.GetNumberOfEnemies <= 0){
            roomManager.GetCurrentRoom.OpenDoors();
        }

        Destroy(gameObject);
    }

    public virtual void OnHit() 
    {
        health--;
        if (health <= 0){
            OnDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            OnHit();
        }
    }
}
