using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedLightGreenLightPortal : MonoBehaviour
{
    public int count=0;
    float t=2f;
    public bool a=false;
    public GameObject a1,b,c,d,f,text;
    public Image img;
    public CapsuleCollider e;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = (-t+2.0f) / 2.0f;
            if(Input.GetButton("Interact")){
                if(a){t = t-Time.deltaTime;
                if(t<=0)
                {
                    count++;
                        t=2f;
                    f.SetActive(false);
                    text.SetActive(false);
                    a =false;
                }}
                
            }
            if(Input.GetButtonUp("Interact"))
            {
                t=2f;
            }
        if(count ==3)
        {
            a1.SetActive(true);
            b.SetActive(true);
            c.SetActive(true);
            d.SetActive(true);
            e.enabled=true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("magicalOrb"))
        {
            text.SetActive(true);
            a =true;
            t=2f;
            f=other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("magicalOrb"))
        {
            text.SetActive(true);
            if (t <= 0)
            {
                other.gameObject.SetActive(false);
                a = false;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("magicalOrb")){
            text.SetActive(false);
            a =false;
            t=2f;}
    }
}
