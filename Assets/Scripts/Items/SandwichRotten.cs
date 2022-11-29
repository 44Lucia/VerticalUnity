using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichRotten : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.Health < 0)
            {
                GameManager.Instance.Health -= 1;
            }
        }
    }
}
