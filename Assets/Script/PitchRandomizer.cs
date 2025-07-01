using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    public AudioSource audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources.pitch = Random.Range(.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
