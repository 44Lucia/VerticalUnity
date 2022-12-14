using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTreasureDoor : MonoBehaviour
{
    private Animator anim;
    private bool isOnButton = false;

    [Header("TreasureRoom")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private Door treasureDoor;

    [Header("Music")]
    [SerializeField] AudioClip m_onOpenClip;

    [Header("Tutorial")]
    [SerializeField] private GameObject lightObject;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (lightObject != null){
            lightObject.SetActive(false);
        }

        InputManager._INPUT_MANAGER.AddListennerToPressOpenTreasureRoom(OpenDoor);
    }

    private void OpenDoor() 
    {
        if (isOnButton){
            InputManager._INPUT_MANAGER.RemoveListennerToPressOpenTreasureRoom(OpenDoor);
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 3f);
            if (lightObject != null){
                lightObject.SetActive(true);
            }
            AudioManager.Instance.UIEffectsAudioSource.PlayOneShot(m_onOpenClip);
            treasureDoor.SetTreasureDoorState = false;
            anim.SetBool("Activated", true);
            Debug.Log("treasure");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOnButton = true;
            Debug.Log("dentro");

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputManager._INPUT_MANAGER.AddListennerToPressOpenTreasureRoom(OpenDoor);
            isOnButton = false;
            Debug.Log("fuera");
        }
    }
}
