using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrefusaPipes : MonoBehaviour
{
    [SerializeField] private GameObject textItem;
    [SerializeField] private GameObject impactEffect;

    [Header("Music")]
    [SerializeField] AudioClip m_pickItemClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.UIEffectsAudioSource.PlayOneShot(m_pickItemClip);
            textItem.SetActive(true);
            PlayerController.Instance.Speed += 2;
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);
            transform.position = new Vector3(0, 0, 0);
            Destroy(gameObject, 3);
        }
    }
}
