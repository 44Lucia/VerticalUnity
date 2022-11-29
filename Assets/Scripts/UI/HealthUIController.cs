using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private GameObject heartContainer;

    private float fillValue;

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameManager.Instance.Health;
        fillValue = fillValue / GameManager.Instance.MaxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
