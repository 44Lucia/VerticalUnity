using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichPate : MonoBehaviour
{
    [SerializeField] private GameObject textItem;
    [SerializeField] private GameObject impactEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager._GAME_MANAGER.SetChargesUltimate != GameManager._GAME_MANAGER.SetMaxChargesUltimate)
            {
                textItem.SetActive(true);
                GameManager._GAME_MANAGER.SetChargesUltimate += 1;
                GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effectIns, 5f);
                transform.position = new Vector3(0, 0, 0);
                Destroy(gameObject, 3);
            }
        }
    }
}
