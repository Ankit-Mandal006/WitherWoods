using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableEnemy : Enemy
{
    public float health = 50f,totalhealth;
    void Start()
    {
        totalhealth = health;
    }

    public override void ReactToHit(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}

