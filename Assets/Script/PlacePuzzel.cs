using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePuzzel : MonoBehaviour
{
    public GameObject location, puzzelpice, block;

    void Update()
    {
        if (puzzelpice != null && location != null) 
        {
            float distance = Vector3.Distance(location.transform.position, puzzelpice.transform.position);
            if (distance <= 2f)
            {
                Destroy(puzzelpice);
                block.SetActive(true);
            }
        }
    }
}