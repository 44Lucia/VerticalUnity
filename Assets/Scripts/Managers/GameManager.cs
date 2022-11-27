using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _GAME_MANAGER;
    [SerializeField] private Text healthText;

    private static int health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    //Contendra las recargas de la ulti

    //Funciones para upgradear las stats

    //Una lista de los objetos recogidos

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

    

    private void Awake()
    {
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

    public static void DamagePlayer(int damage) 
    {
        health -= damage;

        if (health <= 0){
            KillPlayer();
        }

    }

    public static void HealPlayer(int healAmount) 
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    private static void KillPlayer() 
    { 
        
    }

}
