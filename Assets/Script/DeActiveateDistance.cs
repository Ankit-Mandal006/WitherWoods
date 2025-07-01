using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActiveateDistance : MonoBehaviour
{
    public Transform player;
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateActivation", 10f, 1f);
    }

    // Update is called once per frame
    void UpdateActivation()
    {
        float d = Vector3.Distance(player.position, this.transform.position);
        if (d <= 100)
            a.SetActive(true);
        else
            a.SetActive(false);
    }
}
