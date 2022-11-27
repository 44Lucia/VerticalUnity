using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichRotten : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Health < 0)
            {
                GameManager.Health -= 1;
            }
        }
    }
}
