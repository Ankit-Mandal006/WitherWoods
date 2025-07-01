using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterSometime : MonoBehaviour
{
    public AudioSource audio;
    public float T = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Enable",T);
    }
    void Enable()
    {
        audio.volume = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
