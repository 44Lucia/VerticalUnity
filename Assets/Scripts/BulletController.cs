using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 0.5f;
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay() 
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
