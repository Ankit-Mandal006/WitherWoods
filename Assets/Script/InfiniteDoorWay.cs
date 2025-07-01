using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteDoorWay : MonoBehaviour
{
    public TeleportPlayer[] portals;
    public GameObject green_crystal;

    void Start()
    {
        SetPortalConnection();
    }

    public void SetPortalConnection()
    {
        // First, set all portals to "DoorwaySpawnPoint"
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].SpawnPoint = "DoorwaySpawnPoint";
            portals[i].Set();
            //portals[i].GetComponent<TeleportPlayer>().crystal=green_crystal;
        }

        // Randomly select **any** portal to be the "ForestSpawnPoint"
        int randomIndex = Random.Range(0, portals.Length);
        portals[randomIndex].SpawnPoint = "ForestSpawnPoint";
        portals[randomIndex].Set();

        Debug.Log($"Portal {randomIndex} is now set to ForestSpawnPoint");
    }
}
