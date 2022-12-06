using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichPork : MonoBehaviour
{ 
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             if (GameManager._GAME_MANAGER.Health != GameManager._GAME_MANAGER.MaxHealth)
             {
                 GameManager._GAME_MANAGER.Health += 1;
             }
         }
    }
}
