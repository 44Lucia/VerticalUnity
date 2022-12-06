using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager._GAME_MANAGER.SetTimeToDestroyBullet = 0.6f;
            Destroy(this.gameObject);
        }
    }
}
