using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public static GameManager _GAME_MANAGER;
    [SerializeField] private Text healthText;
    private bool isPaused = false;

    private float health = 6;
    private int maxHealth = 6;
    //Contendra las recargas de la ulti

    //Funciones para upgradear las stats

    //Una lista de los objetos recogidos

    public float Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }



    protected override void Awake()
    {
        base.Awake();
        if (_GAME_MANAGER != null && _GAME_MANAGER != this){
            Destroy(_GAME_MANAGER);
        }else{
            _GAME_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        healthText.text = "Health: " + health;
    }

    public void DamagePlayer(float damage) 
    {
        health -= damage;

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
        
    }

    public void pauseGame(bool isPause)
    {
        isPaused = isPause;
    }

}
