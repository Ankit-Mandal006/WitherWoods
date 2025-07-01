using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActive_Monster : MonoBehaviour
{
    public GameObject creep;

    void Start()
    {
       // creep = GameObject.FindWithTag("Creep");
    }

    void Update()
    {
        // No need to find every frame 🚫
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DOOR") && creep != null)
        {
            Instantiate(creep, new Vector3(-131f, 0f, 810f), Quaternion.identity).SetActive(true);
        }
    }
}
