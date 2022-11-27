using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Poner limites en la camara que siga al jugador 
    [SerializeField] private Transform targetToFollow;

    //Que cambie de posicion y limites cuando cambias de habitación

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Mathf.Clamp(targetToFollow.position.x, -35.63f, 5.5f)
            );
    }
}
