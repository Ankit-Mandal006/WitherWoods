using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureAi2 : MonoBehaviour
{
    public Thundering th;
    public PortalSpawner ps;
    public Terrain[] terrains;
    private NavMeshAgent agent;
    public bool isEating = true,catchPayer=false,a=false;
    public GameObject player, eatingPoint, location,jumpscare,cam,flash,portals;
    public Animator amnim1,amnim2;
    private Vector3 locationTarget;
    private Vector3 lastPos;
    public MouseLook ml;
    private bool isWaitingToInvestigate = false;
    public Transform spawnPoint;
    private List<Vector3> occupiedPositions = new List<Vector3>();

    void Start()
    {
        terrains=ps.terrains;
        agent = GetComponent<NavMeshAgent>();
        locationTarget = eatingPoint.transform.position;
        agent.SetDestination(locationTarget);
        lastPos = player.transform.position;
    }
    void A()
    {
        CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            player.transform.position = spawnPoint.position;
            HashSet<int> usedIndices = new HashSet<int>();
            int selectedIndex;
            do
            {
                selectedIndex = Random.Range(0, terrains.Length);
            }
            while (usedIndices.Contains(selectedIndex) ||
                   usedIndices.Contains((selectedIndex - 1 + terrains.Length) % terrains.Length) ||
                   usedIndices.Contains((selectedIndex + 1) % terrains.Length));

            usedIndices.Add(selectedIndex);
            Terrain selectedTerrain = terrains[selectedIndex];

            Vector3 spawnPos;
            do
            {
                spawnPos = GetSafeRandomPoint(selectedTerrain);
            }
            while (IsTooCloseToOthers(spawnPos));

            occupiedPositions.Add(spawnPos);

            GameObject instance = Instantiate(portals, spawnPos, Quaternion.identity);
            th.portal = instance.transform;
            th.thunder = true;
            ps.p[2] = instance;
            Invoke("ReactivateController", 0.1f);
        Invoke("B",.1f);
    }
    void ReactivateController()
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = true;
        ml.enabled=true;
        player.GetComponent<Animator>().SetTrigger("ideal");
        player.GetComponent<PlayerMovement>().enabled=true;
        //GameObject obj = Instantiate(this.gameObject, new Vector3(-131f, 0f, 810f), Quaternion.identity);
        //obj.SetActive(false);
        Destroy(this.gameObject);
        //this.transform.position=new Vector3(-131,0,810);
        catchPayer=false;
    }
    Vector3 GetSafeRandomPoint(Terrain terrain)
    {
        Vector3 point;
        do
        {
            point = GetRandomPointOnTerrain(terrain);
        }
        while (Vector3.Distance(point, player.transform.position) < 20f);

        return point;
    }

    Vector3 GetRandomPointOnTerrain(Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;
        float x = Random.Range(0, terrainData.size.x);
        float z = Random.Range(0, terrainData.size.z);
        float y = terrainData.GetHeight((int)x, (int)z) + terrain.transform.position.y;

        return new Vector3(x + terrain.transform.position.x, y, z + terrain.transform.position.z);
    }

    bool IsTooCloseToOthers(Vector3 pos, int skipIndex = -1)
    {
        for (int k = 0; k < occupiedPositions.Count; k++)
        {
            if (k == skipIndex) continue;
            if (Vector3.Distance(pos, occupiedPositions[k]) < 100)
                return true;
        }
        return false;
    }
    void Update()
    {
        agent.SetDestination(locationTarget);
        if(catchPayer)
            locationTarget=player.transform.position;
        float d=Vector3.Distance(player.transform.position,this.transform.position);
        if(catchPayer&&d<=2f)
        {
            //cam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            player.GetComponent<Animator>().SetTrigger("CreatureJumpScare");
            player.GetComponent<PlayerMovement>().enabled=false;
            flash.SetActive(false);
            ml.enabled=false;
            player.GetComponent<Animator>().SetBool("holdLight",false);
            Invoke("A",2f);
            cam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            agent.isStopped = true;
            agent.ResetPath();
            this.transform.position=new Vector3(100,100,100);
            //this.gameObject.SetActive(false);
        }
       
        if(a)
        {
            float dist = Vector3.Distance(player.transform.position, lastPos);
            
            
             if ( dist > .2f )
        {
            KillPlayer();
        }
        }
        else{
            lastPos = player.transform.position;
        }
        float distance = Vector3.Distance(transform.position, locationTarget);
        if (distance <= 2f && !isWaitingToInvestigate)
        {
            isEating=!isEating;
            if (isEating)
            {
                /*amnim1.SetTrigger("invistigate");
                amnim2.SetTrigger("invistigate");
                Invoke("ReturntoEat", 3f);*/
                locationTarget = eatingPoint.transform.position;

            }
            else
            {
                a=false;
                isWaitingToInvestigate = true;
                amnim2.SetTrigger("eating");
                amnim1.SetTrigger("eating");
                Invoke("Investigate",4.7f);
            }
        }
    }
    void ReturntoEat()
    {
        //isEating=true;
         
    }
    void Investigate()
    {
        //isEating=false;
        a=true;
        float radius = Random.Range(4f, 10f);
        Vector2 randomPos = Random.insideUnitCircle.normalized * radius;
        locationTarget = player.transform.position + new Vector3(randomPos.x, 0, randomPos.y);
        isWaitingToInvestigate = false;
    }

    bool IsPlayerMoving()
    {
        float dist = Vector3.Distance(player.transform.position, lastPos);
        lastPos = player.transform.position;
        return dist > 1f;
    }

    void KillPlayer()
    {
        catchPayer=true;
        Debug.Log("Player caught while moving during investigation!");
    }
}
