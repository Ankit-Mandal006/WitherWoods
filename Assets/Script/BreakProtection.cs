using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BreakProtection : MonoBehaviour
{
    public Transform player;
    public Image image;
    public GameObject myText;
    public float interactRadius = 2f;
    public DeActivateShield das;
private float T = 0f;
    void Start()
    {
        //das = GameObject.FindWithTag("Sheild").GetComponent<DeActivateShield>();
}
    void Update()
    {
        image.fillAmount = T / 3.0f;
        if (Vector3.Distance(transform.position, player.position) <= interactRadius)
        {
            myText.SetActive(true);
            if (Input.GetButton("Interact"))
            {

                T += Time.deltaTime;

                if (T >= 3f)
                {
                    das.DeActivate();
                    T = 0f;
                    Destroy(gameObject);



                }
            }
            else
            {
                T = 0f;
            }
        }
        else
        {
            myText.SetActive(false);
            T = 0f;
        }
    
}
void OnDrawGizmosSelected()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, interactRadius);
}
}

