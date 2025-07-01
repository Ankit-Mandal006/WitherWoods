using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayWaveContoller : MonoBehaviour
{
    public Animator[] s;
    bool canpeek=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canpeek)
        {
            for(int i=0;i<5;i++)
            {
                s[i].SetBool("peeking",true);
            }
        }
        else
        {
            for(int i=0;i<5;i++)
            {
                s[i].SetBool("peeking",false);
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            canpeek=true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            canpeek=false;
            
        }
    }
}
