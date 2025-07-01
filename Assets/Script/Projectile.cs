using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public GameObject explosion;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.ReactToHit(damage);
        }
        Instantiate(explosion, collision.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);
    }
}
