using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    protected float health = 3;
    protected float maxHealth = 3;

    protected bool coolDownAttack = false;
    protected float coolDown;

    protected GameObject impactEffect;

    //Tutorial
    protected GameObject lightOn;

    //Music
    protected AudioClip onHitClip;

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

        if (lightOn != null)
        {
            lightOn.SetActive(true);
        }

        Destroy(gameObject);
    }

    public virtual void OnHit() 
    {
        AudioManager.Instance.UIEffectsAudioSource.PlayOneShot(onHitClip);
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
