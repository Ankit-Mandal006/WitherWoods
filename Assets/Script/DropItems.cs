using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    public PlayerInventory pi;
    public CrystalBheaviour cb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((pi.hasCrystal||pi.hasSkull) && Input.GetButtonDown("Drop"))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            if(Physics.Raycast(ray,out hit,10f) && hit.collider.CompareTag("Ground")) 
            {
                this.gameObject.GetComponent<CapsuleCollider>().enabled=true;
                Instantiate(this.gameObject, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
                if(cb!=null)
                {
                    cb.crystalDroped=true;
                }
                pi.hasCrystal=false;
                pi.hasSkull=false;
                this.enabled=false;
            }
        }
    }
}
