using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Thundering th;
    public PlayerInventory pi;
    public PortalSpawner ps;
    Animator anim;
    public Camera cam ;
    public GameObject player;
    public string SpawnPoint,CurrentProtal;
    public GameObject spawnPoint,crystal;
    DeActive_After_Teleport dat;
    void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        th = GameObject.FindWithTag("thundering").GetComponent<Thundering>();
        player = GameObject.FindWithTag("Player");
        pi = player.GetComponent<PlayerInventory>();
        dat =this.GetComponent<DeActive_After_Teleport>();
        anim=player.GetComponent<Animator>();
        Set();
    }
    void Update()
    {
        Set();
    }
    public void Set()
    {
        //player = GameObject.FindWithTag("Player");
        anim=player.GetComponent<Animator>();
        spawnPoint = GameObject.Find(SpawnPoint);

        if (spawnPoint == null)
        {
            Debug.Log("Spawn point not found! Check the SpawnPoint name.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(dat!=null)
                dat.isTrue=true;
            Debug.Log("Player Entered Portal");
            StartCoroutine(TeleportWithDelay(other));
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player EXITED Portal");
        }
    }

    IEnumerator TeleportWithDelay(Collider player)
    {
       
        anim.SetBool("holdCrystal",false);
        yield return new WaitForEndOfFrame(); // Ensures smooth transition

        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false; // Disable to avoid conflicts
        }

        // Ensure spawnPoint exists
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            if (SpawnPoint == "ForestSpawnPoint")
            {
                //pi.hasCrystal = false;
                pi.nearCrystal = false;
                if (CurrentProtal != null)
                {
                    int j = 0;
                    foreach (GameObject i in ps.portals)
                    {
                        if(i!=null)
                            if (i.name == CurrentProtal)
                                ps.portals[j] = null;
                        j++;

                    }
                }
                anim.SetBool("holdCrystal", true);
                crystal.SetActive(true);
                player.GetComponent<PlayerInventory>().hasCrystal1 = true;
                MazeTimer mt = player.GetComponent<MazeTimer>();
                mt.startTimer = false;
                if (th != null)
                    th.canThunder = true;
                /*crystal = Instantiate(crystal, transform.position, Quaternion.identity, transform);
                crystal.transform.SetParent(cam.transform);
                crystal.transform.localPosition=new Vector3(-0.556f, -0.281f, 0.914f);
                crystal.transform.localScale=new Vector3(.01f,.01f, 0.01f);
                crystal.transform.localRotation = Quaternion.Euler(17.83f, 177.76f, 8.18f);*/
            }
            if (SpawnPoint == "MazeSpawnPoint")
            {
                //pi.hasCrystal = false;
                pi.nearCrystal = false;
                MazeTimer mt = player.GetComponent<MazeTimer>();
                mt.startTimer = true;
                if (th != null)
                    th.canThunder = false;
            }
            if (SpawnPoint == "DoorwaySpawnPoint" || SpawnPoint == "A")
            {
                //pi.hasCrystal = false;
                pi.nearCrystal = false;
                if (th != null)
                    th.canThunder = false;
            }
        }
        else
        {
            Debug.LogError("Spawn point is missing! Teleport failed.");
        }

        yield return new WaitForSeconds(0.1f); // Small delay

        if (controller != null)
        {
            controller.enabled = true; // **Ensure re-enabling!**
            Debug.Log("Teleported using CharacterController");
            
        }
        else
        {
            Debug.Log("Teleported using Transform");
            
        }
    }
}
