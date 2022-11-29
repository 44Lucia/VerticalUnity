using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichPork : MonoBehaviour
{ 
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             if (GameManager.Instance.Health != GameManager.Instance.MaxHealth)
             {
                 GameManager.Instance.Health += 1;
             }
         }
    }
}
