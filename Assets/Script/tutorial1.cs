using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial1 : MonoBehaviour
{
    public GameObject flashlight, compass, f, c,text,crystal,cry,nl,portal;
    public bool hasCryst=false,can=false,door=false;
    public FlashLight fl;
    public Compass com;
    public AudioSource d1, d2;
    public GameObject dt1, dt2;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("A1", 2f);
    }
    void A1()
    {
        d1.Play();

    }
    void A2()
    {
        d2.Play();

    }
    // Update is called once per frame
    void Update()
    {
        if (d1.isPlaying)
        {
            dt1.SetActive(true); // Keep dt1 active while audio is playing
        }
        else
        {
            dt1.SetActive(false); // Deactivate dt1 when audio stops
        }
         if (d2.isPlaying)
        {
            dt2.SetActive(true); // Keep dt1 active while audio is playing
        }
        else
        {
            dt2.SetActive(false); // Deactivate dt1 when audio stops
        }
        if (!hasCryst && flashlight.activeSelf && compass.activeSelf)
        {
            crystal.SetActive(true);
            portal.SetActive(true);
            com.portal = portal.transform;
            if (i == 0)
                Invoke("A2", 2f);
            i = 1;
        }
        if (Input.GetButtonDown("Interact"))
        {
            if (f != null)
            {
                text.SetActive(false);
                flashlight.SetActive(true);
                f.SetActive(false);
                fl.enabled = true;
            }
            if (c != null)
            {
                text.SetActive(false);
                compass.SetActive(true);
                c.SetActive(false);

            }
            if (can)
            {
                text.SetActive(false);
                hasCryst = true;
                crystal.SetActive(false);
            }
            if (door && hasCryst)
            {
                text.SetActive(false);
                nl.SetActive(true);
            }
            }
    }
    void LateUpdate()
    {
        if (hasCryst)
        {
            cry.SetActive(true);
            this.GetComponent<Animator>().SetBool("holdCrystal", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlashLight"))
        {
            f = other.gameObject;
            text.SetActive(true);
        }
        if (other.CompareTag("compass"))
        {
            c = other.gameObject;
            text.SetActive(true);
        }
        if (other.CompareTag("Crystal"))
        {
            text.SetActive(true);
            can = true;
        }
        if (other.CompareTag("DOOR"))
        {
            text.SetActive(true);
            door = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FlashLight"))
        {
            f = null;
            text.SetActive(false);
        }
        if (other.CompareTag("compass"))
        {
            c = null;
            text.SetActive(false);
        }
        if (other.CompareTag("Crystal"))
        {
            text.SetActive(false);
            can = false;
        }
        if (other.CompareTag("DOOR"))
        {
            text.SetActive(false);
            door = false;
        }
    }
}
