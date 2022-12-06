using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    protected float health = 2;
    protected float maxHealth = 2;

    protected MeshRenderer sprite;

    protected virtual void Start()
    {
        RoomManager.Instance.AddEnemy(this);
        sprite = GetComponent<MeshRenderer>();
    }

    protected virtual void OnDeath() 
    {
        RoomManager roomManager = RoomManager.Instance;

        roomManager.RemoveEnemy(this);
        if (roomManager.GetNumberOfEnemies <= 0){
            roomManager.GetCurrentRoom.OpenDoors();
            GameManager._GAME_MANAGER.SetChargesUltimate += 1;
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

    private IEnumerator FlashRed() 
    {
        sprite.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.material.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            StartCoroutine(FlashRed());
            OnHit();
        }
    }
}
