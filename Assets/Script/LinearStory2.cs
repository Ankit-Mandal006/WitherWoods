using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearStory2 : MonoBehaviour
{
    public EnemySpawnner es;
    public KillableEnemy ke;
    public DeActivateShield das;
    public GameObject sheild;
    int c = 0;
    // Start is called before the first frame update
    void Start()
    {
        es.enabled = false;
        das.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (-ke.health + ke.totalhealth >= 10 && c != 1)
        {
            es.enabled = true;
            das.enabled = true;
            sheild.SetActive(true);
            c = 1;
        }
    }
}
