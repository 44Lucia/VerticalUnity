using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateUIController : MonoBehaviour
{
    [SerializeField] private GameObject ultimateContainer;

    private float fillValue;

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameManager._GAME_MANAGER.SetChargesUltimate;
        fillValue = fillValue / GameManager._GAME_MANAGER.SetMaxChargesUltimate;
        ultimateContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
