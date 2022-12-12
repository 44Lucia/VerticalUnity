using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    protected float health = 2;
    protected float maxHealth = 2;

    protected bool coolDownAttack = false;
    protected float coolDown;

    protected MeshRenderer sprite;
    protected GameObject impactEffect;

    protected virtual void Start()
    {
        RoomManager.Instance.AddEnemy(this);
        sprite = GetComponent<MeshRenderer>();
    }

    protected virtual void OnDeath() 
    {
        RoomManager roomManager = RoomManager.Instance;

        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

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
