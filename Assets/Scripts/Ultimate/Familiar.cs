using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float orbitDistance;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        orbitSystem();
    }

    private void orbitSystem()
    {
        transform.position = new Vector3(transform.position.x, 1.4f, transform.position.z);
        transform.position = PlayerController.Instance.transform.position + (transform.position - PlayerController.Instance.transform.position).normalized * orbitDistance;
        transform.RotateAround(PlayerController.Instance.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
    }
}
