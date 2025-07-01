using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMainPortal : MonoBehaviour
{
    public GameObject crystal, DarkSparks, Sparks, Sides, Light,Player,Direction,text,nextLevel;
    public PlayerInventory pi;
    bool CastRay=false;
    public CapsuleCollider cc;
    public int count =0,count1=0;
    public Animator aim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CastRay)
        {
            RaycastHit hit;
            Ray ray=new Ray(Player.transform.position,Direction.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

            if (Physics.Raycast(ray, out hit, 10f, ~0, QueryTriggerInteraction.Collide))
            {
                Debug.Log(hit.collider.name);
                Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);
                if (hit.collider.CompareTag("Place_Skull") && pi.hasSkull && !hit.collider.transform.GetChild(0).gameObject.activeSelf)
                {
                    text.SetActive(true);
                    //aim.SetBool("excited",true);
                    if ((Input.GetButtonDown("Interact") || Input.GetButton("Interact")))
                    {
                        //Debug.Log(hit.collider.name);
                        hit.collider.transform.GetChild(0).gameObject.SetActive(true);
                        pi.hasSkull = false;
                        Player.GetComponent<Animator>().SetBool("holdCrystal", false);
                        pi.skull.SetActive(false);
                        count++;
                    }
                }

                else if (hit.collider.CompareTag("Place_Crystal") && pi.hasCrystal1 && !hit.collider.transform.GetChild(0).gameObject.activeSelf)
                {
                    //aim.SetBool("excited",true);
                    text.SetActive(true);
                    if ((Input.GetButtonDown("Interact") || Input.GetButton("Interact")))
                    {
                        //Debug.Log(hit.collider.name);
                        hit.collider.transform.GetChild(0).gameObject.SetActive(true);
                        pi.hasCrystal1 = false;
                        Player.GetComponent<Animator>().SetBool("holdCrystal", false);
                        pi.crystal1.SetActive(false);
                        count1++;
                    }
                }
                else
                    //aim.SetBool("excited",false);
                    text.SetActive(false);

            }
            if (count == 6 && count1 == 3)
            {
                //crystal.SetActive(true);
                DarkSparks.SetActive(true);
                Sparks.SetActive(true);
                Sides.SetActive(true);
                Light.SetActive(true);
                cc.enabled = true;
                nextLevel.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            CastRay=true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            CastRay=false;
        }
    }
}
