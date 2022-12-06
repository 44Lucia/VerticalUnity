using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAerialEnemies : MonoBehaviour
{
    [SerializeField] float speed = 70f;
    [SerializeField] int damage = 1;
    [SerializeField] float explosionRadius = 0f;

    private Vector3 dir;

    private void Start()
    {
        Destroy(gameObject, 1);
    }

    private void Update()
    {
        float distanceThisFrame = speed * Time.deltaTime;

        dir = transform.forward;

        transform.Translate(dir * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage();
        }

        Destroy(gameObject);
    }

    private void Damage()
    {
        GameManager._GAME_MANAGER.DamagePlayer(1f);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Damage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            HitTarget();
        }
    }
}
