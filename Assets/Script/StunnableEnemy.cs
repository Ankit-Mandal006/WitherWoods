using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnableEnemy : Enemy
{
    public CreatureAI cai;
    public float stunTime = 3f;
    public bool isStunned = false;
    GameObject Player;

    void Start()
    {
        cai = GetComponent<CreatureAI>();
        Player = GameObject.FindWithTag("Player");
    }
    public override void ReactToHit(float damage)
    {
        if (!isStunned) StartCoroutine(Stun());
    }
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, Player.transform.position);
        if (isStunned||distance>100)
            this.gameObject.GetComponent<FootSteps_AudioManager>().enabled = false;
        else
            this.gameObject.GetComponent<FootSteps_AudioManager>().enabled = true;
    }
    IEnumerator Stun()
    {
        
        isStunned = true;
        cai.agent.isStopped = true;
        // Add stun effect here (animation, disable movement, etc.)
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        cai.agent.isStopped = false;
        //this.gameObject.GetComponent<FootSteps_AudioManager>().enabled = true;
    }
}

