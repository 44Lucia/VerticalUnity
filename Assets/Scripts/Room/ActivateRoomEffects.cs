using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRoomEffects : MonoBehaviour
{
    [SerializeField] GameObject poisonEffect;
    // Update is called once per frame
    private void Start()
    {
        poisonEffect.SetActive(true);
    }
}
