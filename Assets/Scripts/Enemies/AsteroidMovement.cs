using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);
            GameManager._GAME_MANAGER.DamagePlayer(1f);
        }
    }
}
