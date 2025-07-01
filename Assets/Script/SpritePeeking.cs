using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePeeking : MonoBehaviour
{
    int count = 0;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim=this.gameObject.GetComponent<Animator>();
    }

   
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {   if(count==0)
                anim.SetBool("peeking",true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            count = 1;
            anim.SetBool("peeking",false);
        }
    }
}
