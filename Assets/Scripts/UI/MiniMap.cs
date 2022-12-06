using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 newPosition = PlayerController.Instance.transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
