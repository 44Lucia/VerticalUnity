using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{
    [SerializeField] private GameObject textItem;
    [SerializeField] private GameObject impactEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textItem.SetActive(true);
            GameManager._GAME_MANAGER.SetTimeToDestroyBullet = 0.6f;
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);
            transform.position = new Vector3(0, 0, 0);
            Destroy(gameObject, 3);
        }
    }
}
