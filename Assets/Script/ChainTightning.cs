using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTightning : MonoBehaviour
{
    // Start is called before the first frame update
    bool canInteract=false,HasInteracted=false;
    float T=0f;
    public int count =0;
    public SphereCollider sc;
    public GameObject nearbyObject;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetButton("Interact") )
        {
            T+=Time.deltaTime;
            if(T>=3f)
            {
                sc=nearbyObject.GetComponent<SphereCollider>();
                T=0f;
                HasInteracted=true;
                sc.enabled=false;
                count++;
                Destroy(nearbyObject);
            }
            
        }
        else
                T=0f;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Chain"))
        {
            nearbyObject = other.gameObject;
            canInteract=true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Chain"))
        {
            nearbyObject = null;
            canInteract=false;
        }
    }
}
