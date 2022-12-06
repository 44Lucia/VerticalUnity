using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichRotten : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager._GAME_MANAGER.Health < 0)
            {
                GameManager._GAME_MANAGER.Health -= 1;
            }
        }
    }
}
