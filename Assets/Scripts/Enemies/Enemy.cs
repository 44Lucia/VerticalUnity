using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    protected float health = 2;
    protected float maxHealth = 2;

    protected bool coolDownAttack = false;
    protected float coolDown;

    protected GameObject impactEffect;

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
            PlayerController.Instance.ResetStats();
            GameManager._GAME_MANAGER.GetInCombat = false;
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

    public virtual IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            GameObject effectIns2 = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns2, 2f);
            OnHit();
        }
    }
}
