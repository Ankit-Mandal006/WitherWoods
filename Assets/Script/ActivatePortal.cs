using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePortal : MonoBehaviour
{
    public PlayerInventory pi;
    bool canActivate=false;
    public GameObject crystal, DarkSparks, Sparks, Sides, Light,mainPortal,crystal_prefab;
    public CapsuleCollider cc;
    public Animator aim;

    void Start()
    {
        aim=GameObject.FindWithTag("Aim").GetComponent<Animator>();
        mainPortal = GameObject.Find("Main Portal");
        pi=GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }
    void Update()
    {
        if(canActivate && pi.hasCrystal)
        {
            aim.SetBool("excited",true);
            GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("holdCrystal",false);
            if(Input.GetButtonDown("Interact")){
            pi.hasCrystal=false;
            //GameObject c=Instantiate(crystal_prefab,transform.position, Quaternion.identity);
            //c.transform.SetParent(mainPortal.transform, true); 
            //c.transform.localPosition=new Vector3(0f,0.112f,0f);
            //c.transform.localScale=new Vector3(0.01f,0.01f,0.01f);
            //c.tag="Crystal";
            //c.SetActive(true);
            //SphereCollider s=c.GetComponent<SphereCollider>();
            //s.enabled=true;
            //s.isTrigger = true;
            pi.crystal.SetActive(false);
            crystal.SetActive(true);
            DarkSparks.SetActive(true);
            Sparks.SetActive(true);
            Sides.SetActive(true);
            Light.SetActive(true);
            cc.enabled = true; // Enable collider properly
            Invoke("Destroy",1f);}
        }
        else
        {
            aim.SetBool("excited",false);
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        { canActivate=true;}
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        { canActivate=false;}
    }
}
