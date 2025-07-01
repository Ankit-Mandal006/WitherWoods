using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    public GameObject ind;
    public AudioSource audio;
    public GameObject text;
    public bool hasCrystal = false; // Track if the player has the crystal
    public bool hasSkull = false;
    public bool hasCrystal1=false;
    public bool nearCrystal=false;
    public Animator anim;
    
    public Camera cam;
    public GameObject nearbyCrystal,crystal,nearbySkull,skull,nearbyCrystal1,crystal1,mainPortal,crystalPrefab,mainCam,nearbyFlash,Flash; // Store reference to the crystal

    public AudioSource d5;
    public GameObject dt5;
    int z = 0;
    void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        anim=this.gameObject.GetComponent<Animator>();
    }
    void A5()
    {
        d5.Play();
    }
    private void LateUpdate()
    {
        if (d5.isPlaying)
            dt5.SetActive(true);
        else
            dt5.SetActive(false);
        if (hasCrystal)
        {
            if (z == 0)
            {
                Invoke("A5", 2f);
            }
            z = 1;
            //text.SetActive(false);
            nearCrystal = false;
            crystal.SetActive(true);
            nearbyCrystal.SetActive(false);
        }
        else if (!hasCrystal)
        {
            //audio.Play();

            crystal.SetActive(false);
            nearbyCrystal.SetActive(true);
        }
        if (hasCrystal1)
        {
            //text.SetActive(false);
            crystal1.SetActive(true);
            //audio.Play();
        }
        else if (!hasCrystal1)
            crystal1.SetActive(false);
        if (hasSkull)
        {
            //text.SetActive(false);
            skull.SetActive(true);
            //audio.Play();
        }
        else if (!hasSkull)
            skull.SetActive(false);
    }
    void Update()
    {
        
        if (Input.GetButtonDown("Interact") && nearbyFlash != null)
        {

            Flash.SetActive(true);
            anim.SetBool("holdLight", true);
            Destroy(nearbyFlash);
        }
        if (Input.GetButtonDown("Interact") && nearbyCrystal != null && !hasCrystal && !hasSkull && !hasCrystal1 && nearCrystal) // Press Interact key
        {
            
            hasCrystal = true;
            audio.Play();
            /*crystal = Instantiate(crystalPrefab, transform.position, Quaternion.identity, mainCam.transform);
            crystal.transform.SetParent(cam.transform);
            crystal.name="crystal";
            crystal.transform.localPosition=new Vector3(-0.757f, -0.383f, 1.132f);
            crystal.transform.localRotation = Quaternion.Euler(6.269f, -243.523f,-11.777f);
            Destroy(nearbyCrystal); 
            crystal.GetComponent<SphereCollider>().enabled=false;
            Debug.Log("Crystal collected!");*/
            crystal.SetActive(true);
            nearbyCrystal.SetActive(false);
            anim.SetBool("holdCrystal",true);
            Debug.Log("Crystal Accuired");
        }/*
        if (Input.GetButtonDown("Interact") && nearbyCrystal1 != null  && !hasCrystal && !hasSkull && !hasCrystal1) // Press Interact key
        {
            
            hasCrystal1 = true;
            audio.Play();
            /*crystal1 = Instantiate(nearbyCrystal1, transform.position, Quaternion.identity, transform);
            crystal1.transform.SetParent(cam.transform);
            crystal1.transform.localPosition=new Vector3(-0.556f, -0.281f, 0.914f);
            crystal1.transform.localRotation = Quaternion.Euler(17.83f, 177.76f, 8.18f);
            Destroy(nearbyCrystal1); // Remove the crystal from the scene
            crystal1.SetActive(true);
            //Destroy(nearbyCrystal1);
            //nearbyCrystal1.SetActive(false);
            anim.SetBool("holdCrystal",true);
            crystal1.GetComponent<SphereCollider>().enabled=false;
            Debug.Log("Crystal1 collected!");
        }*/
        if (Input.GetButtonDown("Interact") && nearbySkull != null  && !hasCrystal && !hasSkull && !hasCrystal1) // Press Interact key
        {
            
            hasSkull = true;
            audio.Play();
            /*skull = Instantiate(nearbySkull, transform.position, Quaternion.identity, transform);
            skull.transform.SetParent(cam.transform);
            skull.transform.localPosition=new Vector3(-0.556f, -0.281f, 0.914f);
            skull.transform.localRotation = Quaternion.Euler(-111f, 100f, -19f);*/
            skull.SetActive(true);
            Destroy(nearbySkull);
            anim.SetBool("holdCrystal",true);
            skull.GetComponent<CapsuleCollider>().enabled=false;
            Debug.Log("Skull collected!");
        }
        
        
        if(Input.GetButtonDown("Drop"))
        {
            /*
            if (hasCrystal)
            {
                nearCrystal = false;
                anim.SetBool("holdCrystal", false);
                crystal.GetComponent<SphereCollider>().enabled = true;
                GameObject c = Instantiate(crystal, transform.position, Quaternion.identity);
                c.transform.SetParent(mainPortal.transform, true);
                c.transform.localPosition = new Vector3(0, 0, 0);
                crystal.SetActive(false);
                nearbyCrystal.SetActive(true);
                //Destroy(crystal);
                hasCrystal = false;
            }
            if(hasCrystal1)
            {
                anim.SetBool("holdCrystal",false);
                crystal1.GetComponent<SphereCollider>().enabled=true;
                GameObject c=Instantiate(crystal1,transform.position, Quaternion.identity);
                
                c.transform.position=new Vector3(c.transform.position.x,0.112f,c.transform.position.z);
                c.transform.localScale=new Vector3(0.01f,.01f,.01f);
                crystal1.SetActive(false);

                //Destroy(crystal1);
                hasCrystal1=false;
            }*/
            if(hasSkull)
            {
                anim.SetBool("holdCrystal",false);
                skull.GetComponent<CapsuleCollider>().enabled=true;
                skull.SetActive(false);
                GameObject s=Instantiate(skull,transform.position, Quaternion.identity);
                s.transform.position=new Vector3(transform.position.x,0.112f,transform.position.z);
                s.transform.rotation=Quaternion.Euler(-135f, Random.Range(0f,360f), 0f);
                s.transform.localScale=new Vector3(100,100,100);
                s.SetActive(true);
                GameObject i=Instantiate(ind, s.transform);
                i.transform.localScale=new Vector3(0.001f,0.001f,0.001f);
                //Destroy(skull);
                hasSkull =false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Crystal"))
        {
            nearCrystal = true;
            text.SetActive(true);
            //nearbyCrystal = other.gameObject; // Store crystal reference
        }
        if(other.CompareTag("Skull"))
        {
            nearbySkull=other.gameObject;
            text.SetActive(true);
            Debug.Log("Enter Skull");
        }
        if (other.CompareTag("Crystal1"))
        {
            nearbyCrystal1 = other.gameObject; // Store crystal reference
            text.SetActive(true);
        }
        if (other.CompareTag("FlashLight"))
        {
            nearbyFlash = other.gameObject;
            text.SetActive(true);
        }
    }

    private void OnTrigger(Collider other) 
    {
        if (other.CompareTag("Crystal")) 
        {
            nearCrystal=true;
            //nearbyCrystal = other.gameObject; // Store crystal reference
        }
        if(other.CompareTag("Skull"))
        {
            nearbySkull=other.gameObject;
            Debug.Log("Enter Skull");
        }
        if (other.CompareTag("Crystal1")) 
        {
            nearbyCrystal1 = other.gameObject; // Store crystal reference
        }
        if (other.CompareTag("FlashLight")) 
        {
            nearbyFlash=other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Crystal"))
        {
            nearCrystal = false;
            text.SetActive(false);
            //nearbyCrystal = null; // Clear reference when player moves away
        }
        if (other.CompareTag("Skull"))
        {
            nearbySkull = null;
            text.SetActive(false);
        }
        if (other.CompareTag("Crystal1"))
        {
            nearbyCrystal1 = null; 
            text.SetActive(false);
        }
        if (other.CompareTag("FlashLight"))
        {
            nearbyFlash = null;
            text.SetActive(false);
        }
    }
    private void CollectCrystal1()
    {
        /*if (nearbyCrystal1 != null) 
        {*/
            hasCrystal1 = true;
            crystal1 = Instantiate(nearbyCrystal1, transform.position, Quaternion.identity, transform);
            crystal1.transform.localPosition=new Vector3(-0.57f, 0f, 0.63f);
            Destroy(nearbyCrystal1); // Remove the crystal from the scene
            crystal1.GetComponent<SphereCollider>().enabled=false;
            Debug.Log("Crystal1 collected!");
            //crystal.GetComponent<DropItems>().enabled=true;
        //}
    }

    private void CollectCrystal() 
    {
        /*if (nearbyCrystal != null) 
        {*/
            hasCrystal = true;
            crystal = Instantiate(crystalPrefab, transform.position, Quaternion.identity, transform);
            crystal.transform.localPosition=new Vector3(-0.57f, 0f, 0.63f);
            Destroy(nearbyCrystal); 
            crystal.GetComponent<SphereCollider>().enabled=false;
            Debug.Log("Crystal collected!");
            //crystal.GetComponent<DropItems>().enabled=true;
        //}
    }
    private void CollectSkull() 
    {
        /*if (nearbySkull != null) 
        {*/
            hasSkull = true;
            skull = Instantiate(nearbySkull, transform.position, Quaternion.identity, transform);
            skull.transform.localPosition=new Vector3(-0.57f, 0f, 0.63f);
            skull.transform.localRotation = Quaternion.Euler(-111f, 100f, -19f);
            Destroy(nearbySkull); // Remove the crystal from the scene
            skull.GetComponent<CapsuleCollider>().enabled=false;
            Debug.Log("Skull collected!");
            //skull.GetComponent<DropItems>().enabled=true;
        //}
    }
}
