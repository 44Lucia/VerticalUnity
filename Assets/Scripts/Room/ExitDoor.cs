using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            UIManager.Instance.GetWinUI.SetActive(true);
            UIManager.Instance.GetBosBarUI.SetActive(false);
        }
    }
}
