using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _GAME_MANAGER;
    private bool isPaused = false;

    //Funciones para upgradear las stats
    [SerializeField] private float timeToDestroyBullet;

    //Contendra las recargas de la ulti
    [SerializeField] private float ultimateCharges = 0;
    [SerializeField] private float maxUltimateCharges = 4;

    [SerializeField] private float health = 6;
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private bool isInvincible = false;

    private bool finishTimeToPoison = false;
    private bool inCombat = false;

    [Header("Music")]
    [SerializeField] AudioClip m_onHitClip;

    //Una lista de los objetos recogidos

    private void Awake()
    {
        if (_GAME_MANAGER != null && _GAME_MANAGER != this){
            Destroy(_GAME_MANAGER);
        }else{
            _GAME_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        timeToDestroyBullet = 0.4f;
    }

    public void DamagePlayer(float damage) 
    {
        health -= damage;
        GameObject effectIns = Instantiate(impactEffect, PlayerController.Instance.transform.position, PlayerController.Instance.transform.rotation);
        Destroy(effectIns, 2f);

        AudioManager.Instance.UIEffectsAudioSource.PlayOneShot(m_onHitClip);

        if (health <= 0){
            KillPlayer();
        }

    }

    public void HealPlayer(int healAmount) 
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    private void KillPlayer() 
    {
        Time.timeScale = 0;
        UIManager.Instance.GetLoseUI.SetActive(true);
    }

    public void pauseGame(bool isPause)
    {
        isPaused = isPause;
    }

    public float Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public float GetTimeToDestroyBullet => timeToDestroyBullet;
    public float SetTimeToDestroyBullet { get => timeToDestroyBullet; set => timeToDestroyBullet = value; }

    public float GetChargesUltimate => ultimateCharges;
    public float SetMaxChargesUltimate { get => maxUltimateCharges; set => maxUltimateCharges = value; }
    public float SetChargesUltimate { get => ultimateCharges; set => ultimateCharges = value; }

    public bool GetFinishTimePoison { get => finishTimeToPoison; set => finishTimeToPoison = value; }

    public bool GetInCombat { get => inCombat; set => inCombat = value; }

}
