using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichPork : MonoBehaviour
{ 
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             if (GameManager.Health != GameManager.MaxHealth)
             {
                 GameManager.Health += 1;
             }
         }
    }
}
