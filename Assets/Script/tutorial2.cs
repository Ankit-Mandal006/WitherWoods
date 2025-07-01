using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial2 : MonoBehaviour
{
    public GameObject dt3;
    public AudioSource d3;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("A3", 2f);
    }
    void A3()
    {
        d3.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (d3.isPlaying)
        {
            dt3.SetActive(true);
        }
        else
        {
            dt3.SetActive(false);
        }
    }
}
