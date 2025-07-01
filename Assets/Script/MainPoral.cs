using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPoral : MonoBehaviour
{
    public PlayerInventory pi;
    public ActivateMainPortal amp;

    public GameObject[] skull, skull_placeHolder, skull_ind;
    public GameObject[] crystal, crystal_placeHolder, crystal_ind;
    public GameObject a;
    public SphereCollider b;

    void Start()
    {
        
    }

    void Update()
    {
        if (pi.hasSkull)
        {
            for (int i = 0; i < skull.Length; i++)
            {
                bool isSkullActive = skull[i].activeSelf;

                //skull_placeHolder[i].SetActive(isSkullActive);
                skull_placeHolder[i].GetComponent<CapsuleCollider>().enabled = !isSkullActive;
                skull_ind[i].SetActive(!isSkullActive);
            }
        }
        else
        {
            for (int i = 0; i < skull.Length; i++)
            {
                //bool isSkullActive = skull[i].activeSelf;

                //skull_placeHolder[i].SetActive(isSkullActive);
                skull_placeHolder[i].GetComponent<CapsuleCollider>().enabled = false;
                skull_ind[i].SetActive(false);
            }
        }
        if (pi.hasCrystal1)
        {
            for (int i = 0; i < crystal.Length; i++)
            {
                bool isSkullActive = crystal[i].activeSelf;

                //skull_placeHolder[i].SetActive(isSkullActive);
                crystal_placeHolder[i].GetComponent<SphereCollider>().enabled = !isSkullActive;
                crystal_ind[i].SetActive(!isSkullActive);
            }
        }
        else
        {
            for (int i = 0; i < crystal.Length; i++)
            {
                //bool isSkullActive = skull[i].activeSelf;

                //skull_placeHolder[i].SetActive(isSkullActive);
                crystal_placeHolder[i].GetComponent<SphereCollider>().enabled = false;
                crystal_ind[i].SetActive(false);
            }
        }
        if (!pi.hasCrystal1 && !pi.hasSkull)
        {
            a.SetActive(true);
            b.enabled = true;
        }
        else
        {
            a.SetActive(false);
            b.enabled = false;
        }
    }
}
