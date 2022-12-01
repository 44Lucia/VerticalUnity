using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] float speed = 70f;
    [SerializeField] int damage = 1;
    [SerializeField] float explosionRadius = 0f;

    private Vector3 dir;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        Destroy(gameObject, 3);

        dir.x = target.position.x - transform.position.x;
        dir.z = target.position.z - transform.position.z;
    }

    private void Update()
    {
        if (target == null){
            return;
        }

        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget() 
    {
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Damage(Transform enemy) 
    {
        GameManager.Instance.DamagePlayer(0.5f);
    }

    private void Explode() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Damage(collider.transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HitTarget();
        }
    }
}
